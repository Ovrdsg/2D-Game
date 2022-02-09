using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace SunnyLand
{
    public enum AnimStatePlayer
    {
        idle = 0,
        run = 1,
        jump = 2,
        fall = 3,
        hurt = 4,
        crouch = 5,
        climb = 6
    }


    [CreateAssetMenu(fileName = "SpriteAnimationsConfig", menuName = "Configs/SpriteAnimationsConfig", order = 1)]
    public class SpriteAnimationsConfig : ScriptableObject
    {
        [SerializeField] private float _animationSpeed;
        
        [Serializable]
        public sealed class SpritesSequence
        {
            public AnimStatePlayer Track;
            [SerializeField] private bool _loop = false;
            public List<Sprite> Sprites = new List<Sprite>();

            internal bool Loop => _loop;
        }

        public float AnimationSpeed => _animationSpeed;
        public List<SpritesSequence> Sequences = new List<SpritesSequence>();
    }
}
