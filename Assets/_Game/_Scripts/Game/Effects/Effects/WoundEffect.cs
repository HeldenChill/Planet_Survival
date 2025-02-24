using UnityEngine;


namespace _Game
{
    using Base;
    using Utilities.Core;
    using Utilities.Timer;
    public class WoundEffect : BaseEffect
    {
        private float currentTime;
        STimer timer;

        public override EFFECT Id => EFFECT.WOUND;

        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 2f;
            timer = TimerManager.Ins.PopSTimer();
            index = 5;
        }

        public override BaseEffect Trigger(IDamageable character)
        {
            this.character = character;
            if (character == null) return null;

            switch (combineEffect)
            {
                case null:
                    currentTime = 0;
                    character.AttachVFXEffect(EFFECT.WOUND);
                    //PoolUtils.Spawn<UIEffectIndicator>(EFFECT_INDICATOR_NAME, character.Tf.position, default).Play(EFFECT.WOUND, character.Tf);
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
                case BurnEffect burningEffect:
                    //Run VFX Bloodloss
                    float damage = LastTime * LastDamage + burningEffect.LastTime * burningEffect.LastDamage;
                    if (character.TakeDamage((int)damage) <= 0) return null;
                    //character.RunTakeDamageAnim();
                    Stop();
                    character.AttachVFXEffect(EFFECT.BLOOD_LOSS, 1f);
                    break;
                case ZapEffect electricEffect:

                    //Indicator
                    timer.Stop();
                    currentTime = 0;
                    electricEffect.Trigger(character);
                    electricEffect.Stop();
                    EffectManager.Inst.PushEffect(electricEffect);

                    combineEffect = null;
                    isCombining = false;

                    Trigger(character);
                    break;
                case FreezeEffect freezeEffect:
                    combineEffect = null;
                    isCombining = false;
                    Stop();
                    return freezeEffect;
            }
            return null;
        }

        public override void Stop()
        {
            character?.DetachVFXEffect(EFFECT.WOUND);
            character?.DetachVFXEffect(EFFECT.BLOOD_LOSS);
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
                    case PoisonEffect poisonEffect:
                        return false;
                    default:
                        return false;
                }
            }
        }
    }
}
