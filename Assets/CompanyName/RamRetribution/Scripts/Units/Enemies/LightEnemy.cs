using BehaviorDesigner.Runtime;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Units.Enemies
{
    public class LightEnemy : Enemy
    {
        public override CurrencyTypes RewardCurrency => CurrencyTypes.Money;
    }
}