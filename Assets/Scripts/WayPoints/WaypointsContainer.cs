using System;
using Pathfinding;
using UnityEngine;

namespace SunnyLand
{
    internal sealed class WaypointsContainer: MonoBehaviour
    {
        [SerializeField] private AIDestinationSetter _aiDSetter;
        [SerializeField] private Transform[] _wayPoints;
        
        private int _currentWpIndex = -1;

        
        internal Transform GetNextTarget()
        {
            _currentWpIndex = (_currentWpIndex + 1) % _wayPoints.Length;
            print(_wayPoints.Length);
            return _wayPoints[_currentWpIndex];
        }

        private void Start()
        {
            _aiDSetter.target = GetNextTarget();
        }

        private void Update()
        {
            if (transform.position == _aiDSetter.target.position)
            {
                _aiDSetter.target = GetNextTarget();
            }
        }
    }
}