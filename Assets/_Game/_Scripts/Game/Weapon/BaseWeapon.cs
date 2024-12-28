using Dynamic.WorldInterface.Data;
using System.Collections;
using System.Collections.Generic;

namespace _Game
{
    using UnityEngine;
    using Utilities.Core;
    public abstract class BaseWeapon : BaseSkill
    {
        protected ICharacter source;
        [SerializeField]
        protected float damage;
        [SerializeField]
        protected Transform fireTf;

        public virtual void Equip(ICharacter source, Transform trackingTf = null)
        {
            this.source = source;
        }

        public virtual void Unequip()
        {
            this.source = null;          
        }
    }
}