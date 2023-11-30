using UnityEngine;

public static class MeshGenerator
{
    // Génère les données de mesh pour représenter le terrain en fonction de la carte de hauteur.
    public static MeshData GenerateGroundMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        // Initialisation des données de mesh
        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        // Boucle pour générer les vertices et les triangles
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Position en fonction de la carte de hauteur et de la courbe d'élévation
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);

                // Coordonnées de texture
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                // Ajout des triangles sauf pour les bords du mesh
                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    // Données du mesh
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    // Constructeur
    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    // Ajoute un triangle aux données du mesh
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    // Crée un objet Mesh à partir des données du mesh
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }
}
