using System;
using UnityEngine;
using System.Collections.Generic;


namespace SunnyLand
{
    internal class SpriteAnimation
    {
        public event Action<AnimationStates, SpriteAnimation> AnimationSleep;
        
        public AnimationStates Track;
        public List<Sprite> Sprites;
        public bool Loop;
        public float Speed;
        public float Counter;
        public bool Sleep;

        internal void Update(float deltaTime)
        {
            if (Sleep) return;

            Counter += deltaTime * Speed;
            if (Loop)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count;
                Sleep = true;
                AnimationSleep?.Invoke(Track, this);
            }
        }
    }
}