using UnityEngine;

namespace SunnyLand
{
    public interface ICollectable
    {
        SpriteRenderer SpriteRenderer { get; } 
        SpriteAnimator SpriteAnimator { get; }
        
        GameObject GameObject { get; }
    }
}