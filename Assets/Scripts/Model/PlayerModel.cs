
using UnityEngine;


namespace SunnyLand
{
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Model/Player", order = 51)]
    internal class PlayerModel : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private int hitPoints;

        internal float Speed => _speed;
        internal int HitPoints => hitPoints;
    }
}
