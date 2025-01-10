using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace _Game
{
    using Utilities.Physics;
    public class AOEDamage : AOEDetection<IDamageable>
    {
        public enum EFFECT
        {
            FREEZE = 0,
            WIND = 1,
            BURN = 2,
            ZAP = 3,
            WOUND = 4,
            WEAKEN = 5,
            POISON = 6,
            CHANGE_TEMP = 7,
        }
        public enum COMBINE_EFFECT
        {
            BLOOD_LOSS = 0, //WOUND + BURN
            OVERLOAD = 1, //BURN + ZAP
            ICE_BREAKING = 2, //ZAP + FREEZE
            ICE_MELTING = 3, //FREEZE + BURN
        }
        [Serializable]
        protected class EffectData
        {
            public EFFECT Type;
            public float Time;
            public float Damage;
            public EffectData(EFFECT type, float time, float damage)
            {
                this.Type = type;
                this.Time = time;
                Damage = damage;
            }
        }
        [SerializeField]
        protected string ENEMY_LAYER = "EnemyCollider";

        #region Property
        [SerializeField]
        List<EffectData> effects;
        public List<float> EffectTimes
        {
            set
            {
                if (effects == null) return;
                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].Time = value[i];
                }
            }
        }
        public List<float> EffectDamages
        {
            set
            {
                if (effects == null) return;
                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].Damage = value[i] / numOfExplode;
                }
            }
        }
        [HideInInspector]
        public float EffectTime
        {
            set
            {
                if (effects.Count == 0) return;
                effects[0].Time = value;
            }
        }
        public float EffectDamage
        {
            set
            {
                if (effects.Count == 0) return;
                effects[0].Damage = value / numOfExplode;
            }
        }
        [HideInInspector]
        public float EffectHit = 1;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            switch (space)
            {
                case SPACE.D2:
                    targetDetection = new Target2DDetection<IDamageable>(new string[] { ENEMY_LAYER }, null, 30);
                    break;
                case SPACE.D3:
                    targetDetection = new Target3DDetection<IDamageable>(new string[] { ENEMY_LAYER }, null, 30);
                    break;
            }
        }
        protected virtual void OnEnable()
        {
            if (autoTrigger)
            {
                Trigger();
            }
            else
            {
                for (int i = 0; i < particleSystems.Length; i++)
                {
                    particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }


        protected override void DealDamage(int damage, float aoeRange)
        {
            aoeRangeTemp = aoeRange;
            targetDetection.CheckTargets(explodeTF.position, aoeRange, ref targets);
            if (type == TYPE.INCREASE_SINGLE_EXPLODE) //NOTE: Remove all targets which be affected already and add new target that be affected
            {
                targets.RemoveAll(x => oldTargets.Exists(y => x == y));
                oldTargets.AddRange(targets);
            }


            for (int i = 0; i < targets.Count; i++)
            {
                if (damage > 0)
                    targets[i].TakeDamage(-damage, this);
                for (int j = 0; j < effects.Count; j++)
                {
                    InflictEffect(effects[j], targets[i]);
                }
                //targets[i].RunTakeDamageAnim();
                //targets[i].SpawnHitImpact(hitImpactType);
            }

        }

        public void AddEffect(EFFECT effectType, float effectTime, float effectDamage)
        {
            EffectData effData = new EffectData(effectType, effectTime, effectDamage);
            effects.Add(effData);
        }

        public void RemoveAllEffect()
        {
            effects.Clear();
        }

        protected virtual void InflictEffect(EffectData effect, IDamageable enemy)
        {
            if (enemy == null) return;
            //if (Utilities.PercentRandom(EffectHit))
            //{
            //    enemy?.StartEffect(effect.Type, effect.Time, effect.Damage);
            //}
        }
    }
}