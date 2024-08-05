using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class MidasHand : ISpell
{
    private LvlCombinator _lvlCombinator;

    public MidasHand(LvlCombinator lvlCombinator, Sprite sprite)
    {
        _lvlCombinator = lvlCombinator;
        Image = sprite;
    }
    public Sprite Image { get; }

    public void ActivateSkill()
    {
        Debug.Log("MidasHand spell activated");
        _lvlCombinator.AddGoldFromSpell();
    }

}
