using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class RageWave : ISkill
{
    private const int EnemyLayerMask = 7; // Add constants.cs for layers
    private Sprite _rageWaveSprite;

    private readonly LvlCombinator _lvlCombinator;

    public RageWave(Sprite sprite, LvlCombinator combinator)
    {
        _rageWaveSprite = sprite;
        _lvlCombinator = combinator;
        //Загрузка спрайтов + подписка кнопки проверить
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
            if (enemy.TryGetComponent(out IAttackble attackble))
            {
                attackble.Damageable.TakeDamage(AttackType.Range, _lvlCombinator.GetCurrentLvlSpellDamage());
            }
        }
    }
}