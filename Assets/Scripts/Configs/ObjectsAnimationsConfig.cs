using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace SunnyLand
{

    [CreateAssetMenu(fileName = "ObjectAnimationsConfig", menuName = "Configs/ObjectsAnimationsConfig", order = 1)]
    public class ObjectsAnimationsConfig : ScriptableObject
    {
        [SerializeField] private float _animationSpeed;
        
        [Serializable]
        public sealed class SpritesSequence
        {
            public AnimationStates Track;
            [SerializeField] private bool _loop = false;
            public List<Sprite> Sprites = new List<Sprite>();

            internal bool Loop => _loop;
        }

        public float AnimationSpeed => _animationSpeed;
        public List<SpritesSequence> Sequences = new List<SpritesSequence>();
    }
}
