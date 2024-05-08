using UnityEngine;

public interface ICharacter
{
    void Move(Vector3 destination); 
    void Attack(ICharacter character);
    void UseAbility();
    void TakeDamage(int damage); 
    void Heal(int amount);
    void Flee(); //  убегания персонажа
}

public enum AllyType
{
    Support,
    Attacker,
    Scout,
    Tank,
    Demolisher
}