using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(fileName = "GameDataBase", menuName = "MainData")]
    public class GameDataBase : ScriptableObject
    {
        [Header("Stats configuration")]
        public float DamageBonusPerLevel = 10;
        public int HealthBonusPerLevel;
        public int RageAccumulationBonus;

        [Header("Gates configuration")]
        public int WoodGateBaseHealth;
        public int RockGateBaseHealth;
        public int WoodGateBaseArmor;
        public int RockGateBaseArmor;
        
        [Header("Gold configuration")]
        public int BaseGoldPerUnit;
        public int BaseGoldPerGate;

        [Header("Unit Curves")]
        public CurveStructure UnitHealthCurve;
        public CurveStructure UnitDamageCurve;
        public CurveStructure GoldPerUnitCurve;

        [Header("Gate Curves")] 
        public CurveStructure GateHealthCurve;
        public CurveStructure GateArmorCurve;
        
        //GoldSpell
        [field: SerializeField] public int GoldSpellValue { get; private set; } = 200;

        public int GetGateHealth(GateTypes type) 
            => type == GateTypes.Wood 
                ? WoodGateBaseHealth 
                : RockGateBaseHealth;

        public int GetGateArmor(GateTypes type) 
            => type == GateTypes.Wood 
                ? WoodGateBaseArmor 
                : RockGateBaseArmor;
    }
}