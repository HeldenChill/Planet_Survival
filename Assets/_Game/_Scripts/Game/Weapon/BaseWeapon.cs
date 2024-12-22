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
        protected ICharacter source;
        [SerializeField]
        protected float damage;
        [SerializeField]
        protected Transform fireTf;

        public virtual void Equip(ICharacter source)
        {
            this.source = source;
        }

        public virtual void Unequip()
        {
            this.source = null;          
        }
    }
}