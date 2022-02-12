using System.IO;
using UnityEngine;
using Object = System.Object;


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

        [Space] [Header("FX data paths")] 
        [SerializeField] private string _pickUpFxPath;
        [SerializeField] private string _enemyDeathFxPath;
        [SerializeField] private string _enemyBlowFxPath;



        internal FxView PickUpFX
        {
            get
            {
                var obj = Resources.Load<FxView>(Path.Combine(_datapath, _pickUpFxPath));
                return Instantiate(obj);
            }
        }

        internal FxView EnemyDeathFx
        {
            get
            {
                var obj = Resources.Load<FxView>(Path.Combine(_datapath, _enemyDeathFxPath));
                return Instantiate(obj);
            }
        }
        
        internal EnemyData EnemyData => Resources.Load<EnemyData>(Path.Combine(_datapath, _enemyDataPath));
        internal PlayerData PlayerData => Resources.Load<PlayerData>(Path.Combine(_datapath, _playerDataPath));

        internal PlayerModel Fox => Resources.Load<PlayerModel>(Path.Combine(_datapath, _playerModelPath));

        
    }
}
