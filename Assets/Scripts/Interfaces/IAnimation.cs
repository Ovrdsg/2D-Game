using UnityEngine;

namespace SunnyLand
{
    internal interface IAnimation
    {
        public SpriteAnimator SpriteAnimator { get; }
        public SpriteRenderer SpriteRenderer { get; }
        void AnimationsInit(ObjectsAnimationsConfig animationsConfig);
    }
}