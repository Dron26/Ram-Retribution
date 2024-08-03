using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public abstract class VIewModel
    {
        public ReactiveProperty<ISkill[]> ViewModelSkillsContainer = new ReactiveProperty<ISkill[]>();

        private Model _model;

        public void InitViewModel(Model model)
        {
            _model = model;

            _model.ModelSkillsContainer.OnValueChange += OnModelSkillsContainerChanged;
        }

        private void OnModelSkillsContainerChanged(ISkill[] skills)
        {
            ViewModelSkillsContainer.Value = skills;
        }

        public void OnActiveSpellButtonClicked(ISkill skill)
        { 
            Debug.Log($"Button clicked");
            skill.ActivateSkill();
        }
    }
}