using UnityEngine;

public static class Noise
{
    // G�n�re une carte de bruit en utilisant le bruit de Perlin avec des octaves.
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        // Initialisation de la carte de bruit
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // G�n�ration d'une graine al�atoire
        System.Random rand = new System.Random(seed);

        // G�n�ration des offsets pour chaque octave
        Vector2[] octaveOffsets = GenerateOctaveOffsets(octaves, rand, offset);

        // �vite une division par z�ro si scale est �gal � 0
        scale = Mathf.Max(scale, 0.0001f);

        // Initialisation des valeurs pour la normalisation
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // Calcul des coordonn�es du centre de la carte
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        // Boucle pour g�n�rer la carte de bruit
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // Initialisation des param�tres pour chaque point de la carte
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                // Boucle pour chaque octave
                for (int i = 0; i < octaves; i++)
                {
                    // Calcul des coordonn�es �chantillonn�es en fonction de l'octave
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // G�n�ration du bruit de Perlin
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    // Ajustement des param�tres pour l'octave suivante
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                // Mise � jour des valeurs maximales et minimales pour la normalisation
                maxNoiseHeight = Mathf.Max(maxNoiseHeight, noiseHeight);
                minNoiseHeight = Mathf.Min(minNoiseHeight, noiseHeight);

                // Assignation de la valeur de bruit au point correspondant dans la carte
                noiseMap[x, y] = noiseHeight;
            }
        }

        // Normalisation de la carte de bruit
        NormalizeNoiseMap(noiseMap, minNoiseHeight, maxNoiseHeight);

        return noiseMap;
    }

    // G�n�re des offsets al�atoires pour chaque octave.
    private static Vector2[] GenerateOctaveOffsets(int octaves, System.Random rand, Vector2 offset)
    {
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rand.Next(-100000, 100000) + offset.x;
            float offsetY = rand.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        return octaveOffsets;
    }

    // Normalise la carte de bruit pour ajuster les valeurs entre 0 et 1.
    private static void NormalizeNoiseMap(float[,] noiseMap, float minNoiseHeight, float maxNoiseHeight)
    {
        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
    }
}
