using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer rendererMap;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    // Affiche une texture sur le renderer de la carte.
    public void DrawTexture(Texture2D texture2D)
    {
        rendererMap.sharedMaterial.mainTexture = texture2D;
        rendererMap.transform.localScale = new Vector3(texture2D.width, 1, texture2D.height);
    }

    // Affiche un mesh en utilisant les données de mesh fournies et une texture.
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
