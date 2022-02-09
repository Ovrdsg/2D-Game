using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace SunnyLand
{
    internal sealed class Main : MonoBehaviour
    {
        public event Action GameInitialized;

        [SerializeField] private Camera _cameraMain;
        [SerializeField] private List<GemView> _gemViews;
        [SerializeField] private List<CherryView> _cherryViews;
        [SerializeField] private List<PlayerView> _deathZones;
        [SerializeField] private List<PlayerView> _winZones;
        [SerializeField] private SpriteAnimationsConfig _playerAnimatorConfig;
        [SerializeField] private SpriteAnimationsConfig _cherryAnimatorConfig;
        [SerializeField] private SpriteAnimationsConfig _gemAnimatorConfig;
        [SerializeField] private PlayerMoveConfig _playerMoveConfig;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private CanonView _canonView;

        

        private SpriteAnimator _playerAnimator;
        private SpriteAnimator _cherryAnimator;
        private SpriteAnimator _gemAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CanonAimController _canonAimController;
        private BulletEmitterController _bulletEmitterController;
        private CoinsController _collectiblesController;
        private LevelCompleteController _levelCompleteController;

        private Controllers _controllers;

        private List<SpriteAnimator> _animators;

        private void Awake()
        {
            _animators = new List<SpriteAnimator>();
        }

        private void Start()
        {
            GameInitialized += StartGameLoop;
            _playerAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("PlayerAnimatorConfig");
            _cherryAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("CherryAnimConfiguration");
            _gemAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("CherryAnimConfiguration");

            if (_playerAnimatorConfig && _cherryAnimatorConfig && _gemAnimatorConfig)
            {
                _controllers = new Controllers();
                
                _playerAnimator = new SpriteAnimator(_playerAnimatorConfig);
                _cherryAnimator = new SpriteAnimator(_cherryAnimatorConfig);
                _gemAnimator = new SpriteAnimator(_gemAnimatorConfig);
                
                _gemViews = FindObjectsOfType<GemView>().ToList();
                _cherryViews = FindObjectsOfType<CherryView>().ToList();

                var contactPooler = new ContactPooler(_playerView._collider2D);
                var pcInputHorizontal = new PCInputHorizontal();
                var pcInputVertical = new PCInputVertical();
                var inputController = new InputController(pcInputHorizontal, pcInputVertical);
                var jumpsCoolDown = new JumpsCoolDown(_playerMoveConfig);
                _cameraController = new CameraController(_playerView.PlayerTransform, _cameraMain.transform);
                _playerController = new PlayerController(_playerView, _playerAnimator, _playerAnimatorConfig, pcInputHorizontal, pcInputVertical, contactPooler, _playerMoveConfig, jumpsCoolDown);
                
                //_canonAimController = new CanonAimController(_canonView._muzzleTransform, _playerView.PlayerTransform);
                //_bulletEmitterController = new BulletEmitterController(_canonView._bullets, _canonView._emitterTransform);
                _collectiblesController = new CoinsController(_cherryAnimatorConfig, _gemAnimatorConfig, _playerView, _gemViews, _cherryViews);
                //_levelCompleteController = new LevelCompleteController(_playerView, _deathZones, _winZones);

                _controllers.Add(_playerController);
                _controllers.Add(_cameraController);
                _controllers.Add(inputController);
                _controllers.Add(_playerAnimator);
                _controllers.Add(contactPooler);
                _controllers.Add(_collectiblesController);
                _controllers.Add(jumpsCoolDown);
            }
            else throw new NullReferenceException("PlayerAnimatorConfig or CollectiblesAnimatorConfig is null. " +
                                                                                            "Please place it in the inspector");
            
        }
        
        
        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
            //_canonAimController.Update();
            //_bulletEmitterController.Update();
            _cherryAnimator.Execute(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.FixedExecute(deltaTime);
        }


        private void StartGameLoop()
        {
            GameInitialized -= StartGameLoop;
        }
    }
}


