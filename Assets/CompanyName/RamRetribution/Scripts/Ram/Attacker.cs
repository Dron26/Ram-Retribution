using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class Attacker : Unit
    {
        private readonly Health _health;
        private readonly int _damage;

        public override void Accept(IRamVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}