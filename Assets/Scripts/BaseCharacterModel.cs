using LearnGame.Movement;
using LearnGame.Shooting;
using System;
using UnityEngine;

namespace LearnGame
{
    public class BaseCharacterModel
    {

        public event Action Dead;

        public bool IsShooting => _shootingController.HasTarget;
        public TransformModel Transform {  get; private set; }

        public float Hp { get; private set; }

        private readonly IMovementController _characterMovementController;
        private readonly ShootingController _shootingController;

        public BaseCharacterModel(IMovementController movementController,ShootingController shootingController,
            ICharacterConfig config) 
        {
            _characterMovementController = movementController;
            _shootingController = shootingController;

            Hp= config.Hp;
        }

        public void Initialize(Vector3 position,Quaternion rotation)
        {
            Transform=new TransformModel(position,rotation);
        }

        public void Move(Vector3 direction)
        {
            Vector3 lookDirection = direction;
            if (_shootingController.HasTarget)
            {
                lookDirection = (_shootingController.TargetPosition - Transform.Position).normalized;
            }
            Transform.Position += _characterMovementController.Translate(direction);
            Transform.Rotation = _characterMovementController.Rotate(Transform.Rotation, lookDirection);
        }

        public void Damage(float damage)
        {
            Hp-=damage;
            if (Hp <= 0) 
                Dead?.Invoke();
        }
 
        public void TryShoot(Vector3 shotPosition)
        {
            _shootingController.TryShoot(shotPosition);
        }

        public void SetWeapon(WeaponModel weapon)
        {
            _shootingController.SetWeapon(weapon);
        }

        public void SetSpeedBoost(float time)
        {
            _characterMovementController.setSpeedBoost(time);
        }

    }
}
