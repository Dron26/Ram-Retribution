using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public abstract class VIewModel
    {
        protected Model _model;

        public ReactiveProperty<ISkill[]> ViewModelSkillsContainer = new ReactiveProperty<ISkill[]>();

        public Button.ButtonClickedEvent OnActivateSpellButtoneClicked { get; internal set; }

        public virtual void InitViewModel(Model model)
        {
            _model = model;

            _model.ModelSkillsContainer.OnValueChange += OnModelSkillsContainerChanged;
        }

        private void OnModelSkillsContainerChanged(ISkill[] skills)
        {
            ViewModelSkillsContainer.Value = skills;
        }

        public void OnActiveSpellButt0oneClicked(ISkill skill) => skill.ActivateSkill();

    }
}