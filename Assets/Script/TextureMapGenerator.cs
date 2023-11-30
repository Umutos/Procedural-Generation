using UnityEngine;

public static class TextureMapGenerator
{
    // G�n�re une texture color�e � partir d'une carte de couleurs.
    public static Texture2D GenerateColorMapTexture(Color[] colorMap, int width, int height)
    {
        // Cr�e une nouvelle texture avec les param�tres sp�cifi�s
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        // Applique la carte de couleurs � la texture
        texture.SetPixels(colorMap);
        texture.Apply();

        return texture;
    }

    // G�n�re une texture bas�e sur une carte de hauteur.
    public static Texture2D GenerateHeightMapTexture(float[,] heightMap)
    {
        // R�cup�re les dimensions de la carte de hauteur
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // Initialise un tableau de couleurs en fonction de la carte de hauteur
        Color[] colors = GenerateColorsFromHeightMap(heightMap, width, height);

        // G�n�re et retourne la texture � partir du tableau de couleurs
        return GenerateColorMapTexture(colors, width, height);
    }

    // G�n�re un tableau de couleurs � partir d'une carte de hauteur.
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
