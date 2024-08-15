using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class RamSpawnerSkill : ISpell
    {
        private readonly LvlCombinator _lvlCombinator;
        private readonly UnitSpawner _spawner;

        public RamSpawnerSkill(LvlCombinator lvlCombinator, UnitSpawner unitSpawner, Sprite spellImage)
        {
            _lvlCombinator = lvlCombinator;
            _spawner = unitSpawner;
            Image = spellImage;
        }
    
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            var spawnRamsCount = _lvlCombinator.GetSpawnRamsCountValue();
            // _spawner.SpawnEnemies();   Надо заспавнить новых баранов при активации скила.Я посмотрел UnitSpawner отдельно 1 барана не может так сделать
        }
    }
}
