using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using Generator.Scripts.Common.Enums;
using SerializableDictionary;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Common
{
    [Serializable]
    public class TilesDictionary : SerializableDictionary<TileType, List<GameObject>, TilesStorage>
    {
    }
}