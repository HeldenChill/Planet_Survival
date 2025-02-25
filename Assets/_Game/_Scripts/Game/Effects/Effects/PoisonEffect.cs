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
        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 0.75f;
            timer = TimerManager.Ins.PopSTimer();
            id = EFFECT.POISON;
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
