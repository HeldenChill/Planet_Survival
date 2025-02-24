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

        public override EFFECT Id => EFFECT.FREEZE;

        public override void Initialize()
        {
            timer = TimerManager.Ins.PopSTimer();
            index = 4;
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
                case WoundEffect bleedingEffect:
                    bleedingEffect.Stop();
                    EffectManager.Inst.PushEffect(bleedingEffect);
                    combineEffect = null;
                    isCombining = false;
                    break;
                case BurnEffect burningEffect:
                    Stop();
                    float iceMeltingDamage = (LastDamage + burningEffect.LastDamage * burningEffect.LastTime) * AMP_ICE_MELTING;
                    character.AttachVFXEffect(EFFECT.ICE_MELTING, 1f);
                    if (character.TakeDamage((int)iceMeltingDamage) <= 0) return null;
                    //character.RunTakeDamageAnim(0.3f);
                    break;
                case ZapEffect electricEffect:
                    float damage = electricEffect.LastDamage + LastDamage;
                    character.TakeDamage((int)(damage * AMP_FREEZE_ELECTRIC));
                    //character.RunTakeDamageAnim();
                    Stop();
                    //Cause Damage and Animation Ice Break
                    character.AttachVFXEffect(EFFECT.ICE_BREAKING, 1f);
                    break;
                case WindEffect windEffect:
                    MulTime *= WindEffect.EFFECT_TIME_AMPLIFY_RATE;
                    MulDamage *= WindEffect.EFFECT_DAME_AMPLIFY_RATE;
                    windEffect.Stop();
                    EffectManager.Inst.PushEffect(windEffect);
                    combineEffect = null;
                    isCombining = false;
                    Trigger(character);
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
