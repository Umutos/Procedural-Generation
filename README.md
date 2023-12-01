# Map Generation Unity Tool

Ce projet Unity permet de générer des cartes de terrain en utilisant du bruit de Perlin. Il offre la possibilité de visualiser les cartes de bruit, les cartes de couleur et les maillages 3D générés à partir des données de hauteur.

## Utilisation

0. Si jamais la scène n'est pas ouverte de base, allez dans le dossier **Scene** puis ouvrez la scene. De plus, si jamais la mesh est tout blanche, ALORS appuyez sur **Generated** pour avoir les couleurs.

1. **Composants :** Le projet comprend les scripts suivants :
   - `MapGenerator.cs` : Génère la carte de bruit et coordonne la génération de textures et de maillages.
   - `Noise.cs` : Implémente la génération de bruit de Perlin.
   - `MeshGenerator.cs` : Génère les données de maillage à partir de la carte de hauteur.
   - `MapDisplay.cs` : Affiche les textures et maillages dans l'éditeur Unity.
   - `MapGeneratorEditor.cs` : Personnalise l'inspecteur Unity pour faciliter la génération de cartes.

2. **Paramètres :** Personnalisez les paramètres dans l'inspecteur Unity de l'objet contenant le script `MapGenerator.cs`. Vous pouvez ajuster la taille de la carte, la hauteur, les couleurs, etc.

3. **Génération Automatique :** Activez l'option `autoUpdate` pour générer automatiquement la carte à chaque modification des paramètres.

4. **Génération Manuelle :** Cliquez sur le bouton "Generated" dans l'inspecteur ou utilisez le bouton correspondant dans l'éditeur personnalisé.
