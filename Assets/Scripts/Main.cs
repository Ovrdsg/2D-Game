using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SunnyLand
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimationsConfig _playerAnimatorConfig;
        [SerializeField] private int _animationSpeed;
        [SerializeField] private PlayerView _playerView;
        private Controllers _controllers;
        private SpriteAnimator _spriteAnimator;

        

        

        private void Start()
        {
            _playerAnimatorConfig = Resources.Load<SpriteAnimationsConfig>("SpriteAnimationsConfig");
            _controllers = new Controllers();

            if (_playerAnimatorConfig)
            {
                _spriteAnimator = new SpriteAnimator(_playerAnimatorConfig);
                _spriteAnimator.StartAnimation(_playerView.playerSpriteRenderer, AnimStatePlayer.idle, true, 10);
                _controllers.Add(_spriteAnimator);
            }

            else
            {
                Debug.Log("error!");
            }
        }

        private void Update()
        {
            var deltatime = Time.deltaTime;
            _controllers.Execute(deltatime);
        }


    }
}


