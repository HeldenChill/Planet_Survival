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

        public const float AMP_TIME_WIND_RATE = 1.2f;
        public const float AMP_DAME_WIND_RATE = 1.12f;
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
                case EFFECT.FREEZE:
                    switch (sub.Id)
                    {
                        case EFFECT.WOUND:
                            sub.Stop();
                            EffectManager.Inst.PushEffect(sub);
                            break;
                        case EFFECT.BURN:
                            main.Stop();
                            float iceMeltingDamage = (main.LastDamage + sub.LastDamage * sub.LastTime) * AMP_ICE_MELTING;
                            character.AttachVFXEffect(EFFECT.ICE_MELTING, 1f);
                            if (character.TakeDamage((int)iceMeltingDamage) <= 0) return null;
                            //character.RunTakeDamageAnim(0.3f);
                            break;
                        case EFFECT.ZAP:
                            float damage = sub.LastDamage + main.LastDamage;
                            character.TakeDamage((int)(damage * AMP_FREEZE_ELECTRIC));
                            //character.RunTakeDamageAnim();
                            main.Stop();
                            //Cause Damage and Animation Ice Break
                            character.AttachVFXEffect(EFFECT.ICE_BREAKING, 1f);
                            break;
                        case EFFECT.WIND:
                            main.MulTime *= WindEffect.EFFECT_TIME_AMPLIFY_RATE;
                            main.MulDamage *= WindEffect.EFFECT_DAME_AMPLIFY_RATE;
                            sub.Stop();
                            EffectManager.Inst.PushEffect(sub);
                            return main;
                    }
                    break;
                case EFFECT.WIND:
                    switch (sub.Id)
                    {
                        case EFFECT.FREEZE:
                            sub.MulTime *= AMP_TIME_WIND_RATE;
                            sub.MulDamage *= AMP_DAME_WIND_RATE;
                            main.Stop();
                            return sub;
                        case EFFECT.BURN:
                            sub.MulTime *= AMP_TIME_WIND_RATE;
                            sub.MulDamage *= AMP_DAME_WIND_RATE;
                            main.Stop();
                            return sub;
                        case EFFECT.POISON:
                            sub.MulTime *= AMP_TIME_WIND_RATE;
                            sub.MulDamage *= AMP_DAME_WIND_RATE;
                            main.Stop();
                            return sub;
                    }
                    break;
                case EFFECT.POISON:
                    switch (sub.Id)
                    {
                        case EFFECT.BURN:
                            main.Stop();
                            AOEDamage poisonExplode = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV: Dependency
                            poisonExplode.Damage = main.LastDamage;
                            poisonExplode.EffectTime = main.LastTime;
                            poisonExplode.EffectDamage = main.BaseDamage;
                            poisonExplode.Trigger();
                            break;
                        case EFFECT.WIND:
                            main.MulTime *= AMP_TIME_WIND_RATE;
                            main.MulDamage *= AMP_DAME_WIND_RATE;
                            sub.Stop();
                            EffectManager.Inst.PushEffect(sub);
                            //timer.Start(LastTime, Stop);
                            return main;
                    }
                    break;
                case EFFECT.WOUND:
                    switch (sub.Id)
                    {
                        case EFFECT.BURN:
                            //Run VFX Bloodloss
                            float damage = main.LastTime * main.LastDamage + sub.LastTime * sub.LastDamage;
                            if (character.TakeDamage((int)damage) <= 0) return null;
                            //character.RunTakeDamageAnim();
                            main.Stop();
                            character.AttachVFXEffect(EFFECT.BLOOD_LOSS, 1f);
                            break;
                        case EFFECT.ZAP:

                            //Indicator
                            main.Stop();
                            sub.Trigger(character);
                            sub.Stop();
                            EffectManager.Inst.PushEffect(sub);
                            return main;
                        case EFFECT.FREEZE:
                            main.Stop();
                            return sub;
                    }
                    break;
                case EFFECT.ZAP:
                    switch (sub.Id)
                    {
                        case EFFECT.BURN:
                            //Cause Explore
                            float overloadDamage = sub.LastDamage * sub.LastTime + main.LastDamage;
                            AOEDamage overloadExplosion = SimplePool.Spawn<AOEDamage>(PoolType.AOE_DAMAGE, character.Tf.position, Quaternion.identity); //DEV:Dependency
                            overloadExplosion.Damage = overloadDamage;
                            overloadExplosion.Trigger();
                            break;
                        case EFFECT.FREEZE:
                            //Breaking Ice And Do Damage                  
                            sub.Trigger(character);

                            if (character.TakeDamage((int)((main.LastDamage + sub.LastDamage) * AMP_FREEZE_ELECTRIC)) <= 0) return null;
                            //character.RunTakeDamageAnim();

                            sub.Stop();
                            EffectManager.Inst.PushEffect(sub);
                            main.Stop();
                            break;
                        case EFFECT.WOUND:
                            main.Stop();
                            return sub;
                    }
                    break;
            }
            return null;
        }
    }
}
