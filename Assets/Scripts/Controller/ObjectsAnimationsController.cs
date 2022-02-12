using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = System.Object;

namespace SunnyLand
{
    internal sealed class ObjectsAnimationsController : IDisposable, IExecute
    {
        private ObjectsAnimationsConfig _objectsAnimationsCfg;
        private float _cherryAnimSpeed;
        private float _gemAnimSpeed;
        private PlayerView _playerView;
        private SpriteAnimator _coinAnimator;
        private List<InteractableObject> _collectiblesList;

        


        public ObjectsAnimationsController(PlayerView player, ObjectsAnimationsConfig objectsAnimationsCfg, List<InteractableObject> interactableObjects)
        {
            
            _playerView = player;
            _objectsAnimationsCfg = objectsAnimationsCfg;
            _collectiblesList = interactableObjects;

            foreach (var obj in interactableObjects)
            {
                if(obj is IAnimation animation) animation.AnimationsInit(_objectsAnimationsCfg);
                
                switch (obj)
                {
                    case CollectibleView collectible:
                    {
                        switch (collectible.Type)
                        {
                            case ObjectType.LIFE:
                            {
                                var o = (IAnimation) obj;
                        
                                o.SpriteAnimator.StartAnimation(o.SpriteRenderer, AnimationStates.cherryIdle, true, _objectsAnimationsCfg.AnimationSpeed);
                                break;
                            }
                            case ObjectType.POINTS:
                            {
                                var o = (IAnimation) obj;
                        
                                o.SpriteAnimator.StartAnimation(o.SpriteRenderer, AnimationStates.gemIdle, true, _objectsAnimationsCfg.AnimationSpeed);
                                break;
                            }
                        }

                        break;
                    }
                    case PlayerView playerView:
                    {
                        var o = (IAnimation) obj;
                    
                        o.SpriteAnimator.StartAnimation(o.SpriteRenderer, AnimationStates.idle, true, _objectsAnimationsCfg.AnimationSpeed);
                        break;
                    }
                    case EnemyView enemyView:
                    {
                        switch (enemyView.Type)
                        {
                            case ObjectType.OPOSSUM:
                            {
                                var o = (IAnimation) obj;
                            
                                o.SpriteAnimator.StartAnimation(o.SpriteRenderer, AnimationStates.opossumRun, true, _objectsAnimationsCfg.AnimationSpeed +5);
                                break;
                            }
                        }

                        break;
                    }
                }
            }
            
            _playerView.PickUpPicked += OnPickUpPicked; //Event Subscribe
            
        }

        private void OnPickUpPicked(InteractableObject contactObjectView)
        {
            if (_collectiblesList.Contains(contactObjectView as CollectibleView))
            {
                var o = (CollectibleView) contactObjectView;
                o.SpriteAnimator.StopAnimation(o.SpriteRenderer);
                o.Dispose();
            }
        }

        public void Execute(float deltaTime)
        {
            foreach (var view in _collectiblesList)
            {
                var v = (IAnimation) view;
                v.SpriteAnimator.Execute(deltaTime);
            }
        }

        public void Dispose()
        {
            _playerView.PickUpPicked -= OnPickUpPicked; //Event UnSubscribe
            _collectiblesList.Clear();
        }
    }
}
