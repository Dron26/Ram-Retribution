using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class WoodGateFactory : IGateFactory
    {
        private readonly LvlCombinator _lvlCombinator;

        public WoodGateFactory(LvlCombinator lvlCombinator) 
            => _lvlCombinator = lvlCombinator;
        
        public Gate Create(Gate instance)
        {
            IDamageable health = new Health(
                _lvlCombinator.GetGateHealth(GateTypes.Wood),
                new MediumArmor(
                    _lvlCombinator.GetGateArmor(GateTypes.Wood)));

            instance.Init(health);

            Debug.Log($"WoodGates health {health.CurrentValue} armor: {health.ArmorValue}");
            
            return instance;
        }
    }
}