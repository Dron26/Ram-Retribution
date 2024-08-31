namespace CompanyName.RamRetribution.Scripts.Units.Components.Interfaces
{
    public interface IArmor
    {
        public ref float Value { get; }
        public int ReduceDamage(IAttackComponent attackComponent, float damage);
    }
}