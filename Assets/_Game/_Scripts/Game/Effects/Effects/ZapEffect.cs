using UnityEngine;
using Utilities.Core;

namespace _Game
{
    using Base;
    using DesignPattern;

    public class ZapEffect : BaseEffect
    {
        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 1.5f;
            id = EFFECT.ZAP;
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
            }
            return null;
        }
    }
}
