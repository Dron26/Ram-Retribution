using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "UnitsDto", order = 51)]
    public class UnitDTO : ScriptableObject
    {
        [SerializeField] private int _health;
        
        public UnitTypes Type { get; private set; }
        public int Health => _health;
    }
}