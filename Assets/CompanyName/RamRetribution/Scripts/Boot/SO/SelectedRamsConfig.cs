using System;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "SelectedRamsCfg")]
    public class SelectedRamsConfig : ScriptableObject
    {
        [SerializeField] private List<UnitConfig> _configs;
        [SerializeField] private int _maxCount;

        public List<UnitConfig> Configs => _configs;

        public void OnValidate()
        {
            if (_configs.Count > _maxCount)
                _configs.RemoveRange(_maxCount, _configs.Count - _maxCount);
        }

        public void Add(UnitConfig ramConfig)
        {
            _configs ??= new List<UnitConfig>();

            _configs.Add(ramConfig);
        }

        public void Remove(UnitConfig ramConfig)
        {
            if (_configs.Contains(ramConfig))
                _configs.Remove(ramConfig);
        }
    }
}