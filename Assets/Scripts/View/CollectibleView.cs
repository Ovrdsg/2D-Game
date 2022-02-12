using System;
using UnityEngine;

namespace SunnyLand
{
    internal class CollectibleView : InteractableObject, ICollectable, IAnimation
    {
        
        private SpriteAnimator _spriteAnimator;
        public SpriteAnimator SpriteAnimator => _spriteAnimator;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public float AmountAdd { get; }
        
        
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            
            col.TryGetComponent(out PlayerView playerView);
            Interaction(playerView);
            col.TryGetComponent(out CollectibleView collectible);
        }

        protected override void Interaction(InteractableObject obj)
        {

        }

        
        public void AnimationsInit(ObjectsAnimationsConfig animationsConfig)
        {
            _spriteAnimator = new SpriteAnimator(animationsConfig);
        }
    }
}