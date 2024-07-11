using LearnGame.Timer;
using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class BulletModel
    {
        public TransformModel Transform { get; private set; }
        public TransformModel Direction { get; private set; }

        private float _speed;
        private float _maxDistance;
        private float _currentDistance;

        public float Damage { get; private set; }

        private readonly ITimer _timer;

        public event Action Disappear;

        public BulletModel(WeaponDescription description,Vector3 direction, ITimer timer)
        {
            _speed = description.BulletSpeed;
            _currentDistance = 0;
            _maxDistance = description.BulletMaxDistance;

            Damage = description.Damage;

            Direction = new TransformModel(direction);
            _timer = timer;
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            Transform = new TransformModel(position, rotation);
        }

        public void Move()
        {
            float delta = _speed * _timer.DeltaTime;
            _currentDistance += delta;
            Transform.Position += Direction.Position * delta;
            if (_currentDistance >= _maxDistance)
            {
                Disappear.Invoke();
            }
        }
    }
}