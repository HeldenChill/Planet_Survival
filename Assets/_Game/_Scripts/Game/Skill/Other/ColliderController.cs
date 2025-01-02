using DesignPattern;
using System;
using UnityEngine;

namespace _Game
{
    public class ColliderController : GameUnit
    {
        public Action<ColliderController, Collider> _OnTriggerEnter;
        private void OnTriggerEnter(Collider other)
        {
            _OnTriggerEnter?.Invoke(this, other);
        }
    }
}
