using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class DeacreseDamage : ISpell
{
    private const int EnemyLayerMask = 7; // Add constants.cs for layers

    private readonly LvlCombinator _lvlCombinator;
    public DeacreseDamage(LvlCombinator lvlCombinator, Sprite spellImage)
    {
        _lvlCombinator = lvlCombinator;
        Image = spellImage;
    }

    public Sprite Image { get; }

    public void ActivateSkill()
    {
        Debug.Log("Deacrese damage SpellActivated");
        var leaderTransform = Services.LeaderTransform;
        var results = new Collider[9];
        Physics.OverlapSphereNonAlloc(leaderTransform.position, 10, results, 1 << EnemyLayerMask);
        //Add particles and sound

        foreach (var enemy in results)
        {
            if (enemy.TryGetComponent(out IAttackble attackble)) // IAttackble подойдет?
            {
                //Ќадо найти класс или интрефейс через который можно на врем€ уменьшить атаку у врагов
            }
        }
    }
}
