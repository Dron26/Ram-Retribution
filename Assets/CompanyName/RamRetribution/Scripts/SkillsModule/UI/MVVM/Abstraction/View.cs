using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using System;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public abstract class View : MonoBehaviour
    {
        protected VIewModel _viewModel;


        public virtual void Init(VIewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.ViewModelSkillsContainer.OnValueChange += PrintActiveSkills;
        }

        protected abstract void PrintActiveSkills(ISkill[] skills);

    }
}
