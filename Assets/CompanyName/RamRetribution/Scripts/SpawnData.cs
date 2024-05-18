using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]
    public class SpawnData
    {
        public float SpawnTime; // Время между спаунами врагов
        public int EnemiesToSpawn; // Количество врагов для спавна
        public Transform[] SpawnPoints; // Точки спавна врагов

        public SpawnData(float spawnTime, int enemiesToSpawn, Transform[] spawnPoints)
        {
            SpawnTime = spawnTime;
            EnemiesToSpawn = enemiesToSpawn;
            SpawnPoints = spawnPoints;
        }
    }
}