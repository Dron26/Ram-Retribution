using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class HealWave : ISpell
{
    private LvlCombinator _lvlCombinator;
    private const int FriendlyLayerMask = 8; // Add constants.cs for layers
    private int _baseHealingValue;

    public HealWave(LvlCombinator lvlCombinator, Sprite skillImage)
    {
        _lvlCombinator = lvlCombinator;
        Image = skillImage;
        _baseHealingValue *= _lvlCombinator.GetHealingSpellValue();
    }
    public Sprite Image { get; }

    public void ActivateSkill()
    {
        Debug.Log(" Heal SpellActivated");
        var leaderTransform = Services.LeaderTransform;
        var results = new Collider[9];
        Physics.OverlapSphereNonAlloc(leaderTransform.position, 10, results, 1 << FriendlyLayerMask);
        //Add particles and sound

        foreach (var enemy in results)
        {
            if (enemy.TryGetComponent(out IRam ram))
            {

                //ram.Heal(_baseHealingValue);
            }
        }
    }
}
