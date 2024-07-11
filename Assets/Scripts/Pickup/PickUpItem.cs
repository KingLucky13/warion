using System;
using UnityEngine;

namespace LearnGame.Pickup
{
    public abstract class PickUpItem : MonoBehaviour
    {
        public event Action<PickUpItem> OnPickedUp;

        public virtual void PickUp(BaseCharacterView character)
        {
            OnPickedUp?.Invoke(this);
        }
    }
}