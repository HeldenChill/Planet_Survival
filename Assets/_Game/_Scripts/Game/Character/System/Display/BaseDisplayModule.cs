using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    public class BaseDisplayModule : MonoBehaviour
    {
        [SerializeField]
        Transform skinTf;
        [SerializeField]
        Transform sensorTf;
        [SerializeField]
        Animator animator;


        public void SetSkinRotation(Quaternion rotation)
        {
            skinTf.rotation = rotation;
            sensorTf.rotation = rotation;
        }

        public void SetSkinLocalRotation(Quaternion rotation)
        {
            skinTf.localRotation = rotation;
            sensorTf.localRotation = rotation;
        }
        public void SetAnimTrigger(Type type, string name)
        {
            animator.SetTrigger(name);
        }

        public void SetAnimBool(Type type, string name, bool value)
        {
            animator.SetBool(name, value);
        }
    }
}