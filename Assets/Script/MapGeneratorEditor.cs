using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // R�cup�re l'instance du MapGenerator cibl�e
        MapGenerator mapGenerator = (MapGenerator)target;

        // Affiche les propri�t�s par d�faut de l'inspecteur
        if (DrawDefaultInspector())
        {
            // Si la propri�t� autoUpdate est activ�e, g�n�re automatiquement la carte
            if (mapGenerator.autoUpdate)
            {
                mapGenerator.GenerateMap();
            }
        }

        // Ajoute un bouton pour g�n�rer manuellement la carte
        if (GUILayout.Button("Generated"))
        {
            mapGenerator.GenerateMap();
        }
    }
}
