namespace _Project_.CodeBase.Scripts
{
    public class PlayerData
    {
        public int health;
        public int damage;
        public int armor;
        public float attackSpeed;
    
        public PlayerData(int initialHealth, int initialDamage, int initialArmor, float initialAttackSpeed, int initialGold, int initialHorn, int initialProgress)
        {
            health = initialHealth;
            damage = initialDamage;
            armor = initialArmor;
            attackSpeed = initialAttackSpeed;
        }
    }
}