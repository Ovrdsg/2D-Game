using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SunnyLand
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 51)]
    internal class EnemyData : ScriptableObject
    {
        [Serializable]
        private struct EnemyInfo
        {
            public EnemyType Type;
            public GameObject EnemyPrefab;
        }

        [SerializeField] private List<EnemyInfo> _enemyInfos;

       public GameObject GetEnemy(EnemyType type)
        {
            var enemyInfo = _enemyInfos.FirstOrDefault(info => info.Type == type);
            if (enemyInfo.EnemyPrefab == null)
            {
                throw new InvalidOperationException($"Enemy type {type} not found");
            }

            return enemyInfo.EnemyPrefab;
        }
    }
}
