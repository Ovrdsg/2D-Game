using System.Collections.Generic;
using UnityEngine;
using System;


namespace SunnyLand
{
    internal class SpriteAnimator : IExecute, IDisposable
    {

        private ObjectsAnimationsConfig _config;
        private Dictionary<SpriteRenderer, SpriteAnimation> _activeAnimation = new Dictionary<SpriteRenderer, SpriteAnimation>();

        public Dictionary<SpriteRenderer, SpriteAnimation> ActiveAnimation => _activeAnimation;
        public SpriteAnimator(ObjectsAnimationsConfig config)
        {
            _config = config;
        }

        public void Execute(float deltaTime)
        {
            foreach (var animation in _activeAnimation)
            {
                animation.Value.Update(deltaTime);

                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }


        public void StartAnimation(SpriteRenderer spriteRenderer, AnimationStates track, bool loop, float speed)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Sleep = false;
                animation.Loop = loop;
                animation.Speed = speed;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimation.Add(spriteRenderer, new SpriteAnimation()
                {

                    Loop = loop,
                    Speed = speed,

                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites

                });
            }
        }



        public void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimation.ContainsKey(spriteRenderer))
            {
                _activeAnimation.Remove(spriteRenderer);
            }
        }

        public void Dispose()
        {
            _activeAnimation.Clear();
        }
    }
}
