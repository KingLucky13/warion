using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.Movement
{

    public class CharacterMovementController:IMovementController
    {
        private static readonly float SqrtEps = Mathf.Epsilon * Mathf.Epsilon;

        private readonly float _speed;
        private readonly float _rotationSpeed;

        private readonly float _accelerationPower;

        private float _accelerationTime=0f;
        
        private readonly ITimer _timer;
        public CharacterMovementController(ICharacterConfig config,ITimer timer)
        {
            _speed = config.Speed;
            _rotationSpeed = config.RotationSpeed;
            _accelerationPower = config.AccelerationPower;

           _timer = timer;
        }

        public Vector3 Translate(Vector3 movementDirection)
        {
            Vector3 delta = movementDirection * _speed * _timer.DeltaTime;
            if (_accelerationTime > 0f)
            {
                delta = delta * _accelerationPower;
                _accelerationTime -= _timer.DeltaTime;
            }
            return delta;
        }

        public Quaternion Rotate(Quaternion currentRotation,Vector3 lookDirection)
        {
            if (_rotationSpeed > 0f && lookDirection != Vector3.zero)
            {
                var currentLookDirection = currentRotation * Vector3.forward;
                float sqrtMagnitude = (currentLookDirection - lookDirection).sqrMagnitude;
                if (sqrtMagnitude > SqrtEps)
                {
                    Quaternion newRotation = Quaternion.Slerp(currentRotation, Quaternion.LookRotation(lookDirection, Vector3.up), _rotationSpeed * _timer.DeltaTime);
                 return newRotation;
                }
            }
            return currentRotation;
        }

        public void setSpeedBoost(float time)
        {
            _accelerationTime = time;
        }
    }
}
