using UnityEngine;

namespace _Game
{
    using Base;
    using DesignPattern;
    using Utilities.Timer;
    public class BurnEffect : BaseEffect
    {
        private float currentTime;
        STimer timer;

        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 1f;
            PROPERTY_MUL_TIME = 1.5f;
            timer = TimerManager.Ins.PopSTimer();
            id = EFFECT.BURN;
        }
        public override BaseEffect Trigger(IDamageable character)
        {
            this.character = character;
            if (character == null) return null;

            switch (combineEffect)
            {
                case null:
                    currentTime = 0;
                    character.AttachVFXEffect(EFFECT.BURN);
                    //PoolUtils.Spawn<UIEffectIndicator>(EFFECT_INDICATOR_NAME, character.Tf.position, default).Play(EFFECT.BURN, character.Tf);
                    timer.Start(INTERVAL_TIME, () =>
                    {
                        if (character.TakeDamage((int)(LastDamage * INTERVAL_TIME)) <= 0) return;
                        //character.RunTakeDamageAnim(0.1f);

                        if (currentTime > LastTime)
                        {
                            Stop();
                        }
                        currentTime += INTERVAL_TIME;
                    }, true);
                    break;              
            }
            return null;
        }
        public override void Stop()
        {
            character?.DetachVFXEffect(EFFECT.BURN);
            character?.DetachVFXEffect(EFFECT.BLOOD_LOSS);
            character?.DetachVFXEffect(EFFECT.ICE_MELTING);
            timer.Stop();
            base.Stop();
        }

        public override bool IsCanCombine(BaseEffect effect)
        {
            if (effect.Index < Index)
            {
                return effect.IsCanCombine(this);
            }
            else if (effect.Index == Index)
            {
                return false;
            }
            else
            {
                switch (effect)
                {
                    case ZapEffect electricEffect:
                        return true;
                    case FreezeEffect freezeEffect:
                        return true;
                    case WoundEffect bleedingEffect:
                        return true;
                    case PoisonEffect poisonEffect:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
