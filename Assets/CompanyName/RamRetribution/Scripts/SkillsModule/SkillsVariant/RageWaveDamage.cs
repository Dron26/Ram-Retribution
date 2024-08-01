using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class RageWaveDamage : ISkill
{
    private Sprite _rageWaveSprite;
    private int _enemyLayerMask = 7;

    private LvlCombinator _lvlCombinator;

    public RageWaveDamage()
    {
        Resources.Load("PathToSprite");
        _lvlCombinator = Services.LvlCombinator;
    }

    public Sprite SkillImage => _rageWaveSprite;

    public void ActivateSkill()
    {
        Debug.Log("SpellActivated");
        Transform liderTranform = Services.GameDataBase.GetLiderRamTransform();
        Collider[] enemysColliders = Physics.OverlapSphere(liderTranform.position, 10, 1 << _enemyLayerMask);
        foreach (var enemy in enemysColliders)
        {
            if (enemy.TryGetComponent(out IAttackble damageableComponnent))
            {
                damageableComponnent.Damageable.TakeDamage(CompanyName.RamRetribution.Scripts.Common.Enums.AttackType.Range, _lvlCombinator.GetCurrentLvlSpellDamage());
                //ThrowSpellEffect
            }
        }
    }
}
