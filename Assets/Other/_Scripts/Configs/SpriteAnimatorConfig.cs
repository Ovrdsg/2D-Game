using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC.Configs
{

    public enum AnimStatePlayer 
    {
        Idle = 0,
        Run = 1,
        Jump = 2
    }

    [CreateAssetMenu(fileName = "SpriteAnimatorConfig", menuName = "Configs/ Animation CFG", order = 1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public  AnimStatePlayer Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
