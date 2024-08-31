using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories
{
    public class RockGateInitializer : IGateInitializer
    {
        private readonly LevelCombinator _levelCombinator;

        public RockGateInitializer(LevelCombinator levelCombinator) 
            => _levelCombinator = levelCombinator;
        
        public void Init(Gate instance)
        {
            var type = GateTypes.Rock;
            
            var health = new Health(
                _levelCombinator.GetGateHealth(type),
                new MediumArmor(
                    _levelCombinator.GetGateArmor(type)));

            instance.Init(health, type);

            Debug.Log($"RockGates health {health.CurrentValue} armor: {health.ArmorValue}");
        }
    }
}