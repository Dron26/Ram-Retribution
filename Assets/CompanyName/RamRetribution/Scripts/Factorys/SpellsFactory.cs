using System;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;

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
            ISpell spell = null;
            
            switch (spellData.Id)
            {
                case SpellsId.Fire:
                    break;
                case SpellsId.Stomp:
                    break;
                case SpellsId.Defence:
                    break;
                case SpellsId.Heal:
                    break;
                case SpellsId.RageWave: 
                    spell = new RageWave(spellData.Image, Services.LvlCombinator);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return spell;
        }
    }
}