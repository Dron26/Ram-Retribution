using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "ConfigsStorage", order = 51)]
    public class ConfigsContainer : ScriptableObject
    {
        [SerializeField] private List<UnitConfig> _unitConfigs;
    
        public UnitConfig Get(ConfigId id)
        {
            foreach (var config in _unitConfigs.Where(config => config.Id == id))
                return config;

            throw new ArgumentException($"There is no unit config with id: {id}");
        }
    }
}
