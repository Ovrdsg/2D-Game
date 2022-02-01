using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyLand
{
    public class CoinsController : IDisposable
    {
        private const float _animationsSpeed = 10;

        private PlayerView _playerView;
        private SpriteAnimator _coinAnimator;
        private List<CoinView> _coinViews;


        public CoinsController(PlayerView player, SpriteAnimator coinAnimator, List<CoinView> coinViews)
        {
            _playerView = player;
            _coinAnimator = coinAnimator;
            _coinViews = coinViews;
            _playerView.OnObjectContact += OnLevelObjectContact; //Event Subscribe
            foreach (var coinView in _coinViews)
            {
                _coinAnimator.StartAnimation
                    (coinView.SpriteRenderer, AnimStatePlayer.idle, true, _animationsSpeed);
            }
        }

        public void OnLevelObjectContact(CoinView contactObjectView)
        {
            if (_coinViews.Contains(contactObjectView))
            {
                _coinAnimator.StopAnimation(contactObjectView.SpriteRenderer);
                GameObject.Destroy(contactObjectView.gameObject);
            }
        }

        public void Dispose()
        {
            _playerView.OnObjectContact -= OnLevelObjectContact; //Event UnSubscribe
            _coinViews.Clear();
        }
    }
}
