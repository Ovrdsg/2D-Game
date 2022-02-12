using System;
using UnityEngine;

namespace SunnyLand
{
    internal class FxView: MonoBehaviour, IAnimation, IDisposable
    {
        
        [SerializeField] private ObjectsAnimationsConfig _objectsAnimationsConfig;
        private SpriteRenderer _spriteRenderer;
        private SpriteAnimator _spriteAnimator;
        
        public Transform FxTransform => transform;        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public SpriteAnimator SpriteAnimator => _spriteAnimator;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            AnimationsInit(_objectsAnimationsConfig);
        }

        public void AnimationsInit(ObjectsAnimationsConfig animationsConfig)
        {
            _spriteAnimator = new SpriteAnimator(animationsConfig);
        }


        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}