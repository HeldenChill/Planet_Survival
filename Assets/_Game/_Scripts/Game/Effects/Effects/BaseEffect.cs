using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    using Base;
    using Utilities.Core;
    public abstract class BaseEffect
    {
        public const float INTERVAL_TIME = 0.25f;
        public const float MAX_MUL_DAMAGE = 3.5f;
        public const float MAX_MUL_TIME = 3f;

        protected const float AMP_FREEZE_ELECTRIC = 1.5f;
        protected const float AMP_ICE_MELTING = 2f;

        public event System.Action<BaseEffect> _OnEffectStop;

        #region Property       
        public float BaseDamage = 100;
        public float BaseTime;

        protected int index = -1;
        protected float PROPERTY_MUL_DAMAGE = 1f;
        protected float PROPERTY_MUL_TIME = 1f;
        protected float mulDamage = 1f;
        protected float mulTime = 1f;

        public float LastDamage => BaseDamage * mulDamage * PROPERTY_MUL_DAMAGE;
        public float LastTime => BaseTime * mulTime * PROPERTY_MUL_TIME;
        public float MulDamage
        {
            get => mulDamage;
            set
            {
                mulDamage = Mathf.Clamp(value, 0f, MAX_MUL_DAMAGE);
            }
        }
        public float MulTime
        {
            get => mulTime;
            set
            {
                mulTime = Mathf.Clamp(value, 0f, MAX_MUL_TIME);
            }
        }
        public int Index => index;
        #endregion
        public abstract EFFECT Id { get; }
        public bool IsCombining => isCombining;
        protected IDamageable character;

        protected bool isCombining = false;
        protected BaseEffect combineEffect;
        public abstract void Initialize(); // Init

        public abstract BaseEffect Trigger(IDamageable character); //Trigger Effect
        public virtual void Stop() //Stop Effect
        {
            _OnEffectStop?.Invoke(this);
            if (combineEffect != null)
            {
                combineEffect.Stop();
                //EffectManager.Inst.PushEffect(combineEffect);
            }
            mulTime = 1f;
            mulDamage = 1f;
            combineEffect = null;
            isCombining = false;
            character = null;
        }
        public virtual void AddEffect(BaseEffect effect) // Combine Effect Processing
        {
            /*
            Recalculate Dealing Damage 
            Relculate Remaining Time 
            Calculate Controlling Status(Breaking ice...)
            Calculate can add effect or not
            */
            if (isCombining) return;
            combineEffect = effect;
            isCombining = true;
        }

        public abstract bool IsCanCombine(BaseEffect effect);

    }
}