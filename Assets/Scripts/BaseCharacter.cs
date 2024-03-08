
using LearnGame.Movement;
using LearnGame.Pickup;
using LearnGame.Shooting;
using UnityEngine;
using static UnityEditor.Progress;
namespace LearnGame
{
    [RequireComponent(typeof(CharacterMovementController),typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeapon;
        [SerializeField]
        private Transform _hand;
        [SerializeField]
        private float _hp = 2f;
        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;

        protected void Awake()
        {
            _movementDirectionSource=GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeapon);
        }

        protected void Update()
        {
            Vector3 direction = _movementDirectionSource.MovementDirection;
            Vector3 lookDirection = direction;
            if (_shootingController.HasTargert)
            {
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            }
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if(LayerUtils.IsBullet(other.gameObject))
            {
                Bullet bullet=other.gameObject.GetComponent<Bullet>();
                _hp -=bullet.Damage;
                Destroy(other.gameObject);
            }
            else if(LayerUtils.IsItem(other.gameObject))
            {
                var item = other.gameObject.GetComponent<PickUpWeapon>();
                item.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
    }
}
