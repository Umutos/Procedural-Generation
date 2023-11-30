using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap,
        ColorMap,
        Mesh
    }

    [Header("Map Settings")]
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float scale;
    public int seed;
    public Vector2 offset;

    [Header("Noise Settings")]
    public int octaves;
    [Range(0f, 1f)]
    public float persistence;
    public float lacunarity;

    [Header("Mesh Settings")]
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    [Header("Regions")]
    public TypeDeTerrain[] regions;

    public bool autoUpdate;

    public void GenerateMap()
    {
        // G�n�re la carte de bruit
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistence, lacunarity, offset);

        // G�n�re la carte de couleur en fonction des r�gions d�finies
        Color[] colorMap = GenerateColorMap(noiseMap);

        // Affiche la carte en fonction du mode de dessin s�lectionn�
        DisplayMap(noiseMap, colorMap);
    }

    private Color[] GenerateColorMap(float[,] noiseMap)
    {
        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                // Assigner une couleur en fonction de la hauteur dans les r�gions d�finies
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break; // Arr�ter la boucle une fois que la r�gion appropri�e est trouv�e
                    }
                }
            }
        }

        return colorMap;
    }

    private void DisplayMap(float[,] noiseMap, Color[] colorMap)
    {
        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            mapDisplay.DrawTexture(TextureMapGenerator.GenerateHeightMapTexture(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            mapDisplay.DrawTexture(TextureMapGenerator.GenerateColorMapTexture(colorMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            mapDisplay.DrawMesh(MeshGenerator.GenerateGroundMesh(noiseMap, meshHeightMultiplier, meshHeightCurve),
                                TextureMapGenerator.GenerateColorMapTexture(colorMap, mapWidth, mapHeight));
        }
    }

    private void OnValidate()
    {
        // Assurez-vous que les param�tres sont valides
        mapWidth = Mathf.Max(1, mapWidth);
        mapHeight = Mathf.Max(1, mapHeight);
        octaves = Mathf.Max(0, octaves);
        lacunarity = Mathf.Max(1, lacunarity);
    }
}

[System.Serializable]
public struct TypeDeTerrain
{
    public string name;
    public float height;
    public Color color;
}
