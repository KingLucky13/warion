﻿using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class WeaponModel
    {
        public event Action<Vector3, WeaponDescription> Shot;

        public WeaponDescription Description { get; }

        public WeaponModel(WeaponDescription description)
        {
            Description = description;
        }

        public void Shoot(Vector3 shotPosition, Vector3 targetPoint) 
        {
          
            Vector3 target=targetPoint-shotPosition;
            target.y = 0;
            target.Normalize();

            Shot?.Invoke(target,Description);
        }
    }
}