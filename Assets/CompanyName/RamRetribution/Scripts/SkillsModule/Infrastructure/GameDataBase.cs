using System;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Infrastructure
{

    //[CreateAssetMenu(fileName = "GameDataBase", menuName = "MainData")]
    public class GameDataBase /*: ScriptableObject*/
    {
        [Range(0, 10000)] public float DamageKooficient = 1;

        public Transform GetLiderRamTransform()
        {
            //return liderRamTransform
            throw new NotImplementedException();
        }
    }

}