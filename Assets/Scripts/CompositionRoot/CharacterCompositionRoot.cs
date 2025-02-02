﻿using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.CompositionRoot
{
    public class CharacterCompositionRoot:CompositionRoot<BaseCharacterView>
    {
        [SerializeField]
        private CharacterConfig _characterConfig;

        [SerializeField]
        private BaseCharacterView _view;

        public override BaseCharacterView Compose(ITimer timer)
        {
            IMovementController movementController=new CharacterMovementController(_characterConfig,timer);
            IShootingTarget shootingTarget = new ShootingTargetGO(_view.gameObject);
            var shootingController = new ShootingController(shootingTarget, timer);

            var character=new BaseCharacterModel(movementController, shootingController,_characterConfig);
            _view.Initialize(character);

            return _view;
        }
    }
}
