using System.Collections.Generic;
using _Project_.CodeBase.Scripts.Enemyes;
using UnityEngine;

namespace _Project_.CodeBase.Scripts
{
    public class SpawnData
    {
        public float SpawnTime; // Время между спаунами врагов
        public int EnemiesToSpawn; // Количество врагов для спавна
        public Dictionary<List<int>, List<Enemy>> EnemyGroup;
        public Transform[] SpawnPoints; // Точки спавна врагов

        public SpawnData(float spawnTime, int enemiesToSpawn, Transform[] spawnPoints)
        {
            SpawnTime = spawnTime;
            EnemiesToSpawn = enemiesToSpawn;
            SpawnPoints = spawnPoints;
        }
    }
}