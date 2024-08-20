using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(fileName = "GameDataBase", menuName = "MainData")]
    public class GameDataBase : ScriptableObject
    {
        [Range(0, 100)] public float DamageBonusPerLevel = 10;
        [FormerlySerializedAs("HealthBonus")] public int HealthBonusPerLevel;
        
        public int RageAccumulationBonus;
        public int BaseGoldPerUnit;
        public int BaseGoldPerGate;

        public CurveStructure UnitHealthCurve;
        public CurveStructure UnitDamageCurve;
        public CurveStructure GoldPerUnitCurve;

        //GoldSpell
        [field: SerializeField] public int GoldSpellValue { get; private set; } = 200;
    }
}