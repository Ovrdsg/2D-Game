using UnityEngine;

namespace SunnyLand
{
    public class StunCoolDown : IExecute
    {
        private float _firstStun;
        private readonly float _timeToRelease;
        private bool _canMove;
        private PlayerView _playerView;
        
        internal float FirstStun => _firstStun;
        internal float TimeToRelease => _timeToRelease;
        internal bool CanMove => _canMove;
        
        internal StunCoolDown(PlayerMoveConfig playerMoveConfig, PlayerView playerView)
        {
            _timeToRelease = playerMoveConfig.TimeToReleaseStun;
            _playerView = playerView;
            _firstStun = _timeToRelease;
            _canMove = true;
        }
        
        public void Execute(float deltatime)
        {
            _firstStun += deltatime;
            if (_firstStun >= _timeToRelease)
            {
                _canMove = true;
            }
        }
        
        internal void ResetFirstStun()
        {
            _firstStun = 0;
            _canMove = false;
        }
    }
}