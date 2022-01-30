using UnityEngine;

namespace SunnyLand
{
    [CreateAssetMenu(fileName = "EnemyModel", menuName = "Model/Enemy", order = 51)]
    internal class EnemyModel : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _hitPoints;
        [SerializeField] private int _damage;

        internal float Speed => _speed;
        internal int HitPoints => _hitPoints;
        internal int Damage => _damage;

    }
}
