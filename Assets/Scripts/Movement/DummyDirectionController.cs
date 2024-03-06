using UnityEngine;

namespace LearnGame.Movement
{
    public class DummyDirectionController : MonoBehaviour,IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }

        // Use this for initialization
        protected void Awake()
        {
            MovementDirection= Vector3.zero;
        }

        // Update is called once per frame
        protected void Update()
        {   
        }
    }
}