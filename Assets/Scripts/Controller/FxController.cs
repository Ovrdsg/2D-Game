using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace SunnyLand
{
    internal class FxController : IExecute
    {
        private FxFactory _fxFactory;
        private PlayerView _playerView;
        private List<FxView> _fxList;
        private ObjectsAnimationsConfig _animationsConfig;
        
        public FxController(PlayerView playerView, FxFactory fxFactory, ObjectsAnimationsConfig animationsConfig, List<FxView> listAnimatedObjects)
        {
            _playerView = playerView;
            _fxFactory = fxFactory;
            _animationsConfig = animationsConfig;
            _fxList = listAnimatedObjects;
            _playerView.PickUpPicked += OnPickUpPicked;
            _playerView.EnemyContact += OnEnemyContact;
        }

        private void OnEnemyContact(EnemyView obj)
        {
            var fxView = _fxFactory.GetFx(FxType.ENEMYDEATH);
            fxView.transform.position = _playerView.ObjectTransform.position;
            fxView.SpriteAnimator.StartAnimation(fxView.SpriteRenderer, AnimationStates.enemyDeath, false, _animationsConfig.AnimationSpeed+5);
            _fxList.Add(fxView);
        }

        private void OnPickUpPicked(CollectibleView obj)
        {
            var fxView = _fxFactory.GetFx(FxType.PICKUP);
            fxView.transform.position = obj.ObjectTransform.position;
            fxView.SpriteAnimator.StartAnimation(fxView.SpriteRenderer, AnimationStates.itemPickUp, false, _animationsConfig.AnimationSpeed+5);
            _fxList.Add(fxView);
        }


        public void Execute(float deltaTime)
        {
            foreach (var fx in _fxList.ToList().Where(fx => fx.SpriteAnimator.ActiveAnimation[fx.SpriteRenderer].Sleep &&
                                                            fx.SpriteAnimator.ActiveAnimation[fx.SpriteRenderer].Track == AnimationStates.itemPickUp))
            {
                fx.SpriteAnimator.StopAnimation(fx.SpriteRenderer);
                _fxList.Remove(fx);
                fx.Dispose();
            }
            
            foreach (var fx in _fxList.ToList().Where(fx => fx.SpriteAnimator.ActiveAnimation[fx.SpriteRenderer].Sleep &&
                                                            fx.SpriteAnimator.ActiveAnimation[fx.SpriteRenderer].Track == AnimationStates.enemyDeath))
            {
                fx.SpriteAnimator.StopAnimation(fx.SpriteRenderer);
                _fxList.Remove(fx);
                fx.Dispose();
            }

            foreach(var fx in _fxList)
                fx.SpriteAnimator.Execute(deltaTime);
        }
        
        
    }
}