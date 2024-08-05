using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class IncreseDamage : ISpell
{
    private LvlCombinator _lvlCombinator;
    private int _baseIncreseDamageValue;

    private const int FriendlyLayerMask = 8; // Add constants.cs for layers

    public IncreseDamage(LvlCombinator lvlCombinator, Sprite image)
    {
        _lvlCombinator = lvlCombinator;
        Image = image;
        _baseIncreseDamageValue *= _lvlCombinator.GetIncreseDamageKooficient();

    }
    public Sprite Image { get; }

    public void ActivateSkill()
    {
        Debug.Log(" IncreseDamage SpellActivated");
        var leaderTransform = Services.LeaderTransform;
        var results = new Collider[9];
        Physics.OverlapSphereNonAlloc(leaderTransform.position, 10, results, 1 << FriendlyLayerMask);
        //Add particles and sound

        foreach (var enemy in results)
        {
            if (enemy.TryGetComponent(out IRam ram))
            {
                //ram.IncreseDamageValue; (Надо найти интерфейс или класс, через который можно на время увеличить урон Unit(Баранам)
            }
        }
    }
}
