using UnityEngine;

public static class TextureMapGenerator
{
    // Génère une texture colorée à partir d'une carte de couleurs.
    public static Texture2D GenerateColorMapTexture(Color[] colorMap, int width, int height)
    {
        // Crée une nouvelle texture avec les paramètres spécifiés
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        // Applique la carte de couleurs à la texture
        texture.SetPixels(colorMap);
        texture.Apply();

        return texture;
    }

    // Génère une texture basée sur une carte de hauteur.
    public static Texture2D GenerateHeightMapTexture(float[,] heightMap)
    {
        // Récupère les dimensions de la carte de hauteur
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // Initialise un tableau de couleurs en fonction de la carte de hauteur
        Color[] colors = GenerateColorsFromHeightMap(heightMap, width, height);

        // Génère et retourne la texture à partir du tableau de couleurs
        return GenerateColorMapTexture(colors, width, height);
    }

    // Génère un tableau de couleurs à partir d'une carte de hauteur.
    private static Color[] GenerateColorsFromHeightMap(float[,] heightMap, int width, int height)
    {
        Color[] colors = new Color[width * height];

        // Remplit le tableau de couleurs en fonction de la carte de hauteur
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colors[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return colors;
    }
}
