using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "SpellsContainer", order = 51)]
    public class SpellsContainer : ScriptableObject
    {
        [SerializeField] private List<SpellData> _spells;
    
        public SpellData Get(SpellsId id)
        {
            for (int i = 0; i < _spells.Count; i++)
            {
                if (_spells[i].Id == id)
                    return _spells[i];
            }

            throw new ArgumentException($"There is no unit config with id: {id}");
        }
    }
}