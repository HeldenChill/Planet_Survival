using UnityEngine;

namespace _Game
{
    using Base;
    using DesignPattern;
    using Utilities.Timer;
    public class BurnEffect : BaseEffect
    {
        public override EFFECT Id => EFFECT.BURN;
        private float currentTime;
        STimer timer;

        public override void Initialize()
        {
            PROPERTY_MUL_DAMAGE = 1f;
            PROPERTY_MUL_TIME = 1.5f;
            timer = TimerManager.Ins.PopSTimer();
            index = 2;
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
                case ZapEffect electricEffect:
                    //Damage Explosion
                    float overloadDamage = LastDamage * LastTime + electricEffect.LastDamage;
                    AOEDamage overloadExplosion = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV:Dependency
                    overloadExplosion.Damage = overloadDamage;
                    overloadExplosion.Trigger();
                    Stop();
                    break;
                case FreezeEffect freezeEffect:
                    Stop();
                    float iceMeltingDamage = (LastDamage * LastTime + freezeEffect.LastDamage) * AMP_ICE_MELTING;
                    character.AttachVFXEffect(EFFECT.ICE_MELTING, 1f);
                    if (character.TakeDamage((int)iceMeltingDamage) <= 0) return null;
                    //character.RunTakeDamageAnim(0.3f);
                    break;
                case WoundEffect bleedingEffect:
                    float bloodlossDamage = LastTime * LastDamage + bleedingEffect.LastTime * bleedingEffect.LastDamage;
                    if (character.TakeDamage((int)bloodlossDamage) <= 0) return null;
                    //character.RunTakeDamageAnim(0.3f);
                    Stop();
                    character.AttachVFXEffect(EFFECT.BLOOD_LOSS, 1);
                    break;
                case PoisonEffect poisonEffect:
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
                    Trigger(character);
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
