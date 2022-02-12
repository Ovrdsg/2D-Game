using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyLand
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerView : InteractableObject, IAnimation
    {
        public event Action<CollectibleView> PickUpPicked;
        public event Action<EnemyView> EnemyContact;
        
        private ObjectsAnimationsConfig _animationsConfig;
        private SpriteAnimator _spriteAnimator;
        
        private Rigidbody2D _rigidbody2D;
        
        public SpriteAnimator SpriteAnimator => _spriteAnimator;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        internal Rigidbody2D Rigidbody2D => _rigidbody2D;
        
        protected override void Awake()
        {
            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void AnimationsInit(ObjectsAnimationsConfig animationsConfig)
        {
            _spriteAnimator = new SpriteAnimator(animationsConfig);
        }
        
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if(col.TryGetComponent(out CollectibleView pickUp))
                PickUpPicked?.Invoke(pickUp);
            else if (col.TryGetComponent(out EnemyView enemy))
            {
                EnemyContact?.Invoke(enemy);
                if (enemy.transform.position.x > transform.position.x)
                {
                    Rigidbody2D.velocity += Vector2.left * 10;
                }
                else if (enemy.transform.position.x < transform.position.x)
                {
                    Rigidbody2D.velocity += Vector2.right * 10;
                }
            }
            base.OnTriggerEnter2D(col);
        }
        
        protected override void Interaction(InteractableObject obj)
        {
            
        }

        internal void AddHealth()
        {
            print("Add health to Player");
        }
    }
}
