using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame.Pickup
{
    public class PickUpWeapon : MonoBehaviour
    {
        [field:SerializeField]
        public Weapon WeaponPrefab { get; private set; }

    }
}