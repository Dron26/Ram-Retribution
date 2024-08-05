using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

public class RamSpawnerSkill : ISpell
{
    private LvlCombinator _lvlCombinator;
    private UnitSpawner _spawner;

    public RamSpawnerSkill(LvlCombinator lvlCombinator, UnitSpawner unitSpawner, Sprite SpellImage)
    {
        _lvlCombinator = lvlCombinator;
        _spawner = unitSpawner;
        Image = SpellImage;
    }
    public Sprite Image { get; }

    public void ActivateSkill()
    {
        int spawnRamsCount = _lvlCombinator.GetSpawnRamsCountValue();
       // _spawner.SpawnEnemies();   ���� ���������� ����� ������� ��� ��������� �����.� ��������� UnitSpawner �������� 1 ������ �� ����� ��� �������
    }
}
