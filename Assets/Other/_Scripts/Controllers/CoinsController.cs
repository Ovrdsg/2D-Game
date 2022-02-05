using System;
using System.Collections.Generic;
using PlatformerMVC.Configs;
using PlatformerMVC.View;
using UnityEngine;

namespace PlatformerMVC.Controllers
{
    public class CoinsController: IDisposable
    {
        private const float _animationsSpeed = 10;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _coinAnimator;
        private List<CoinView> _coinViews;


        public  CoinsController(LevelObjectView player, SpriteAnimatorController coinAnimator, List<CoinView> coinViews)
        {
            _playerView = player;
            _coinAnimator = coinAnimator;
            _coinViews = coinViews;
            _playerView.OnObjectContact += OnLevelObjectContact; //Event Subscribe
            foreach (var coinView in _coinViews)
            {
                 _coinAnimator.StartAnimation
                     (coinView.SpriteRenderer, AnimStatePlayer.Run, true, _animationsSpeed);   
            }
        }

        public void OnLevelObjectContact(CoinView contactObjectView)
        {
            if(_coinViews.Contains(contactObjectView))
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