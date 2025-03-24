using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace Utilities.Core.Character
{
    public class PerceptionData 
    {
        private Transform characterTransform;
        private Transform characterSkinTransform;
        private bool isFaceRight = true;
       
        public void Initialize(Transform characterTransform, Transform characterSkinTransform)
        {
            this.characterTransform = characterTransform;
            this.characterSkinTransform = characterSkinTransform;
        }
        public bool IsFaceRight { 
            get => isFaceRight; 
        }
        public Transform Tf => characterTransform;
        public Transform SkinTf => characterSkinTransform;
    }
}