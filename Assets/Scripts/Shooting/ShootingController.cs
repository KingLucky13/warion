using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingController
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.Transform.Position;

        private WeaponModel _weapon;

        private float _nextShotTimeSec;
        private BaseCharacterModel _target;

        private readonly IShootingTarget _shootingTarget;
        private readonly ITimer _timer;

        public ShootingController(IShootingTarget shootingTarget,ITimer timer)
        {
            _shootingTarget = shootingTarget;
            _timer = timer;
        }

        public void TryShoot(Vector3 position)
        {
            _target = _shootingTarget.GetTarget(position,_weapon.Description.ShootRadius);

            _nextShotTimeSec -= _timer.DeltaTime;
            if (_nextShotTimeSec < 0)
            {
                if (HasTarget)
                    _weapon.Shoot(position,TargetPosition);
            
                _nextShotTimeSec = _weapon.Description.ShootSpeedSec;
            }
        }

        public void SetWeapon(WeaponModel weapon) 
        {
            _weapon = weapon;
        }
    }
}