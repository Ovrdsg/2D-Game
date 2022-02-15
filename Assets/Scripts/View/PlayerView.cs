using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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


        private AudioSource _audioSource;
        [SerializeField] private AudioClip _collisionAudioClip;
        [SerializeField] private AudioClip _gemAudioClip;
       
        private AudioSource _audioSource2;
        private AudioClip _jumpSound;
        

        internal void PlaySound()
        {
            _audioSource2.PlayOneShot(_jumpSound);
        }
        internal AudioClip Sound
        {
            set => _jumpSound = value;
        }
        protected override void Awake()
        {
            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();          
        }

        public void AnimationsInit(ObjectsAnimationsConfig animationsConfig)
        {
            _spriteAnimator = new SpriteAnimator(animationsConfig);
        }
        
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if(col.TryGetComponent(out CollectibleView pickUp))
            {
                PickUpPicked?.Invoke(pickUp);
                _audioSource.PlayOneShot(_gemAudioClip);               

            }
               
            else if (col.TryGetComponent(out EnemyView enemy))
            {
                EnemyContact?.Invoke(enemy);
                _audioSource.PlayOneShot(_collisionAudioClip);

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
