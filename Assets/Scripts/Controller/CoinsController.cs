using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyLand
{
    internal sealed class CoinsController : IDisposable, IExecute
    {
        private SpriteAnimationsConfig _cherryAnimationsCfg;
        private SpriteAnimationsConfig _gemAnimationsCfg;
        private float _cherryAnimSpeed;
        private float _gemAnimSpeed;
        
        private PlayerView _playerView;
        private SpriteAnimator _coinAnimator;
        private List<GemView> _gemViews;
        private List<CherryView> _cherryViews;
        private List<ICollectable> _collectablesList;


        public CoinsController(SpriteAnimationsConfig cherryAnimationCfg, SpriteAnimationsConfig gemAnimationCfg, PlayerView player, List<GemView> gemViews, List<CherryView> cherryViews )
        {
            _cherryAnimationsCfg = cherryAnimationCfg;
            _gemAnimationsCfg = gemAnimationCfg;
            _cherryAnimSpeed = cherryAnimationCfg.AnimationSpeed;
            _gemAnimSpeed = gemAnimationCfg.AnimationSpeed;
            _playerView = player;
            _gemViews = gemViews;
            _cherryViews = cherryViews;
            _collectablesList = new List<ICollectable>();
           
            foreach (var gemView in _gemViews)
            {
                _collectablesList.Add(gemView);
            }
            
            foreach (var cherryView in _cherryViews)
            {
                _collectablesList.Add(cherryView);
            }
            
            _playerView.OnObjectContact += OnLevelObjectContact; //Event Subscribe
            
            foreach (var gemView in _gemViews)
            {
                gemView.Init(_gemAnimationsCfg);
                gemView.SpriteAnimator.StartAnimation
                    (gemView.SpriteRenderer, AnimStatePlayer.idle, true, _cherryAnimSpeed);
            }
            foreach (var cherryView in cherryViews)
            {
                cherryView.Init(cherryAnimationCfg);
                cherryView.SpriteAnimator.StartAnimation
                    (cherryView.SpriteRenderer, AnimStatePlayer.idle, true, _cherryAnimSpeed);
            }
        }

        private void OnLevelObjectContact(ICollectable contactObjectView)
        {
            if (_collectablesList.Contains(contactObjectView))
            {
                contactObjectView.SpriteAnimator.StopAnimation(contactObjectView.SpriteRenderer);
                GameObject.Destroy(contactObjectView.GameObject);
            }
        }

        public void Execute(float deltaTime)
        {
            foreach (var collectible in _collectablesList)
            {
                collectible.SpriteAnimator.Execute(deltaTime);
            }
        }

        public void Dispose()
        {
            _playerView.OnObjectContact -= OnLevelObjectContact; //Event UnSubscribe
            _collectablesList.Clear();
            _gemViews.Clear();
            _cherryViews.Clear();
        }
    }
}
