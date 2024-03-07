using UnityEngine;

namespace LearnGame.Shooting
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;
        private float _speed;
        private float _maxDistance;
        private float _currentDistance;
        public float Damage { get; private set; }

        public void Initialize(Vector3 direction,float speed,float maxDistance,float damage)
        {
            _direction = direction;
            _speed = speed;
            _maxDistance = maxDistance;
            Damage = damage;
        }

        protected void Update()
        {
            float delta=_speed*Time.deltaTime;
            _currentDistance += delta;
            transform.Translate(_direction * delta);
            if(_currentDistance >=_maxDistance) {
                Destroy(gameObject);
            }
        }
       
    }
}