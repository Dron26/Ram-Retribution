using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Common;
using UnityEditor;

namespace CompanyName.RamRetribution.EditorScripts
{
    [CustomPropertyDrawer(typeof(TilesStorage))]
    public class DictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer
    {
    }
}