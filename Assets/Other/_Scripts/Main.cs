using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerMVC.Configs;
using PlatformerMVC.Controllers;
using PlatformerMVC.View;
using UnityEngine;


namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {

        [SerializeField] private List<CoinView> _coinViews;
        #region Configs
        private SpriteAnimatorConfig _playerAnimatorConfig;
        private SpriteAnimatorConfig _coinAnimatorConfig;
        

        #endregion


        #region Views
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CanonView _canonView;
        #endregion



        #region Controllers
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _coinAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CanonAimController _canonAimController;
        private BulletEmitterController _bulletEmitterController; //The intialization of BulletController we make here
        private CoinsController _coinsController;
        #endregion

        
        private void Start()
        {
            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            if (_playerAnimatorConfig) _playerAnimator = new SpriteAnimatorController(_playerAnimatorConfig);
            
            _coinAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("CoinAnimConfiguration");
            if (_coinAnimatorConfig) _coinAnimator = new SpriteAnimatorController(_coinAnimatorConfig);
            
           
            

            _cameraController = new CameraController(_playerView._Transform, Camera.main.transform);
            _playerController = new PlayerController(_playerView, _playerAnimator);

            _canonAimController = new CanonAimController(_canonView._muzzleTransform, _playerView._Transform);
            _bulletEmitterController = new BulletEmitterController(_canonView._bullets, _canonView._emitterTransform);
            
            
            _coinsController = new CoinsController(_playerView, _coinAnimator, _coinViews);
        }

        private void LateUpdate()
        {
            _playerController.Update();
            _cameraController.Update();
            _canonAimController.Update();
            _bulletEmitterController.Update();
            _coinAnimator.Update();
        }
    }
}

