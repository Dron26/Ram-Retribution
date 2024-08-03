using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.Skills.MVVM;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM.Abstraction
{
    public abstract class VIewModel
    {
        public readonly ReactiveProperty<ISpell[]> ViewModelSkillsContainer = new ReactiveProperty<ISpell[]>();

        private Model _model;

        protected void InitViewModel(Model model)
        {
            _model = model;

            _model.ModelSkillsContainer.OnValueChange += OnModelSkillsContainerChanged;
        }

        private void OnModelSkillsContainerChanged(ISpell[] skills)
        {
            ViewModelSkillsContainer.Value = skills;
        }

        public void OnActiveSpellButtonClicked(ISpell spell)
        { 
            spell.ActivateSkill();
        }
    }
}