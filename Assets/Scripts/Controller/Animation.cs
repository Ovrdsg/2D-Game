using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    public class Animation
    {
        public AnimStatePlayer Track;
        public List<Sprite> Sprites;
        public bool Loop = false;
        public float Speed = 10;
        public float Counter = 0;
        public bool Sleeps;

        public void UpdateAnimation(float deltatime)
        {
            if (Sleeps) return;
            Counter += deltatime * Speed;
            if (Loop)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count - 1;
                Sleeps = true;
            }
        }
    }
}
