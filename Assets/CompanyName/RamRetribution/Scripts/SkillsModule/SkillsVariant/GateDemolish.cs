using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class GateDemolish : ISpell
{
    private LvlCombinator _lvlCombinator;
    private int _baseDemolishSpellDamage = 50;

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
            damagable.Damageable.TakeDamage(CompanyName.RamRetribution.Scripts.Common.Enums.AttackType.Range, _baseDemolishSpellDamage * _lvlCombinator.GetCurrentLvlSpellDamage());



    }
}
