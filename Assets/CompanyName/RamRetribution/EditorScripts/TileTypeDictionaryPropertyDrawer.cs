using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Common;
using UnityEditor;

namespace CompanyName.RamRetribution.EditorScripts
{
    [CustomPropertyDrawer(typeof(TilesDictionary))]
    public class TileTypeDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}