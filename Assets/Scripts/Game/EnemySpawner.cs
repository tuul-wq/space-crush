using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using ScriptableObjects;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> enemyWaves;

        public event Action<WaveConfig, int, Vector3> DeathAction;
        
        private WaveConfig _activeWave;
        private int _activeEnemiesCount;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return StartCoroutine(SpawnAllWaves());
            }
        }

        private IEnumerator SpawnAllWaves()
        {
            foreach (var wave in enemyWaves)
            {
                _activeWave = wave;
                SpawnWave();
                yield return new WaitUntil(() => _activeEnemiesCount == 0);
            }

            yield return null;
        }

        private void SpawnWave()
        {
            _activeEnemiesCount = _activeWave.GetAllEnemiesCount();

            foreach (var waveUnit in _activeWave.GetWaveConfig())
            {
                StartCoroutine(SpawnWaveEnemies(waveUnit));
            }
        }

        private IEnumerator SpawnWaveEnemies(WaveUnit waveUnit)
        {
            var waypoints = waveUnit.GetPoints();
            var numberOfEnemies = waveUnit.GetNumberOfEnemies();
                    
            for (int enemyIndex = 0; enemyIndex < numberOfEnemies; enemyIndex++)
            {
                CreateNewEnemy(waveUnit, waypoints);
                yield return new WaitForSeconds(waveUnit.GetTimeBetweenSpawns());
            }
        }

        private void CreateNewEnemy(WaveUnit waveUnit, List<Transform> waypoints)
        {
            var enemy = Instantiate(
                waveUnit.GetEnemyPrefab(),
                waypoints[0].transform.position,
                Quaternion.identity
            );
        
            var movementObject = enemy.GetComponent<MovementSystem>();
            movementObject.SetupMovement(waypoints, waveUnit.GetSpeed());
            movementObject.DeathAction += OnEnemyDeath;
        
            var enemyObject = enemy.GetComponent<Enemy>();
            enemyObject.Score = waveUnit.GetEnemyScore();
            enemyObject.DeathAction += NotifySubscribers;
        }

        private void OnEnemyDeath()
        {
            _activeEnemiesCount -= 1;
        }

        private void NotifySubscribers(Vector3 deathPosition)
        {
            DeathAction?.Invoke(_activeWave, _activeEnemiesCount, deathPosition);
            OnEnemyDeath();
        }
    }
}