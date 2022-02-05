using PlatformerMVC.View;
using UnityEngine;

namespace PlatformerMVC.Controllers
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;


        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        public void Active(bool value)
        {
            _view.gameObject.SetActive(value);
        }


        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            float angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 _axis = Vector3.Cross(Vector3.left, _velocity);
            _view._Transform.rotation = Quaternion.AngleAxis(angle, _axis);
        }
        
        
        
        public void Shoot(Vector3 position, Vector3 velocity)
        {
            Active(true);
            SetVelocity(velocity);
            _view._Transform.position = position;
            _view._rigidbody2D.velocity = Vector2.zero;
            _view._rigidbody2D.AddForce(-velocity, ForceMode2D.Impulse); //TODO here the velocity should be negative to shoot at the player
        }
        
        
        
    }
}