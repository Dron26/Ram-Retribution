using Generator.Scripts.Level;
using UnityEditor;
using UnityEngine;

namespace Generator.Scripts
{
    [CustomEditor(typeof(LevelConfigurator))]
    public class GeneratorButton : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelConfigurator generator = (LevelConfigurator)target;

            if (GUILayout.Button("Generate"))
            {
                for (int i = generator.transform.childCount - 1; i >= 0; i--)
                    DestroyImmediate(generator.transform.GetChild(i).gameObject);
                generator.GenerateGridAsync();
            }

            if (GUILayout.Button("Clear "))
            {
                for (int i = generator.transform.childCount - 1; i >= 0; i--)
                    DestroyImmediate(generator.transform.GetChild(i).gameObject);
            }
        }
    }
}