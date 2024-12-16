using System.Collections;
using System.Collections.Generic;


namespace _Game.Character
{
    using System;
    using UnityEngine;
    using Utilities.Core.Data;
    public class TakeDamageModule : MonoBehaviour, IDamageable
    {
        protected CharacterStats stats;
        public CharacterStats Stats => stats;
        protected Type type;
        public Type Type => type;

        public void OnInit(Type type, CharacterStats stats)
        {
            this.type = type;
            this.stats = stats;
        }
        public float TakeDamage(float damage, object source)
        {
            Stats.Hp.AddModifier(new SStats.StatModifier(damage, SStats.StatModType.Flat, 0, source));
            return Stats.Hp.Value;
        }
    }
}