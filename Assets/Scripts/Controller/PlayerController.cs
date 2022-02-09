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
        private JumpsCoolDown _jumpsCoolDown;
        private SpriteAnimator _animator;
        private readonly ContactPooler _contactPooler;
        private readonly SpriteAnimationsConfig _animationsConfig;
        private readonly PlayerMoveConfig _playerMoveConfig;
        private IUserInputProxy _horizontalInputProxy;
        private IUserInputProxy _verticalInputProxy;

        public PlayerController(PlayerView player, SpriteAnimator animator, SpriteAnimationsConfig animationsConfig, IUserInputProxy horizontal, IUserInputProxy vertical,
            ContactPooler contactPooler, PlayerMoveConfig playerMoveConfig, JumpsCoolDown jumpsCoolDown)
        {
            _playerView = player;
            _animator = animator;
            _animationsConfig = animationsConfig;
            _horizontalInputProxy = horizontal;
            _verticalInputProxy = vertical;
            _contactPooler = contactPooler;
            _playerMoveConfig = playerMoveConfig;
            _jumpsCoolDown = jumpsCoolDown;
            _animationSpeed = _animationsConfig.AnimationSpeed;
            _speed = _playerMoveConfig.Speed;
            _jumpForce = _playerMoveConfig.JumpForce;
            _movingThreshHold = _playerMoveConfig.MovingThreshHold;
            _jumpThreshHold = _playerMoveConfig.JumpThreshHold;
            _fallThreshHold = _playerMoveConfig.FallThreshHold;
            
            
            _animator.StartAnimation(_playerView.SpriteRenderer, AnimStatePlayer.idle, _animationsConfig.Sequences[(int)AnimStatePlayer.idle].Loop, _animationSpeed);
            _isFalling = false;

            _horizontalInputProxy.AxisChange += OnHorizontalChange;
            _verticalInputProxy.AxisChange += OnVerticalChange;
        }

        private void OnHorizontalChange(float value)
        {
            _xAxisInput = value;
            
        }

        private void OnVerticalChange(float value)
        {
            _yAxisInput = value;
            
        }

        public void MoveTowards(float fixedDeltaTime)
        {
            _xVelocity = fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            
            _playerView._rigidbody2D.velocity = _playerView._rigidbody2D.velocity.Change(x: _xVelocity);
            
            _playerView.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);

        }

        public void FixedExecute(float fixedDeltaTime)
        {
            _isJump = _yAxisInput > 0;
            _isFalling = _playerView._rigidbody2D.velocity.y < _fallThreshHold;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshHold;
            
            if (_isMoving) MoveTowards(fixedDeltaTime);


            if (_contactPooler.IsGrounded)
            {
                _isFalling = false;
                _animator.StartAnimation
                    (_playerView.SpriteRenderer, _isMoving ? AnimStatePlayer.run : AnimStatePlayer.idle, true, _animationSpeed);


                if (!_isJump || !(Mathf.Abs(_playerView._rigidbody2D.velocity.y) <= _jumpThreshHold) ||
                    !_jumpsCoolDown.CanJump) return;
                _playerView._rigidbody2D.velocity = Vector2.up * _jumpForce;
                _jumpsCoolDown.ResetFirstJump();

            }
            else if (_isJump && Mathf.Abs(_playerView._rigidbody2D.velocity.y) >= _jumpThreshHold && _jumpsCoolDown.CanJump)
            {
                    _animator.StartAnimation
                        (_playerView.SpriteRenderer, AnimStatePlayer.jump, _animationsConfig.Sequences[(int)AnimStatePlayer.jump].Loop, _animationSpeed);
            }
            
            else if (_isFalling)
            {
                _animator.StartAnimation
                    (_playerView.SpriteRenderer, AnimStatePlayer.fall, _animationsConfig.Sequences[(int)AnimStatePlayer.fall].Loop, _animationSpeed);
            }
            
        }

        public void Dispose()
        {
            _horizontalInputProxy.AxisChange -= OnHorizontalChange;
            _verticalInputProxy.AxisChange -= OnVerticalChange;
        }
    }
}
