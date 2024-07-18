using CompanyName.RamRetribution.Scripts.Common.Enums;
using Generator.Scripts;
using Generator.Scripts.Common.Enums;
using UnityEditor;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.Level
{
    [CustomEditor(typeof(GridData))]
    public class GridDataEditor : Editor
    {
        SerializedProperty tilesProperty;

        private void OnEnable()
        {
            tilesProperty = serializedObject.FindProperty("Tiles");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GridData gridData = (GridData)target;
            int gridSizeX = 5;  
            int gridSizeY = 8;  
            EditorGUILayout.BeginVertical();

            for (int y = 0; y < gridSizeY; y++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int x = 0; x < gridSizeX; x++)
                {
                    int index = y * gridSizeX + x;
                    SerializedProperty tileProperty = tilesProperty.GetArrayElementAtIndex(index);
                    tileProperty.enumValueIndex = (int)(TileType)EditorGUILayout.EnumPopup((TileType)tileProperty.enumValueIndex);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(gridData);
                AssetDatabase.SaveAssets();
            }
        }
    }
}