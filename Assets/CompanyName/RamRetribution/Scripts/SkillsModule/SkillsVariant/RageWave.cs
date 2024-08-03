using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class RageWave : ISkill
{
    private const int EnemyLayerMask = 7; // Add constants.cs for layers

    private readonly LvlCombinator _lvlCombinator;

    public RageWave(Sprite sprite, LvlCombinator combinator)
    {
        Image = sprite;
        _lvlCombinator = combinator;
    }

    public Sprite Image { get; }

    public void ActivateSkill()
    {
        Debug.Log("SpellActivated");
        var leaderTransform = Services.LeaderTransform;
        var results = new Collider[9];
        var size = Physics.OverlapSphereNonAlloc(leaderTransform.position, 10, results, 1 << EnemyLayerMask);
        //Add particles and sound
        foreach (var enemy in results)
        {
            if (enemy.TryGetComponent(out IAttackble attackble))
            {
                attackble.Damageable.TakeDamage(AttackType.Range, _lvlCombinator.GetCurrentLvlSpellDamage());
            }
        }
    }
}