using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame.Pickup
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private WeaponFactory _weaponFactory;

        public override void PickUp(BaseCharacterView character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponFactory);
        }
    }
}