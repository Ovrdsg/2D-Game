using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand { 
    internal sealed class PlayerController : IFixedExecute, IDisposable
    {
        private float _xAxisInput;
        private float _yAxisInput;
        private bool _isJump;
        private bool _isMoving;
        private bool _isFalling;
        private bool _isStuned;

        private readonly float _speed;
        private readonly float _jumpForce;
        private readonly float _movingThreshHold;
        private readonly float _jumpThreshHold;
        private readonly float _fallThreshHold;
        private readonly float _animationSpeed;
        
        private float _earthGravitation = -9.8f;
        private float _yVelocity;
        private float _xVelocity;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);


        private PlayerView _playerView;
        
        private SpriteAnimator _animator;
        private JumpsCoolDown _jumpsCoolDown;
        private StunCoolDown _stunCoolDown;
        private readonly ContactPooler _contactPooler;
        private readonly ObjectsAnimationsConfig _animationsConfig;
        private readonly PlayerMoveConfig _playerMoveConfig;
        private IUserInputProxy _horizontalInputProxy;
        private IUserInputProxy _verticalInputProxy;

        public PlayerController(PlayerView player, ObjectsAnimationsConfig animationsConfig, IUserInputProxy horizontal, IUserInputProxy vertical,
            ContactPooler contactPooler, PlayerMoveConfig playerMoveConfig, JumpsCoolDown jumpsCoolDown, StunCoolDown stunCoolDown)
        {
            _playerView = player;
            _animator = _playerView.SpriteAnimator;
            _animationsConfig = animationsConfig;
            _horizontalInputProxy = horizontal;
            _verticalInputProxy = vertical;
            _contactPooler = contactPooler;
            _playerMoveConfig = playerMoveConfig;
            _jumpsCoolDown = jumpsCoolDown;
            _stunCoolDown = stunCoolDown;
            _animationSpeed = _animationsConfig.AnimationSpeed;
            _speed = _playerMoveConfig.Speed;
            _jumpForce = _playerMoveConfig.JumpForce;
            _movingThreshHold = _playerMoveConfig.MovingThreshHold;
            _jumpThreshHold = _playerMoveConfig.JumpThreshHold;
            _fallThreshHold = _playerMoveConfig.FallThreshHold;
            _isFalling = false;
            _isStuned = false;

            _horizontalInputProxy.AxisChange += OnHorizontalChange;
            _verticalInputProxy.AxisChange += OnVerticalChange;
            _playerView.EnemyContact += OnEnemyContact;
        }


        private void MoveTowards(float fixedDeltaTime)
        {
            _xVelocity = fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            
            _playerView.Rigidbody2D.velocity = _playerView.Rigidbody2D.velocity.Change(x: _xVelocity);
            
            _playerView.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);

        }

        public void FixedExecute(float fixedDeltaTime)
        {
            _isJump = _yAxisInput > 0;
            _isFalling = _playerView.Rigidbody2D.velocity.y < _fallThreshHold;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshHold && _stunCoolDown.CanMove;
            
            if (_isMoving) MoveTowards(fixedDeltaTime);


            if (_contactPooler.IsGrounded)
            {
                _isFalling = false;
                _animator.StartAnimation
                    (_playerView.SpriteRenderer, _isMoving ? AnimationStates.run : AnimationStates.idle, true, _animationSpeed);


                if (!_isJump || !(Mathf.Abs(_playerView.Rigidbody2D.velocity.y) <= _jumpThreshHold) ||
                    !_jumpsCoolDown.CanJump) return;
                _playerView.Rigidbody2D.velocity = Vector2.up * _jumpForce;
                _jumpsCoolDown.ResetFirstJump();

            }
            else if (_isJump && Mathf.Abs(_playerView.Rigidbody2D.velocity.y) >= _jumpThreshHold && _jumpsCoolDown.CanJump)
            {
                    _animator.StartAnimation
                        (_playerView.SpriteRenderer, AnimationStates.jump, _animationsConfig.Sequences[(int)AnimationStates.jump].Loop, _animationSpeed);
            }
            
            else if (_isFalling)
            {
                _animator.StartAnimation
                    (_playerView.SpriteRenderer, AnimationStates.fall, _animationsConfig.Sequences[(int)AnimationStates.fall].Loop, _animationSpeed);
            }
            else if (!_stunCoolDown.CanMove)
            {
                _playerView.Rigidbody2D.velocity = Vector2.zero;   
                _animator.StartAnimation
                    (_playerView.SpriteRenderer, AnimationStates.hurt, false, _animationSpeed);
            }
            
        }
        
        private void OnEnemyContact(EnemyView enemy)
        {
            _stunCoolDown.ResetFirstStun();
            
            if (enemy.transform.position.x > _playerView.transform.position.x)
            {
                _playerView.Rigidbody2D.velocity += Vector2.left * 10;
            }
            else if (enemy.transform.position.x < _playerView.transform.position.x)
            {
                _playerView.Rigidbody2D.velocity += Vector2.right * 10;
            }
        }

        private void OnHorizontalChange(float value)
        {
            _xAxisInput = _stunCoolDown.CanMove ? value : 0;
        }

        private void OnVerticalChange(float value)
        {
            _yAxisInput = _stunCoolDown.CanMove ? value : 0;
        }

        public void Dispose()
        {
            _horizontalInputProxy.AxisChange -= OnHorizontalChange;
            _verticalInputProxy.AxisChange -= OnVerticalChange;
        }
    }
}
