using UnityEditor;

namespace Sorting
{
    public class SortingLayer : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            ReimportSortingLayers();
        }

        static void ReimportSortingLayers()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(SortingLayerScriptableObject)}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                var scriptableObject = AssetDatabase.LoadAssetAtPath(path, typeof(SortingLayerScriptableObject)) as SortingLayerScriptableObject;
                scriptableObject.RestoreSortingLayers();

                AssetDatabase.DeleteAsset(path);
            }
        }
    }
}