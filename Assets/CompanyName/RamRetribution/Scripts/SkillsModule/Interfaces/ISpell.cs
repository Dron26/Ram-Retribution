using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces
{
    public interface ISpell
    {
        public Sprite Image { get; }
        public void ActivateSkill();
    }
}