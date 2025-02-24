using UnityEngine;

namespace _Game
{
    using Base;
    using DesignPattern;

    public static class COMBAT_EFFECT
    {
        public const float INTERVAL_TIME = 0.25f;
        public const float MAX_MUL_DAMAGE = 3.5f;
        public const float MAX_MUL_TIME = 3f;

        public const float AMP_FREEZE_ELECTRIC = 1.5f;
        public const float AMP_ICE_MELTING = 2f;
        public static BaseEffect Combine(BaseEffect main, BaseEffect sub, IDamageable character) 
        {
            if (!main.IsCanCombine(sub)) return sub;
            switch (main.Id)
            {
                case EFFECT.BURN:
                    switch (sub.Id)
                    {
                        case EFFECT.ZAP:
                            //Damage Explosion
                            float overloadDamage = main.LastDamage * main.LastTime + sub.LastDamage;
                            AOEDamage overloadExplosion = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV:Dependency
                            overloadExplosion.Damage = overloadDamage;
                            overloadExplosion.Trigger();
                            main.Stop();
                            break;
                        case EFFECT.FREEZE:
                            main.Stop();
                            float iceMeltingDamage = (main.LastDamage * main.LastTime + sub.LastDamage) * AMP_ICE_MELTING;
                            character.AttachVFXEffect(EFFECT.ICE_MELTING, 1f);
                            if (character.TakeDamage((int)iceMeltingDamage) <= 0) return null;
                            //character.RunTakeDamageAnim(0.3f);
                            break;
                        case EFFECT.WOUND:
                            float bloodlossDamage = main.LastTime * main.LastDamage + sub.LastTime * sub.LastDamage;
                            if (character.TakeDamage((int)bloodlossDamage) <= 0) return null;
                            //character.RunTakeDamageAnim(0.3f);
                            main.Stop();
                            character.AttachVFXEffect(EFFECT.BLOOD_LOSS, 1);
                            break;
                        case EFFECT.POISON:
                            main.Stop();
                            AOEDamage poisonExplode = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV: Dependency
                            poisonExplode.Damage = main.LastDamage;
                            poisonExplode.EffectTime = main.LastTime;
                            poisonExplode.EffectDamage = main.BaseDamage;
                            poisonExplode.Trigger();
                            break;
                        case EFFECT.WIND:
                            main.MulTime *= WindEffect.EFFECT_TIME_AMPLIFY_RATE;
                            main.MulDamage *= WindEffect.EFFECT_DAME_AMPLIFY_RATE;
                            sub.Stop();
                            EffectManager.Inst.PushEffect(sub);
                            return main;
                    }                   
                    break;
            }
            return null;
        }
    }
}
