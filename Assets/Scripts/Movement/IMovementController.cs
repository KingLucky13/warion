
using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovementController
    {

        Vector3 Translate(Vector3 movementDirection);

        Quaternion Rotate(Quaternion currentRotation,Vector3 lookDirection);
        void setSpeedBoost(float time);
    }
}
