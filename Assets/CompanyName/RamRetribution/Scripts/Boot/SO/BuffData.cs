using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "Buffs", fileName = "BuffData")]
    public class BuffData : ScriptableObject
    {
        public ConfigId UnitId;
        public ImprovableUnitFields ImprovableField;
        public float BonusValue;
    }
}
