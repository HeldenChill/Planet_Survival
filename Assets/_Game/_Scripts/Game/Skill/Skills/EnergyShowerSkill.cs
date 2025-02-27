using _Game.Character;
using DesignPattern;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Utilities.Core;
using Utilities.Timer;

namespace _Game
{
    public class EnergyShowerSkill : BaseSkill
    {
        protected enum POOL_ID
        {
            CAST = 0,
            EXPLOSION = 1,
        }
        private readonly Vector2 DAMAGE_AREA = new Vector2(2.5f, 4.2f);

        private const float CAST_TIME = 0.9f;
        private const int METEOR_COUNT_MAX = 20;
        private const float CHARGE_TIME = 0.5f;

        [SerializeField]
        ParticleSystem Charge;
        [SerializeField]
        float SKILL_RANGE = 6;

        Transform characterTF;

        protected int numOfMeteors;
        protected float betweenMeteorTime = 0.6f;
        Vector3[] randomPos = new Vector3[METEOR_COUNT_MAX];
        Vector3[] randomDirections = new Vector3[METEOR_COUNT_MAX];
        List<STimerData> sTimerDataPools = new List<STimerData>();
        List<STimerData> sTimerDatas = new List<STimerData>();
        FakeGravityBody fakeGravitySource;

        float initExploVFXScale;
        protected STimer shootTimer;
        protected STimer phaseTimer;
        public ObjectContainer objCon;
        public override int SkillLevel
        {
            get => skillLevel;
            set
            {
                base.SkillLevel = value;
                switch (value)
                {
                    case 0:
                    case 1:
                        numOfMeteors = 5;
                        betweenMeteorTime = 0.4f;
                        break;
                    case 2:
                        numOfMeteors = 8;
                        betweenMeteorTime = 0.35f;
                        break;
                    case 3:
                        numOfMeteors = 11;
                        betweenMeteorTime = 0.3f;
                        break;
                    case 4:
                        numOfMeteors = 14;
                        betweenMeteorTime = 0.25f;
                        break;
                    case 5:
                        numOfMeteors = 20;
                        betweenMeteorTime = 0.25f;
                        break;
                }
                if(skillType == typeof(EnergyShowerSkill))
                {
                    UpdateLevelSkillPropertys();
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            phaseTimer = TimerManager.Ins.PopSTimer();
        }
        public override void OnInit(ICharacter source, PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            base.OnInit(source, Parameter, Data);
            this.characterTF = source.Tf;
            objCon.OnInit();
            fakeGravitySource = source.GetVariable<FakeGravityBody>();
            skillType = typeof(EnergyShowerSkill);

            //initExploVFXScale = objCon.Data[(int)POOL_ID.EXPLOSION][0].Tf.localScale.x;
            //UpdateProperty = () =>
            //{
            //    foreach (SkillObjContainer explosion in objCon.Data[(int)POOL_ID.EXPLOSION])
            //    {
            //        explosion.AOEDamage.Damage = SkillData.Damage;
            //        explosion.AOEDamage.EffectDamage = SkillData.Damage;
            //        explosion.AOEDamage.AOERange = SkillData.AOERange;
            //        explosion.AOEDamage.EffectHit = SkillData.EffectHIT / 100;
            //        explosion.AOEDamage.EffectTime = SkillData.EffectTime;
            //        explosion.Tf.localScale = Vector3.one * GetAOEIncreasingRate(initExploVFXScale);
            //    }
            //};
            SkillLevel = 0;
        }

        private void FixedUpdate()
        {
            //if (Charge.isPlaying)
            //{
            //    Charge.transform.position = characterTF.position;
            //}
        }
        public override void SkillExecute()
        {
            base.SkillExecute();
            skillTimer.Start(skillData.CD, SkillActivation, true);
            SkillActivation();
        }

        protected void BeginPhase()
        {
            //Charge?.Play();
        }
        public override void SkillActivation()
        {
            base.SkillActivation();
            BeginPhase();
            RandomPos();
            phaseTimer.Start(sTimerDatas);
        }

        protected void OnActivationLevelUp()
        {
            UpdateLevelSkillPropertys();
        }
        public override void StopExecute()
        {
            objCon.ResetContainer(ObjectContainer.RESET_ACTION.SET_ACTIVE_FALSE);
            skillTimer.Stop();
            phaseTimer.Stop();
        }

        private void ActiveVFX(int posID, POOL_ID id, float timeRelease)
        {
            SkillObjContainer explosion = objCon.Pop((int)id);
            explosion.Tf.position = randomPos[posID];
            explosion.Tf.rotation = Quaternion.FromToRotation(Vector3.up, randomDirections[posID]);
            explosion.Tf.gameObject.SetActive(true);
            TimerManager.Ins.WaitForTime(timeRelease, () => ReleaseVFX(id, explosion));
        }

        private void ReleaseVFX(POOL_ID id, SkillObjContainer objContainer)
        {
            objContainer.Tf.gameObject.SetActive(false);
            objCon.Push((int)id, objContainer);
        }
        private void RandomPos()
        {
            float size = fakeGravitySource.Attractor.WorldSize;
            Vector3 worldPosition = fakeGravitySource.Attractor.Tf.position;
            Vector3 playerPosition = source.Tf.position;

            for (int i = 0; i < numOfMeteors; i++)
            {
                Vector3 rangePos = Random.onUnitSphere.normalized * SKILL_RANGE + playerPosition;
                randomDirections[i] = (rangePos - worldPosition).normalized;
                randomPos[i] = randomDirections[i] * size + worldPosition;
                //DevLog.Log(DevId.Hung, $"POS - {randomPos[i]}");
            }
        }
        protected override void UpdateLevelSkillPropertys()
        {
            int sTimerDataNum = 0;
            for (int i = 0; i < numOfMeteors; i++)
            {
                int index = i;
                STData(i * 2, sTimerDataPools).SetData(CHARGE_TIME + i * betweenMeteorTime, () => ActiveVFX(index, POOL_ID.CAST, betweenMeteorTime + 3f));
                STData(i * 2 + 1, sTimerDataPools).SetData(CHARGE_TIME + i * betweenMeteorTime + CAST_TIME, () => ActiveVFX(index, POOL_ID.EXPLOSION, 3f));
                sTimerDataNum += 2;
            }
            sTimerDataPools.Sort(0, sTimerDataNum, STimerData.Comparer);

            sTimerDatas.Clear();
            for (int i = 0; i < sTimerDataNum; i++)
            {
                sTimerDatas.Add(sTimerDataPools[i]);
            }
        }

        /// <summary>
        /// Reuse STimerData, Use like an array variable 
        /// </summary>
        protected STimerData STData(int index, List<STimerData> sTimerDatas)
        {
            if (index >= sTimerDatas.Count)
            {
                for (int i = sTimerDatas.Count; i <= index; i++)
                {
                    sTimerDatas.Add(new STimerData());
                }
            }

            if (sTimerDatas[index] == null)
            {
                sTimerDatas[index] = new STimerData();
            }
            return sTimerDatas[index];
        }
    }
}
