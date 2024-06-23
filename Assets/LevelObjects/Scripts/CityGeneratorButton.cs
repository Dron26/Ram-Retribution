using UnityEditor;
using UnityEngine;

namespace LevelObjects.Scripts
{
    [CustomEditor(typeof(TileGenerator))]
    public class CityGeneratorButton : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TileGenerator cityGenerator = (TileGenerator)target;

            if (GUILayout.Button("Generate City"))
            {
                for (int i = cityGenerator.transform.childCount - 1; i >= 0; i--)
                    DestroyImmediate(cityGenerator.transform.GetChild(i).gameObject);
                cityGenerator.GenerateGrid();
            }

            if (GUILayout.Button("Clear City"))
            {
                for (int i = cityGenerator.transform.childCount - 1; i >= 0; i--)
                    DestroyImmediate(cityGenerator.transform.GetChild(i).gameObject);
            }
        }
    }
}