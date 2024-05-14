namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IUnit
    {
        public void TakeDamage(int amount);
        public void Flee();
        public void Heal(int amount);
    }
}