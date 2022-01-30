using System;
using UnityEngine;

namespace SunnyLand
{
    internal abstract class InteractiveObject : MonoBehaviour, IDisposable
    {
        private bool _isInteractable;
        protected Rigidbody2D _rigidbody2D;
        protected PlayerView _player;


        protected bool IsInteractable
        {
            get => _isInteractable;

            private set
            {
                _isInteractable = value;
                GetComponent<Renderer>().enabled = _isInteractable;
                GetComponent<Collider>().enabled = _isInteractable;
            }

        }

        protected abstract void Interaction();



        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
