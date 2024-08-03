using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Intefaces
{
    public interface ISkill
    {
        public Sprite Image { get; }
        public void ActivateSkill();
    }
}