using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using Generator.Scripts;
using Generator.Scripts.Common.Enums;
using UnityEditor;
using UnityEngine;

namespace CompanyName.RamRetribution.EditorScripts
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GridData))]
    public class GridDataEditor : Editor
    {
        private SerializedProperty _tilesProperty;

        private void OnEnable()
        {
            _tilesProperty = serializedObject.FindProperty("Tiles");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GridData gridData = (GridData)target;
            int gridSizeX = 6;
            int gridSizeY = 6;
            EditorGUILayout.BeginVertical();

            for (int x = 0; x < gridSizeX; x++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int y = 0; y < gridSizeY; y++)
                {
                    int index = x * gridSizeY + y;
                    SerializedProperty tileProperty = _tilesProperty.GetArrayElementAtIndex(index);
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
#endif
}