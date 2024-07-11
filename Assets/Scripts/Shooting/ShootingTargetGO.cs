using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingTargetGO:IShootingTarget
    {
        private readonly Collider[] colliders=new Collider[2];
        private readonly GameObject _shooter;

        public ShootingTargetGO(GameObject shooter)
        {
            _shooter = shooter;
        }
        public BaseCharacterModel GetTarget(Vector3 position, float radius)
        {
            BaseCharacterModel target = null;

            int mask = LayerUtils.EnemyMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, colliders, mask);
            if (size > 1)
            {
                for (int i = 0; i < size; i++)
                {
                    if (colliders[i].gameObject != _shooter)
                    {
                        target = colliders[i].gameObject.GetComponent<BaseCharacterView>().Model;
                        break;
                    }
                }
            }
            return target;
        }

    }
}
