using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace SunnyLand
{
    public class Main : MonoBehaviour
    {
        public event Action GameInitialized;

        [SerializeField] private List<CoinView> _coinviews;
        [SerializeField] private List<PlayerView> _deathZones;
        [SerializeField] private List<PlayerView> _winZones;
        [SerializeField] private SpriteAnimationsConfig _playerAnimatorConfig;
        [SerializeField] private SpriteAnimationsConfig _coinsAnimatorConfig;
        [SerializeField] private int _animationSpeed;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private CanonView _canonView;

        

        private SpriteAnimator _playerAnimator;
        private SpriteAnimator _coinAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CanonAimController _canonAimController;
        private BulletEmitterController _bulletEmitterController;
        private CoinsController _coinsController;
        private LevelCompleteController _levelCompleteController;
        //The intialization of BulletController we make here




        private void Start()
        {
            GameInitialized += StartGameLoop;

            _playerAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("PlayerAnimatorConfig");
            if (_playerAnimatorConfig)
            {
                _playerAnimator = new SpriteAnimator(_playerAnimatorConfig);
            }
            _coinsAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("CoinAnimConfiguration");
            if (_coinsAnimatorConfig)
            {
                _coinAnimator = new SpriteAnimator(_coinsAnimatorConfig);
            }


            _cameraController = new CameraController(_playerView._Transform, Camera.main.transform);
            _playerController = new PlayerController(_playerView, _playerAnimator);
            

            _canonAimController = new CanonAimController(_canonView._muzzleTransform, _playerView._Transform);
            _bulletEmitterController = new BulletEmitterController(_canonView._bullets, _canonView._emitterTransform);
            _coinsController = new CoinsController(_playerView, _coinAnimator, _coinviews);
            _levelCompleteController = new LevelCompleteController(_playerView, _deathZones, _winZones);


        }
        private void LateUpdate()
        {
            _playerController.Update();
            _cameraController.Update();
            _canonAimController.Update();
            _bulletEmitterController.Update();
            _coinAnimator.Update();
            
        }




        private void StartGameLoop()
        {
            GameInitialized -= StartGameLoop;
        }
    }
}


