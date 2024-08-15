using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(fileName = "GameDataBase", menuName = "MainData")]
    public class GameDataBase : ScriptableObject
    {
        [Range(0, 10000)] public float DamageBonus = 1;
        public int HealthBonus;
        
        public int Gold;
        public int RageAccumulationBonus;

        public CurveStructure UnitHealthCurve;
        public CurveStructure UnitDamageCurve;

        //GoldSpell
        [field: SerializeField] public int GoldSpellValue { get; private set; } = 200;
    }
}