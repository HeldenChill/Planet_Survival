using UnityEngine;

namespace _Game
{
    using Base;
    using Utilities.Timer;

    public class FreezeEffect : BaseEffect
    {
        STimer timer;
        float initEnemyBulletSpeed;
        public const float SLOW_BULLET_SPEED_RATE = 0.5f;

        public override void Initialize()
        {
            timer = TimerManager.Ins.PopSTimer();
            id = EFFECT.FREEZE;
        }

        public override BaseEffect Trigger(IDamageable character)
        {
            this.character = character;
            if (character == null) return null;

            switch (combineEffect)
            {
                case null:                    
                    timer.Start(LastTime, Stop);
                    //PoolUtils.Spawn<UIEffectIndicator>(EFFECT_INDICATOR_NAME, character.Tf.position, default).Play(EFFECT.FREEZE, character.Tf);
                    break;               
            }
            return null;
        }

        public override void Stop()
        {
            character.DetachVFXEffect(EFFECT.ICE_BREAKING);
            character.DetachVFXEffect(EFFECT.ICE_MELTING);
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
                    case WoundEffect bleedingEffect:
                        return true;
                    case PoisonEffect poisonEffect:
                        return false;
                    default:
                        return false;
                }
            }
        }
    }
}
