using System;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class SpellsFactory
    {
        private readonly SpellsContainer _spellsContainer;

        public SpellsFactory(SpellsContainer spellsContainer)
            => _spellsContainer = spellsContainer;
        
        public ISpell Create(SpellsId id)
        {
            var spellData = _spellsContainer.Get(id);

            return spellData.Id switch
            {
                SpellsId.DecreaseDamage => null,
                SpellsId.GateDemolish => new GateDemolish(spellData.Value, spellData.Image, Services.LvlCombinator),
                SpellsId.HealWave => null,
                SpellsId.IncreaseDamage => null,
                SpellsId.RageWave => new RageWave(spellData.Value, spellData.Image, Services.LvlCombinator),
                SpellsId.MidasHand => null,
                SpellsId.RageIncrease => null,
                SpellsId.RamSpawnSkill => null,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}