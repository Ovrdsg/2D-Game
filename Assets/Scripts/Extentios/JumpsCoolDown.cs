namespace SunnyLand
{
    internal sealed class JumpsCoolDown : IExecute
    {
        private float _firstJump;
        private readonly float _timeToNextJump;
        private bool _canJump;
        
        internal float FirstJump => _firstJump;
        internal float TimeToNextJump => _timeToNextJump;
        internal bool CanJump => _canJump;
        
        internal JumpsCoolDown(PlayerMoveConfig playerMoveConfig)
        {
            _timeToNextJump = playerMoveConfig.TimeToNextJump;
        }
        
        
        public void Execute(float deltatime)
        {
            _firstJump += deltatime;
            if (_firstJump >= _timeToNextJump)
            {
                _canJump = true;
            }
        }

        internal void ResetFirstJump()
        {
            _firstJump = 0;
            _canJump = false;
        }
    }
}