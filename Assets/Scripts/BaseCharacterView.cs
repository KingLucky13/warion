using LearnGame.Movement;
using LearnGame.Pickup;
using LearnGame.Shooting;
using System;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(CharacterController),typeof(Animator))]
    public abstract class BaseCharacterView : MonoBehaviour
    {

        public event Action<BaseCharacterView> Dead;

        private Animator _animator;
        private CharacterController _characterController;

        [SerializeField]
        private WeaponFactory _baseWeapon;

        [SerializeField]
        private Transform _hand;

        private WeaponView _weapon;

        private IMovementDirectionSource _movementDirectionSource;

        public BaseCharacterModel Model { get; private set; }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _movementDirectionSource=GetComponent<IMovementDirectionSource>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeapon);
        }

        public void Initialize(BaseCharacterModel model)
        {
            Model = model;
            Model.Initialize(transform.position, transform.rotation);
            Model.Dead += OnDeath;
        }
        protected void Update()
        {
            Model.Move(_movementDirectionSource.MovementDirection);
            Model.TryShoot(_weapon.BulletSpawnPosition.position);

            var moveDelta = Model.Transform.Position - transform.position;
            _characterController.Move(moveDelta);
            Model.Transform.Position = transform.position;

            transform.rotation = Model.Transform.Rotation;
            
            _animator.SetBool("isMoving",moveDelta !=  Vector3.zero);
            _animator.SetBool("isShooting",Model.IsShooting);

        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Dead -= OnDeath;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if(LayerUtils.IsBullet(other.gameObject))
            {
                BulletModel bullet=other.gameObject.GetComponent<BulletView>().Model;

                Model.Damage(bullet.Damage);

                Destroy(other.gameObject);
            }
            else if(LayerUtils.IsItem(other.gameObject))
            {
                var item = other.gameObject.GetComponent<PickUpItem>();
                item.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(WeaponFactory weaponFactory)
        {
            if(_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = weaponFactory.Create(_hand);

            Model.SetWeapon(_weapon.Model);
        }
        /*
        public void SetSpeedBoost(float power,float time)
        {
            _characterMovementController.setSpeedBoost(power,time);
        }
        */
        private void OnDeath()
        {
            Dead?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
