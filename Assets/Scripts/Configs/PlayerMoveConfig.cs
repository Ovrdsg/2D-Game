using UnityEngine;

namespace SunnyLand
{
    [CreateAssetMenu(fileName = "PlayerMoveConfig", menuName = "Configs/PlayerMoveConfig", order = 1)]
    public class PlayerMoveConfig : ScriptableObject
    {
        [SerializeField][Range(1f, 1000f)] private float _speed;
        [SerializeField][Range(1f, 1000f)] private float _jumpForce;
        [SerializeField][Range(0.1f, 1000f)] private float _movingThreshHold;
        [SerializeField][Range(0.1f, 1f)] private float _jumpThreshHold;
        [SerializeField][Range(-10f, 10f)] private float _fallThreshHold;
        [SerializeField] [Range(0.1f, 5f)] private float _timeToNextJump;

        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public float MovingThreshHold => _movingThreshHold;
        public float JumpThreshHold => _jumpThreshHold;
        public float FallThreshHold => _fallThreshHold;

        public float TimeToNextJump => _timeToNextJump;
    }
}