using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class MovementSystem : MonoBehaviour
    {
        public event Action DeathAction;

        private List<Transform> _wayPoints;
        private float _speed;
        private int _wayPointIndex;
        
        private void Update()
        {
            Move();
        }
        
        private void Move()
        {
            if (_wayPointIndex != _wayPoints.Count)
            {
                var next = _wayPoints[_wayPointIndex].transform.position;
                var step = Time.deltaTime * _speed;
                transform.position = Vector2.MoveTowards(transform.position, next, step);

                if (transform.position == next)
                {
                    _wayPointIndex += 1;
                }
            }
            else
            {
                OnDeath();
            }
        }

        public void SetupMovement(List<Transform> paths, float enemySpeed)
        {
            _wayPoints = paths;
            _speed = enemySpeed;
            transform.position = _wayPoints[_wayPointIndex].position;
        }
        
        private void OnDeath()
        {
            DeathAction?.Invoke();
            DeathAction = null;
            Destroy(gameObject);
        }
    }
}