using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Wave config")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private List<WaveUnit> waveConfig;
        [SerializeField] private List<GameObject> boosters;
        [SerializeField] private bool isBoosterMandatory;
        [SerializeField] private int boostersDropAmount = 1;

        public int GetAllEnemiesCount()
        {
            return waveConfig.Aggregate(0, (acc, waveUnit) => acc + waveUnit.GetNumberOfEnemies());
        }

        public List<WaveUnit> GetWaveConfig()
        {
            return waveConfig;
        }

        public GameObject GetRandomBooster()
        {
            return boosters[Random.Range(0, boosters.Count)];
        }
        
        public bool GetIsBoosterMandatory()
        {
            return isBoosterMandatory;
        }
        
        public int GetBoostersDropAmount()
        {
            return boostersDropAmount;
        }
    }
}