using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyLand
{
    public class PlayerView : MonoBehaviour
    {
        public Transform _Transform;
        public SpriteRenderer SpriteRenderer;
        public Rigidbody2D _rigidbody2D;
        public Collider2D _collider2D;

        public Action<CoinView> OnObjectContact;

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.gameObject.TryGetComponent(out CoinView levelObject);
            OnObjectContact?.Invoke(levelObject);            
        }
    }
}
