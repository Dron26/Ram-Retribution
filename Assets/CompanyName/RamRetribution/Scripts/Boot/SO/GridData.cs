using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "GridData")]
public class GridData : ScriptableObject
{
    public GridTypes[,] Grids = new GridTypes[6, 6];
}

public enum GridTypes
{
    Any,
    Gate,
    EnemiesSpawner,
    RamsSpawnPoint,
    Road, // else RoadForward,RoadAngleLeft and others
}

[CustomEditor(typeof(GridData))]
public class GridDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gridData = (GridData)target;
        
        for (int i = 0; i < gridData.Grids.GetLength(1); i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < gridData.Grids.GetLength(0); j++)
            {
                gridData.Grids[i, j] = (GridTypes)EditorGUILayout.EnumPopup(gridData.Grids[i, j]);
            }

            EditorGUILayout.EndHorizontal();
        }
        
        EditorUtility.SetDirty(target);
    }
}