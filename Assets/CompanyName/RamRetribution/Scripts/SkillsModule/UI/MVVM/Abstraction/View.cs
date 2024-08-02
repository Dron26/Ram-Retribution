using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using System;
using CompanyName.RamRetribution.Scripts.Common.Services;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public abstract class View : MonoBehaviour
    {
        protected VIewModel _viewModel;
        
        public void Start()
        {
            _viewModel = Services.VIewModel;
            
            //_viewModel.ViewModelSkillsContainer.OnValueChange += ShowSkills;
            ShowSkills(_viewModel.ViewModelSkillsContainer.Value);
        }

        protected abstract void ShowSkills(ISkill[] skills);
    }
}
