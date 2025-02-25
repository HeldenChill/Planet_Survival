using UnityEngine;
using Utilities.Core;

namespace _Game
{
    using Base;
    using DesignPattern;
    using Utilities.Timer;

    public class WindEffect : BaseEffect
    {
        public const float EFFECT_TIME_AMPLIFY_RATE = 1.2f;
        public const float EFFECT_DAME_AMPLIFY_RATE = 1.12f;
        STimer timer;
        public override void Initialize()
        {
            timer = TimerManager.Ins.PopSTimer();
            id = EFFECT.WIND;
        }

        public override BaseEffect Trigger(IDamageable character)
        {
            this.character = character;
            if (character == null) return null;
            switch (combineEffect)
            {
                case null:
                    character.AttachVFXEffect(EFFECT.WIND);
                    //SimplePool.Spawn<UIEffectIndicator>(EFFECT_INDICATOR_NAME, character.Tf.position, default).Play(EFFECT.WIND, character.Tf);
                    timer.Start(LastTime, Stop);
                    break;
            }
            return null;
        }

        public override void Stop()
        {
            timer.Stop();
            if (character != null)
            {
                character.DetachVFXEffect(EFFECT.WIND);
            }
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
                    case BurnEffect burningEffect:
                        return true;
                    case ZapEffect electricEffect:
                        return false;
                    case FreezeEffect freezeEffect:
                        return true;
                    case WoundEffect bleedingEffect:
                        return false;
                    case PoisonEffect poisonEffect:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
