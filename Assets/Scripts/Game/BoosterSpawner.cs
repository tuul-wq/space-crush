using ScriptableObjects;
using UnityEngine;

namespace Game
{
    public class BoosterSpawner : MonoBehaviour
    {
        private WaveConfig _currentWave;
        private int _boostersDropAmount;
        
        private void Start()
        {
            var enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.DeathAction += OnEnemyDeath;
        }

        private void OnEnemyDeath(WaveConfig wave, int activeEnemiesCount, Vector3 deathPosition)
        {
            if (!_currentWave || _currentWave != wave)
            {
                _currentWave = wave;
                _boostersDropAmount = wave.GetBoostersDropAmount();
            }
            else if (_boostersDropAmount == 0)
            {
                return;
            }

            var boosterChance = wave.GetIsBoosterMandatory()
                ? _boostersDropAmount / (float) activeEnemiesCount
                : _boostersDropAmount / (float) wave.GetAllEnemiesCount();

            if (Random.Range(0f, 1f) <= boosterChance)
            {
                _boostersDropAmount--;
                Instantiate(wave.GetRandomBooster(), deathPosition, Quaternion.identity);
            }
        }
    }
}