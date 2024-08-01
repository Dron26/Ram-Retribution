using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.Skills.MVVM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.UI
{

    public class UiDataBinding
    {
        private Model _uiModel;


        public UiDataBinding(Model uiModel)
        {
            _uiModel = uiModel;
        }
        public void SetNewDataForGame(params ISkill[] skills)
        {
            _uiModel.SetSkills(skills);
        }
    }

}
