using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyLand
{
    internal class PlayerView : MonoBehaviour
    {
        public Transform _transform;
        public SpriteRenderer SpriteRenderer;
        public Rigidbody2D _rigidbody2D;
        public Collider2D _collider2D;

        public Action<ICollectable> OnObjectContact;
        internal Transform PlayerTransform => _transform;

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.gameObject.TryGetComponent(out ICollectable levelObject);
            OnObjectContact?.Invoke(levelObject);            
        }
    }
}
