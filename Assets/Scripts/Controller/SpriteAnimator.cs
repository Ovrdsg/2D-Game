using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SunnyLand
{
    public class SpriteAnimator : IDisposable, IExecute
    {
       
        private SpriteAnimationsConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimator(SpriteAnimationsConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimStatePlayer track, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleeps = false;
                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Execute(float deltatime)
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.UpdateAnimation(deltatime);
                animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
