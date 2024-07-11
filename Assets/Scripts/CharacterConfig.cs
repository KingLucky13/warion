using UnityEngine;

namespace LearnGame
{
    [CreateAssetMenu(fileName =nameof(CharacterConfig),menuName =nameof(CharacterConfig))]
    public class CharacterConfig:ScriptableObject, ICharacterConfig
    {
        [field: SerializeField]
        public float Hp { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public float RotationSpeed { get; private set; }

        [field: SerializeField]
        public float AccelerationPower { get; private set; }
    }
}
