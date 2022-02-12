using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SunnyLand
{
    internal class EnemyView : InteractableObject, IAnimation
    {
        [SerializeField] private BoxCollider2D _head;
        
        private SpriteAnimator _spriteAnimator;
        
        protected override void Interaction(InteractableObject obj)
        {
            
        }

        public SpriteAnimator SpriteAnimator => _spriteAnimator;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D other)
        {
            GetComponent<AIPath>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            _collider2D.enabled = false;
            _spriteRenderer.flipY = true;
            _head.enabled = false;
            Vector3 movement = new Vector2(Random.Range(40, 70), Random.Range(-40, 40));
            transform.position += movement * Time.deltaTime;
        }

        public void AnimationsInit(ObjectsAnimationsConfig animationsConfig)
        {
            _spriteAnimator = new SpriteAnimator(animationsConfig);
        }
    }
}