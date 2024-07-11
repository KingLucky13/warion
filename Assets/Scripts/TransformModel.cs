﻿using UnityEngine;

namespace LearnGame
{
    public class TransformModel
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public TransformModel(Vector3 position,Quaternion rotation) {
            Position= position;
            Rotation= rotation;
        }
        public TransformModel(Vector3 position) {
            Position = position;
        }
    }
}
