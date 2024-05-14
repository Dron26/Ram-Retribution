using _Project_.CodeBase.Scripts.Common.Enums;
namespace _Project_.CodeBase.Scripts
{
    public class GateData
    {
        public int GateLevel;
        public int MaxHealth;
        public int StrengthIndex;
        public GateType Material;
        public int ReductionModifier;
    
        public GateData(int gateLevel, int maxHealth,GateType material, int reductionModifier, int strengthIndex)
        {
            GateLevel = gateLevel;
            StrengthIndex = strengthIndex;
            ReductionModifier = reductionModifier;
            MaxHealth = maxHealth;
            Material = material;
        }
    }
}