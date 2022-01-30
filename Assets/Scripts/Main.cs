using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SunnyLand
{
    public class Main : MonoBehaviour
    {
        public event Action GameInitialized;

        [SerializeField] private SpriteAnimationsConfig _playerAnimatorConfig;
        [SerializeField] private int _animationSpeed;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private CanonView _canonView;

        private SpriteAnimator _playerAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CanonAimController _canonAimController;
        private BulletEmitterController _bulletEmitterController; //The intialization of BulletController we make here




        private void Start()
        {
            GameInitialized += StartGameLoop;

            _playerAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("PlayerAnimatorConfig");
            if (_playerAnimatorConfig)
            {
                _playerAnimator = new SpriteAnimator(_playerAnimatorConfig);
            }

            _cameraController = new CameraController(_playerView._Transform, Camera.main.transform);
            _playerController = new PlayerController(_playerView, _playerAnimator);

            _canonAimController = new CanonAimController(_canonView._muzzleTransform, _playerView._Transform);
            _bulletEmitterController = new BulletEmitterController(_canonView._bullets, _canonView._emitterTransform);

        }
        private void LateUpdate()
        {
            _playerController.Update();
            _cameraController.Update();
            _canonAimController.Update();
            _bulletEmitterController.Update();
        }




        private void StartGameLoop()
        {
            GameInitialized -= StartGameLoop;
        }
    }
}


