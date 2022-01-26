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
        hurt = 3,
        crouch = 4,
        climb = 5
    }


    [CreateAssetMenu(fileName = "SpriteAnimationsConfig", menuName = "Configs/SpriteAnimationsConfig", order = 1)]
    public class SpriteAnimationsConfig : ScriptableObject
    {
        [Serializable]

        public sealed class SpritesSequence
        {
            public AnimStatePlayer Track;
            public List<Sprite> Sprites = new List<Sprite>();

        }

        public List<SpritesSequence> Sequences = new List<SpritesSequence>();
    }
}
