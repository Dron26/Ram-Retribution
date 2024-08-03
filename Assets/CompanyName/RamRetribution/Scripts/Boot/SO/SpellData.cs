using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "SpellData", order = 51)]
    public class SpellData : ScriptableObject
    {
        [field: SerializeField] public SpellsId Id { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
    }
}