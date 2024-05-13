using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class Support : Unit, IAbilityCaster
    {
        private readonly Health _health;
        private readonly int _damage;

        public void UseAbility()
        {
        
        }

        public override void Accept(IRamVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}