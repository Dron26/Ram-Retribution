using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Units.Enemies
{
    public class HeavyEnemy : Enemy
    {
        public override CurrencyTypes RewardCurrency => CurrencyTypes.Money;
    }
}