using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class RageIncreaser : ISpell
{
    public RageIncreaser(LvlCombinator lvlCombinator, Sprite sprite)
    {
        Image = sprite; 
        _lvlCombinator = lvlCombinator;
    }
    public Sprite Image { get; }

    private LvlCombinator _lvlCombinator;

    public void ActivateSkill()
    {
        Debug.Log("ReageIncreserSpell Activated");
        _lvlCombinator.IncresetRageValueAccumulation(); //”величивает на врем€ увеличение накоплени€ €рости

    }
}
