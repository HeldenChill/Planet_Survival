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

        public Transform Tf { 
            get => characterTransform;

            set 
            { 
                characterTransform = value;
            }
        }
    }
}