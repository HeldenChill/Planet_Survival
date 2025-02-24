using System.Collections;
using System.Collections.Generic;


namespace _Game.Character
{
    using Base;
    using System;
    using UnityEngine;
    using Utilities.Core.Data;
    public class TakeDamageModule : MonoBehaviour, IDamageable
    {
        protected CharacterStats stats;
        public CharacterStats Stats => stats;
        protected Type type;
        [SerializeField]
        protected Transform tf;
        public Type Type => type;
        public Transform Tf => tf;

        public void OnInit(Type type, CharacterStats stats)
        {
            this.type = type;
            this.stats = stats;
        }
        public float TakeDamage(float damage, object source = null)
        {
            Stats.Hp.AddModifier(new SStats.StatModifier(damage, SStats.StatModType.Flat, 0, source));
            return Stats.Hp.Value;
        }

        public void AttachVFXEffect(EFFECT effect, float time = -1) 
        { 

        }

        public void DetachVFXEffect(EFFECT effect)
        {

        }

        public void SpawnHitImpact(HIT_IMPACT_TYPE type)
        {
            
        }
    }
}