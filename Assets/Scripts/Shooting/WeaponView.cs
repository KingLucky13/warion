using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform BulletSpawnPosition { get; private set; }

        [SerializeField]
        private BulletView _bulletPrefab;

        [SerializeField]
        private ParticleSystem _shootParticle;

        [SerializeField]
        private AudioSource _shootAudio;

        public WeaponModel Model { get; private set; }

        public void Initialize(WeaponModel model)
        {
            if (Model != null) {
                Debug.LogWarning("Weapon model has already been initialized");
                return;
            }
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Model = model;
            Model.Shot += Shoot;
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Shot -= Shoot;
        }
        public void Shoot(Vector3 targetDirection,WeaponDescription description) 
        {
            BulletView bullet = Instantiate(_bulletPrefab, BulletSpawnPosition.position, Quaternion.identity);
            ITimer timer=new UnityTimer();
            var model = new BulletModel(description,targetDirection,timer);
            bullet.Initialize(model);

            _shootParticle.Play();
            _shootAudio.Play();
        }
    }
}