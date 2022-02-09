using UnityEngine;

namespace SunnyLand
{
    internal sealed class InputController : IExecute
    {
        private readonly IUserInputProxy _horizontal;
        private readonly IUserInputProxy _vertical;
        private PlayerController _playerController;

        internal InputController(IUserInputProxy horizontalInput, IUserInputProxy verticalInput)
        {
            _horizontal = horizontalInput;
            _vertical = verticalInput;
        }

        public void Execute(float time)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
        }
    }
}