using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "BuffsStorage", order = 51)]
    public class BuffsContainer : ScriptableObject
    {
        [SerializeField] private List<BuffData> _buffs;
    
        public BuffData Get(ConfigId id)
        {
            for (var i = 0; i < _buffs.Count; i++)
                if (_buffs[i].UnitId == id)
                    return _buffs[i];

            throw new ArgumentException($"There is no unit config with id: {id}");
        }
    }
}