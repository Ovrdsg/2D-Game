using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    internal sealed class CameraController: IFixedExecute
    {
        private float _x;
        private float _y;

        private float offSetX = 1f;
        private float offSetY = 1f;

        private int _camSmoothFactor = 3;


        private Transform _playerTransform;
        private Transform _cameraTransform;


        public CameraController(Transform player, Transform camera)
        {
            _playerTransform = player;
            _cameraTransform = camera;
        }

        public void FixedExecute(float deltaTime)
        {
            _x = _playerTransform.position.x + offSetX;
            _y = _playerTransform.position.y + offSetY;

            
               Vector3 smoothPosition = Vector3.Lerp(_cameraTransform.position, new Vector3(_x, _y, _cameraTransform.position.z),
                    _camSmoothFactor * deltaTime);
               _cameraTransform.position = smoothPosition;
        }


    }
}
