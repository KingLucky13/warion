using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace LearnGame.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _rotation_speed = 10f;
        private static readonly float SqrtEps = Mathf.Epsilon *Mathf.Epsilon;
        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }
        private CharacterController _characterController;
        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();
            if(_rotation_speed>0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }
        }
        private void Translate()
        {
            Vector3 delta = MovementDirection * _speed * Time.deltaTime;
            _characterController.Move(delta);
        }
        private void Rotate()
        {
            var _currentRotation = transform.rotation*Vector3.forward;
            float sqrtMagnitude = (_currentRotation - LookDirection).sqrMagnitude;
            if (sqrtMagnitude > SqrtEps)
            {
                Quaternion newRotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(LookDirection,Vector3.up),_rotation_speed*Time.deltaTime);
                transform.rotation= newRotation;
            }
        }
    }
}
