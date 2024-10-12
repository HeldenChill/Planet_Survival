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


        public void SetSkinRotation(Quaternion rotation)
        {
            skinTf.rotation = rotation;
            sensorTf.rotation = rotation;
        }
    }
}