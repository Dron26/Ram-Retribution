using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public abstract class Model
    {
        private ISkill[] ActiveSkills;

        /// <summary>
        /// Skills  that player choosed in Shop
        /// </summary>
        /// <param name="skills"></param>
        public void SetSkills(ISkill[] skills)
        {

            ActiveSkills = skills;
            ModelSkillsContainer.Value = skills;
        }

        public ReactiveProperty<ISkill[]> ModelSkillsContainer = new ReactiveProperty<ISkill[]>();
    }
}
