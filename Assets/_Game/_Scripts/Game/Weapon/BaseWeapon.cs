using Dynamic.WorldInterface.Data;
using System.Collections;
using System.Collections.Generic;

namespace _Game
{
    using UnityEngine;
    using Utilities.Core;
    using Utilities.Core.Character.WorldInterfaceSystem;
    public abstract class BaseWeapon : MonoBehaviour
    {
        protected WorldInterfaceData data;
        protected WorldInterfaceModule module;
        protected ICharacter source;
        [SerializeField]
        protected float damage;

        public abstract void Fire();
        public virtual void Equip(WorldInterfaceModule module, WorldInterfaceData data, ICharacter source)
        {
            this.data = data;
            this.module = module;
            this.source = source;
        }

        public virtual void Unequip()
        {
            data = null;
            module = null;           
        }
    }
}