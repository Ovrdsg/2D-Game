using UnityEngine;
using System.Collections.Generic;


namespace SunnyLand
{
    public class SpriteAnimation
    {
        public AnimStatePlayer Track;
        public List<Sprite> Sprites;
        public bool Loop;
        public float Speed = 10;
        public float Counter;
        public bool Sleep;

        public void Update(float deltaTime)
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
            }
        }
    }
}