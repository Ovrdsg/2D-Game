using UnityEngine;

namespace SunnyLand
{
    internal sealed class CherryView : PlayerView, ICollectable
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteAnimator _spriteAnimator;
        private SpriteAnimationsConfig _config;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public SpriteAnimator SpriteAnimator => _spriteAnimator;
        public GameObject GameObject => gameObject;
        internal void Init(SpriteAnimationsConfig config)
        {
            _config = config;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteAnimator = new SpriteAnimator(_config);
        }
    }
}