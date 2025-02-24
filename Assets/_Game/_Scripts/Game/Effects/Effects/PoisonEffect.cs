using Utilities.Core;

namespace _Game
{
    using Base;
    using DesignPattern;
    using UnityEngine;
    using Utilities.Timer;

    public class PoisonEffect : BaseEffect
    {
        private float currentTime;
        STimer timer;

        public override EFFECT Id => EFFECT.POISON;

        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 0.75f;
            index = 6;
            timer = TimerManager.Ins.PopSTimer();
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
                return false;
            }
        }

        public override BaseEffect Trigger(IDamageable character)
        {
            this.character = character;
            if (character == null) return null;

            switch (combineEffect)
            {
                case null:
                    currentTime = 0;
                    character.AttachVFXEffect(EFFECT.POISON);
                    //PoolUtils.Spawn<UIEffectIndicator>(EFFECT_INDICATOR_NAME, character.Tf.position, default).Play(EFFECT.POISON, character.Tf);
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
                    Stop();
                    AOEDamage poisonExplode = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV: Dependency
                    poisonExplode.Damage = LastDamage;
                    poisonExplode.EffectTime = LastTime;
                    poisonExplode.EffectDamage = BaseDamage;
                    poisonExplode.Trigger();
                    break;
                case WindEffect windEffect:
                    MulTime *= WindEffect.EFFECT_TIME_AMPLIFY_RATE;
                    MulDamage *= WindEffect.EFFECT_DAME_AMPLIFY_RATE;
                    windEffect.Stop();
                    EffectManager.Inst.PushEffect(windEffect);
                    combineEffect = null;
                    isCombining = false;
                    //timer.Start(LastTime, Stop);
                    Trigger(character);
                    break;
            }
            return null;
        }

        public override void Stop()
        {
            character?.DetachVFXEffect(EFFECT.POISON);
            timer.Stop();
            base.Stop();
        }
    }
}
