using System;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Infrastructure
{
    [CreateAssetMenu(fileName = "GameDataBase", menuName = "MainData")]
    public class GameDataBase : ScriptableObject
    {
        [Range(0, 10000)] public float DamageKooficient = 1;
        internal int HealingKooficient;



        //GoldSpell
        public int GoldSpellLvl = 0;
        public int GoldSpellValue = 200;
        public int Gold;
        internal int RageAccumulationKooficient;
    }
}