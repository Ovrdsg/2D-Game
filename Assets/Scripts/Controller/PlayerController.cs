using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand { 
    public class PlayerController
    {
        private float _xAxisInput;
        private bool _isJump;
        private bool _isMoving;

        private float _speed = 190f;
        private float _animationSpeed = 3f;
        private float _jumpSpeed = 10;
        private float _movingThreshHold = .1f;
        private float _jumpThreshHold = 1f;


        private float _earthGravitation = -9.8f;
        private float _yVelocity;
        private float _xVelocity;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);


        private PlayerView _playerView;
        private SpriteAnimator _animator;
        private readonly ContactPooler _contactPooler;

        public PlayerController(PlayerView player, SpriteAnimator animator)
        {
            _playerView = player;
            _animator = animator;
            _animator.StartAnimation(_playerView.SpriteRenderer, AnimStatePlayer.idle, true, _animationSpeed);
            _contactPooler = new ContactPooler(_playerView._collider2D);
        }


        public void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            _playerView._rigidbody2D.velocity = _playerView._rigidbody2D.velocity.Change(x: _xVelocity);
            _playerView.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);

        }

        public void Update()
        {
            _contactPooler.Update();
            _animator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshHold;
            if (_isMoving) MoveTowards();


            if (_contactPooler.IsGrounded)
            {
                _animator.StartAnimation
                    (_playerView.SpriteRenderer, _isMoving ? AnimStatePlayer.run : AnimStatePlayer.idle, true, _animationSpeed);
                if (_isJump && Mathf.Abs(_playerView._rigidbody2D.velocity.y) <= _jumpThreshHold)
                {
                    _playerView._rigidbody2D.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else
            {
                if ((_isJump && Mathf.Abs(_playerView._rigidbody2D.velocity.y) >= _jumpThreshHold))
                {
                    _animator.StartAnimation
                        (_playerView.SpriteRenderer, AnimStatePlayer.jump, true, _animationSpeed);
                }
            }
        }
    }
}
