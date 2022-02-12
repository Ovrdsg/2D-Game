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
        [SerializeField] private Data _data;
        [SerializeField] private ObjectsAnimationsConfig objectsAnimatorConfig;
        [SerializeField] private PlayerMoveConfig _playerMoveConfig;
        
        [SerializeField] private Camera _cameraMain;
        [SerializeField] private List<PlayerView> _winZones;
        
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private CanonView _canonView;

        

        private SpriteAnimator _playerAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CanonAimController _canonAimController;
        private BulletEmitterController _bulletEmitterController;
        private ObjectsAnimationsController _objectsAnimationsController;
        private LevelCompleteController _levelCompleteController;

        private Controllers _controllers;

        private List<SpriteAnimator> _animators;

        private void Awake()
        {

        }

        private void Start()
        {
            GameInitialized += StartGameLoop;
            objectsAnimatorConfig = Resources.Load<ObjectsAnimationsConfig>("ObjectAnimationConfig");


            if (objectsAnimatorConfig)
            {
                _controllers = new Controllers();
                var fxFactory = new FxFactory(_data);
                
                
                var listAnimatedObjects = FindObjectsOfType<InteractableObject>().ToList();
                var listFxObjects = new List<FxView>();
                _objectsAnimationsController = new ObjectsAnimationsController(_playerView, objectsAnimatorConfig, listAnimatedObjects);
                var fxController = new FxController(_playerView, fxFactory, objectsAnimatorConfig, listFxObjects);
                var contactPooler = new ContactPooler(_playerView.Collider2D);
                var pcInputHorizontal = new PCInputHorizontal();
                var pcInputVertical = new PCInputVertical();
                var inputController = new InputController(pcInputHorizontal, pcInputVertical);
                var jumpsCoolDown = new JumpsCoolDown(_playerMoveConfig);
                var stunCoolDown = new StunCoolDown(_playerMoveConfig, _playerView);
                _cameraController = new CameraController(_playerView.ObjectTransform, _cameraMain.transform);
                _playerController = new PlayerController(_playerView, objectsAnimatorConfig, pcInputHorizontal, pcInputVertical, contactPooler, _playerMoveConfig, jumpsCoolDown, stunCoolDown);

                
                
                //_canonAimController = new CanonAimController(_canonView._muzzleTransform, _playerView.PlayerTransform);
                //_bulletEmitterController = new BulletEmitterController(_canonView._bullets, _canonView._emitterTransform);
                //_levelCompleteController = new LevelCompleteController(_playerView, _deathZones, _winZones);

                _controllers.Add(_playerController);
                _controllers.Add(_cameraController);
                _controllers.Add(fxController);
                _controllers.Add(inputController);
                _controllers.Add(_playerAnimator);
                _controllers.Add(_objectsAnimationsController);
                _controllers.Add(contactPooler);
                _controllers.Add(jumpsCoolDown);
                _controllers.Add(stunCoolDown);
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


