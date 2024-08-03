using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM.Abstraction
{
    public abstract class View : MonoBehaviour
    {
        protected VIewModel ViewModel;
        
        public void Start()
        {
            ViewModel = Services.ViewModel;
            
            //_viewModel.ViewModelSkillsContainer.OnValueChange += ShowSkills;
            ShowSkills(ViewModel.ViewModelSkillsContainer.Value);
        }

        protected abstract void ShowSkills(ISkill[] skills);
    }
}
