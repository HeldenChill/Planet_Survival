using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;


namespace _Game
{
    using Base;
    using Utilities.Timer;
    using Utilities;

    [DefaultExecutionOrder(-90)]
    public class EffectManager : MonoBehaviour
    {
        private static EffectManager inst;
        public static EffectManager Inst => inst;
        int InitNum = 50;
        int ReAddNum = 20;
        List<Queue<BaseEffect>> data = new List<Queue<BaseEffect>>();

        private void Awake()
        {
            if (inst == null)
            {
                inst = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            for (int i = 0; i < Enum.GetNames(typeof(EFFECT)).Length; i++)
            {
                data.Add(new Queue<BaseEffect>());
                EFFECT effect = (EFFECT)i;

                for (int j = 0; j < InitNum; j++)
                {
                    data[i].Enqueue(CreateEffect(effect));
                }

            }

        }

        public BaseEffect PopEffect(EFFECT effect)
        {

            if (data[(int)effect].Count <= 0)
            {
                for (int i = 0; i < ReAddNum; i++)
                {
                    data[(int)effect].Enqueue(CreateEffect(effect));
                }
            }
            //if (effect == EFFECT.FREEZE)
            //{
            //    Debug.LogError("=====Pop Effect: FREEZE - " + (data[(int)effect].Count - 1));
            //}
            return data[(int)effect].Dequeue();
        }

        public void PushEffect(BaseEffect effect, bool checkDuplicate = false)
        {
            if (effect == null) return;
            EFFECT effectEnum = EFFECT.FREEZE;
            switch (effect)
            {
                case FreezeEffect freezeEffect:
                    effectEnum = EFFECT.FREEZE;
                    break;
                case WindEffect silentEffect:
                    effectEnum = EFFECT.WIND;
                    break;
                case BurnEffect burningEffect:
                    effectEnum = EFFECT.BURN;
                    break;
                case ZapEffect electricEffect:
                    effectEnum = EFFECT.ZAP;
                    break;
                case WoundEffect bleedingEffect:
                    effectEnum = EFFECT.WOUND;
                    break;
                case PoisonEffect poisonEffect:
                    effectEnum = EFFECT.POISON;
                    break;
            }
            if (checkDuplicate)
            {
                if (data[(int)effectEnum].Contains(effect)) return;
            }
            data[(int)effectEnum].Enqueue(effect);

            if (effectEnum == EFFECT.FREEZE || effectEnum == EFFECT.BURN
                || effectEnum == EFFECT.POISON || effectEnum == EFFECT.WOUND)
            {
                DevLog.Log(DevId.Hung, $"Push Effect: {effectEnum} - {data[(int)effectEnum].Count}");
            }
        }

        private BaseEffect CreateEffect(EFFECT effect)
        {
            BaseEffect effectReturn = null;
            switch (effect)
            {
                case EFFECT.FREEZE:
                    effectReturn = new FreezeEffect();
                    break;
                case EFFECT.WIND:
                    effectReturn = new WindEffect();
                    break;
                case EFFECT.BURN:
                    effectReturn = new BurnEffect();
                    break;
                case EFFECT.ZAP:
                    effectReturn = new ZapEffect();
                    break;
                case EFFECT.WOUND:
                    effectReturn = new WoundEffect();
                    break;
                case EFFECT.POISON:
                    effectReturn = new PoisonEffect();
                    break;
            }
            effectReturn?.Initialize();
            return effectReturn;
        }
        private void OnDestroy()
        {
            TimerManager.Ins.RecallAllSData(); //Recall all STimer class when change scene
        }
    }
}