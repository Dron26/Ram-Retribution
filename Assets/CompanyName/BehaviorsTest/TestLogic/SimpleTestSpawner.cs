using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using UnityEngine;

namespace CompanyName.BehaviorsTest.TestLogic
{
    public class SimpleTestSpawner : MonoBehaviour
    {
        public LightEnemy EnemyPrefab;
        public List<Transform> Points;
        
        private void Start()
        {
            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            var enemy = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity);
            var behaviorTree = enemy.GetComponent<BehaviorTree>();
            
            behaviorTree.SetVariableValue("Waypoints", Points);
        
            behaviorTree.EnableBehavior();
        }
    }
}