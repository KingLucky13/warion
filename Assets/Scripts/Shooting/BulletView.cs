using UnityEngine;

namespace LearnGame.Shooting
{
    public class BulletView : MonoBehaviour
    {
        public BulletModel Model { get; private set; }

        public void Initialize(BulletModel model)
        {
            Model = model;
            model.Initialize(transform.position, transform.rotation);
            Model.Disappear += OnDisappear;
        }

        protected void Update()
        {
            Model.Move();
            transform.position = Model.Transform.Position;
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Disappear -= OnDisappear;
        }

        private void OnDisappear()
        {
            Destroy(gameObject);
        }

    }
}