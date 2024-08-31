using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Units.Enemies
{
    public class MediumEnemy : Enemy
    {
        public override CurrencyTypes RewardCurrency => CurrencyTypes.Money;
    }
}