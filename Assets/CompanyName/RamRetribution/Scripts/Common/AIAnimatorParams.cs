using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common
{
    public static class AIAnimatorParams
    {
        public static readonly int Run = Animator.StringToHash(nameof(Run));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}