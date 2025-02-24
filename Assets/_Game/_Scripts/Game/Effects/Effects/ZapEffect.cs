using UnityEngine;
using Utilities.Core;

namespace _Game
{
    using Base;
    using DesignPattern;

    public class ZapEffect : BaseEffect
    {
        public override EFFECT Id => EFFECT.ZAP;

        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 1.5f;
            index = 3;
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
                    case FreezeEffect freezeEffect:
                        return true;
                    case WoundEffect bleedingEffect:
                        return true;
                    case PoisonEffect poisonEffect:
                        return false;
                    default:
                        return false;
                }
            }
        }

        public override BaseEffect Trigger(IDamageable character)
        {
            this.character = character;
            if (character == null) return null;

            switch (combineEffect)
            {
                case null:
                    if (character.TakeDamage((int)LastDamage) <= 0) return null;
                    //PoolUtils.Spawn<UIEffectIndicator>(EFFECT_INDICATOR_NAME, character.Tf.position, default).Play(EFFECT.ZAP, character.Tf);
                    character.SpawnHitImpact(HIT_IMPACT_TYPE.NONE);
                    Stop();
                    break;
                case BurnEffect burningEffect:
                    //Cause Explore
                    float overloadDamage = burningEffect.LastDamage * burningEffect.LastTime + LastDamage;
                    AOEDamage overloadExplosion = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV:Dependency
                    overloadExplosion.Damage = overloadDamage;
                    overloadExplosion.Trigger();
                    break;
                case FreezeEffect freezeEffect:
                    //Breaking Ice And Do Damage                  
                    freezeEffect.Trigger(character);

                    if (character.TakeDamage((int)((LastDamage + freezeEffect.LastDamage) * AMP_FREEZE_ELECTRIC)) <= 0) return null;
                    //character.RunTakeDamageAnim();

                    freezeEffect.Stop();
                    EffectManager.Inst.PushEffect(freezeEffect);
                    Stop();
                    break;
                case WoundEffect bleedingEffect:
                    Stop();
                    return bleedingEffect;
            }
            return null;
        }
    }
}
