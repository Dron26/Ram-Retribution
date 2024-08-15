using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common
{
    public static class AIAnimatorParams
    {
        public const int RamsAttackAnimationCount = 2;
        public const int EnemyAttackAnimationCount = 1;
        public static readonly int Run = Animator.StringToHash("Run");
        public static readonly int Attack = Animator.StringToHash("Attack");
    }
}