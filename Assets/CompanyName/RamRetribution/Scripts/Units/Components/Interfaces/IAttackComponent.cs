namespace CompanyName.RamRetribution.Scripts.Units.Components.Interfaces
{
    public interface IAttackComponent : IImprovable
    {
        public ref float AttackSpeed { get; }
        public ref float Damage { get; }
        public float Distance { get; }
        public void Attack(IAttackble entity);
    }
}