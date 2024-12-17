using Dynamic.WorldInterface.Data;
using System.Collections;
using System.Collections.Generic;

namespace _Game
{
    using UnityEngine;
    using Utilities.Core;
    using Utilities.Core.Character.WorldInterfaceSystem;
    public abstract class BaseWeapon : BaseSkill
    {
        protected WorldInterfaceData data;
        protected WorldInterfaceModule module;
        protected ICharacter source;
        [SerializeField]
        protected float damage;
        [SerializeField]
        protected Transform fireTf;

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