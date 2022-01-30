using System.IO;
using UnityEngine;


namespace SunnyLand
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data Paths", order = 51)]
    public class Data : ScriptableObject
    {
        private const string _datapath = "Data";

        [Header("Datapath")]
        [SerializeField] private string _enemyDataPath;
        [SerializeField] private string _playerDataPath;

        [Space][Header("Models paths")]
        [SerializeField] private string _playerModelPath;

        internal EnemyData EnemyData => Resources.Load<EnemyData>(Path.Combine(_datapath, _enemyDataPath));
        internal PlayerData PlayerData => Resources.Load<PlayerData>(Path.Combine(_datapath, _playerDataPath));

        internal PlayerModel Fox => Resources.Load<PlayerModel>(Path.Combine(_datapath, _playerModelPath));


    }
}
