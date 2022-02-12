using System;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public abstract class InteractableObject : MonoBehaviour, IDisposable
    {
        public Action<InteractableObject> ObjectContact;
        
        [SerializeField] private ObjectType _type;
        protected Transform _transform;
        protected SpriteRenderer _spriteRenderer;
        protected Collider2D _collider2D;
        
        internal Transform ObjectTransform => _transform;
        internal Collider2D Collider2D => _collider2D;
        public ObjectType Type => _type;
        
        protected virtual void Awake()
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            col.gameObject.TryGetComponent(out InteractableObject levelObject);
            ObjectContact?.Invoke(levelObject); 
        }

        protected abstract void Interaction(InteractableObject obj);

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}