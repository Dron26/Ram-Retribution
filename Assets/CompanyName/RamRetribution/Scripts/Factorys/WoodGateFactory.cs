using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Factorys.Interfaces;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class WoodGateFactory : IGateFactory
    {
        public Gate Create(Gate instance, bool isLeft)
        {
            IDamageable health = CreateHealth(1000, 50);
            instance.Init(health, isLeft);
            
            return instance;
        }

        private IDamageable CreateHealth(float value, int armorValue)
        {
            return new Health(value, new LightArmor(armorValue));
        }
    }
}