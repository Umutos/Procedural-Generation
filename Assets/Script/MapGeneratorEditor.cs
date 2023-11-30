using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Récupère l'instance du MapGenerator ciblée
        MapGenerator mapGenerator = (MapGenerator)target;

        // Affiche les propriétés par défaut de l'inspecteur
        if (DrawDefaultInspector())
        {
            // Si la propriété autoUpdate est activée, génère automatiquement la carte
            if (mapGenerator.autoUpdate)
            {
                mapGenerator.GenerateMap();
            }
        }

        // Ajoute un bouton pour générer manuellement la carte
        if (GUILayout.Button("Generated"))
        {
            mapGenerator.GenerateMap();
        }
    }
}
