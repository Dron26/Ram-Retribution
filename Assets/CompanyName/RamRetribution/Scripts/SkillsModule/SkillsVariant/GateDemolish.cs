using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Infrastructure;
using UnityEngine;

public class GateDemolish : ISpell
{
    private readonly LvlCombinator _lvlCombinator;
    private readonly int _baseDemolishSpellDamage = 50;

    public GateDemolish(Sprite image, LvlCombinator lvlCombinator)
    {
        _lvlCombinator = lvlCombinator;
        Image = image;
    }
    
    public Sprite Image { get; }

    public void ActivateSkill()
    {
        Debug.Log(" GateDemolish spell activated");
        /*тут ошибка!!!!*/
        Transform gateTransform = Services.LeaderTransform; //Надо получить ворота со сцены, чтобы нанести урон, Где хранится ссылка на него?
        if (gateTransform.TryGetComponent(out IAttackble damagable))
            damagable.Damageable.TakeDamage(AttackType.Range, _baseDemolishSpellDamage * _lvlCombinator.GetCurrentLvlSpellDamage());
    }
}
