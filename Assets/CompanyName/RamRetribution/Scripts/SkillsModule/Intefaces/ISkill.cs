using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Intefaces
{
    public interface ISkill
    {
        public Sprite SkillImage { get; }
        public void ActivateSkill();
    }
}