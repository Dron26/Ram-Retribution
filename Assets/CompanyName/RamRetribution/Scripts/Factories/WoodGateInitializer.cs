using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories
{
    public class WoodGateInitializer : IGateInitializer
    {
        private readonly LevelCombinator _levelCombinator;

        public WoodGateInitializer(LevelCombinator levelCombinator) 
            => _levelCombinator = levelCombinator;
        
        public void Init(Gate instance)
        {
            var type = GateTypes.Wood;
            
            var health = new Health(
                _levelCombinator.GetGateHealth(type),
                new MediumArmor(
                    _levelCombinator.GetGateArmor(type)));

            instance.Init(health, type);

            Debug.Log($"WoodGates health {health.CurrentValue} armor: {health.ArmorValue}");
        }
    }
}