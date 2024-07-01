using System;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "ConfigsStorage", order = 51)]
    public class ConfigsContainer : ScriptableObject
    {
        [SerializeField] private List<UnitConfig> _unitConfigs;

        public string GetId(UnitConfig config)
        {
            if (_unitConfigs.Contains(config))
                return config.Id;

            throw new ArgumentException($"Cannot found configuration {config.name}");
        }
    
        public UnitConfig GetConfig(string id)
        {
            for (int i = 0; i < _unitConfigs.Count; i++)
            {
                if (_unitConfigs[i].Id == id)
                    return _unitConfigs[i];
            }

            throw new ArgumentException($"There is no unit config with id: {id}");
        }
    }
}