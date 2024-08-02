using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class RageWaveDamage : ISkill
{
    private const int EnemyLayerMask = 7; // Add constants.cs for layers
    private Sprite _rageWaveSprite;

    private LvlCombinator _lvlCombinator;

    public RageWaveDamage()
    {
        _rageWaveSprite = Services.ResourceLoadService.Load<Sprite>($"{AssetPaths.RandomSpellSprite}{0}");
        _lvlCombinator = Services.LvlCombinator;
    }

    public Sprite SkillImage
        => _rageWaveSprite;

    public void ActivateSkill()
    {
        Debug.Log("SpellActivated");
        Transform liderTranform = Services.LeaderTransform;
        Collider[] enemysColliders = Physics.OverlapSphere(liderTranform.position, 10, 1 << EnemyLayerMask);
        //Add particles and sound
        foreach (var enemy in enemysColliders)
        {
            if (enemy.TryGetComponent(out IAttackble damageableComponnent))
            {
                damageableComponnent.Damageable.TakeDamage(AttackType.Range, _lvlCombinator.GetCurrentLvlSpellDamage());
            }
        }
    }
}