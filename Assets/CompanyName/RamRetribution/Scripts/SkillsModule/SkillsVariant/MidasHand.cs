using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class MidasHand : ISpell
    {
        private readonly Wallet _wallet;
        private readonly LvlCombinator _lvlCombinator;

        public MidasHand(LvlCombinator lvlCombinator, Sprite sprite)
        {
            _lvlCombinator = lvlCombinator;
            Image = sprite;
        }
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log("MidasHand spell activated");
            _lvlCombinator.AddGold();
        }
    }
}