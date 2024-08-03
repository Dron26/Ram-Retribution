using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Intefaces
{
    public interface ISpell
    {
        public Sprite Image { get; }
        public void ActivateSkill();
    }
}