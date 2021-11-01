using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Wave unit config")]
    public class WaveUnit : ScriptableObject
    {
        [Header(header: "Prefabs")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject pathPrefab;
        
        [Header(header: "Spawn config")]
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float randomFactor = 0.3f;
        [SerializeField] private int numberOfEnemies = 5;
        [SerializeField] private float speed = 2f;
        [SerializeField] private int enemyScore = 10;

        public GameObject GetEnemyPrefab()
        {
            return enemyPrefab;
        }

        public List<Transform> GetPoints()
        {
            List<Transform> points = new List<Transform>();
            foreach (Transform point in pathPrefab.transform)
            {
                points.Add(point);
            }

            return points;
        }

        public float GetTimeBetweenSpawns()
        {
            return timeBetweenSpawns;
        }

        public float GetRandomFactor()
        {
            return randomFactor;
        }

        public int GetNumberOfEnemies()
        {
            return numberOfEnemies;
        }

        public float GetSpeed()
        {
            return speed;
        }
        
        public int GetEnemyScore()
        {
            return enemyScore;
        }
    }
}