using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace Utilities.Core.Character
{
    public class CharacterParameterData 
    {
        private Transform characterTransform;
        private bool isFaceRight = true;
       
        public void Initialize(Transform characterTransform)
        {
            this.characterTransform = characterTransform;
        }
        public bool IsFaceRight { 
            get => isFaceRight; 
        }

        public Vector2 FaceDirection
        {
            get
            {
                if (isFaceRight)
                {
                    return Vector2.right;
                }
                else
                {
                    return Vector2.left;
                }
            }

            set
            {
                if(value.x > 0)
                {
                    isFaceRight = true;
                }
                else
                {
                    isFaceRight = false;
                }
            }
        }
        public Vector3 RbVelocity;

        public Transform Tf { 
            get => characterTransform;

            set 
            { 
                characterTransform = value;
            }
        }
    }
}