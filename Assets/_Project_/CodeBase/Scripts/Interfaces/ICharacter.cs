namespace _Project_.CodeBase.Scripts.Interfaces
{
  public interface ICharacter
  {
    public void Attack(ICharacter character);
    public void Heal(int amount);
    public void Flee(); //  убегания персонажа
  }
}