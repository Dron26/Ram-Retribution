using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class RamSpawnerSkill : ISpell
    {
        private readonly LevelCombinator _levelCombinator;
        private readonly UnitSpawner _spawner;

        public RamSpawnerSkill(LevelCombinator levelCombinator, UnitSpawner unitSpawner, Sprite spellImage)
        {
            _levelCombinator = levelCombinator;
            _spawner = unitSpawner;
            Image = spellImage;
        }
    
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            var spawnRamsCount = _levelCombinator.GetSpawnRamsCountValue();
            // _spawner.SpawnEnemies();   Надо заспавнить новых баранов при активации скила.Я посмотрел UnitSpawner отдельно 1 барана не может так сделать
        }
    }
}
