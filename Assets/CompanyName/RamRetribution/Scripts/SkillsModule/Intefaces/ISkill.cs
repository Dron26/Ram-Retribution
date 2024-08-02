using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Intefaces
{
    public interface ISkill
    {
        Sprite SkillImage { get; }
        void ActivateSkill();
    }
}
