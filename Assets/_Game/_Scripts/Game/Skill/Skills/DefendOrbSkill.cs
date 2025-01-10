using _Game.Character;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Utilities;
using Utilities.Core;
using Utilities.Timer;
using System;

namespace _Game
{
    public class DefendOrbSkill : BaseSkill
    {
        [SerializeField]
        List<ColliderController> orbs;
        [SerializeField]
        Transform orbsParentTf;
        int numOfBall;
        [SerializeField]
        protected float ballDistance = 2f;
        [SerializeField]
        protected float phaseTime = 0.6f;
        FakeGravityBody fakeGravitySource;
        STimer phaseTimer;
        protected override void Awake()
        {
            base.Awake();
            actions = new List<Action>();
            times = new List<float>();
            phaseTimer = TimerManager.Ins.PopSTimer();

            for(int i = 0; i < orbs.Count; i++)
            {
                orbs[i]._OnTriggerEnter += OnOrbTriggerEnter;
            }
        }
        private void OnDestroy()
        {
            for (int i = 0; i < orbs.Count; i++)
            {
                orbs[i]._OnTriggerEnter -= OnOrbTriggerEnter;
            }
        }
        public override void OnInit(ICharacter source, PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            base.OnInit(source, Parameter, Data);
            skillType = typeof(DefendOrbSkill);
            fakeGravitySource = source.GetVariable<FakeGravityBody>();
            UpdateLevelSkillPropertys();
        }
        public override int SkillLevel { 
            get => base.SkillLevel;
            set
            {
                base.SkillLevel = value;
                switch (value)
                {
                    case 0:
                    case 1:
                        numOfBall = 2;
                        break;
                    case 2:
                        numOfBall = 3;
                        break;
                    case 3:
                        numOfBall = 4;
                        break;
                    case 4:
                        numOfBall = 5;
                        break;
                    case 5:
                        numOfBall = 6;
                        break;
                }
            }
        }
        public override void SkillExecute()
        {
            base.SkillExecute();
            skillTimer.Start(skillData.CD, SkillActivation);
            SkillActivation();
            //SkillActivation();
        }
        public override void SkillActivation()
        {
            if (!Parameter.WIData.IsGrounded)
            {
                skillTimer.Stop();
                return;
            }

            base.SkillActivation();
            orbsParentTf.gameObject.SetActive(true);
            phaseTimer.Start(times, actions, null, STimer.CAL_TYPE.ADD);
        }
        
        protected void BeginPhase()
        {
            float angle;
            float distanceFromAttractor = (source.Tf.position - fakeGravitySource.Attractor.Tf.position).magnitude;

            angle = 360 / numOfBall;
            for (int i = 0; i < numOfBall; i++)
            {
                orbs[i].Tf.localPosition = Vector3.zero;
                Vector3 orbFirstPosition = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * ((Vector3)MathHelper.AngleToVector(angle * i)) * ballDistance;
                Vector3 originLocalPos = source.Tf.InverseTransformPoint(fakeGravitySource.Attractor.Tf.position);
                Vector3 orbDirection = (orbFirstPosition - originLocalPos).normalized;
                Vector3 orbPosition = orbDirection * distanceFromAttractor + originLocalPos;

                orbs[i].Tf.DOLocalMove(orbPosition, phaseTime);
                orbs[i].gameObject.SetActive(true);
            }
        }
        protected void EndPhase()
        {
            for(int i = 0; i < numOfBall; i++)
            {
                orbs[i].Tf.DOLocalMove(Vector3.zero, phaseTime);
            }
        }
     
        public override void StopActivation()
        {
            base.StopActivation();
            for (int i = 0; i < numOfBall; i++)
            {
                orbs[i].gameObject.SetActive(false);
            }
        }
        public override void StopExecute()
        {
            base.StopExecute();
            skillTimer.Stop();
            phaseTimer.Stop();
        }
        private void FixedUpdate()
        {
            if(isExecute && !skillTimer.IsStart)
            {
                if (Parameter.WIData.IsGrounded)
                {
                    SkillExecute();
                }
            }
            if (isActivation)
            {
                orbsParentTf.position = source.Tf.position;
                tf.rotation = source.Tf.rotation;
                orbsParentTf.Rotate(Vector3.up * skillData.Speed * Time.fixedDeltaTime * 60 / ballDistance, Space.Self);

                //string content = "";
                //for(int i = 0; i < numOfBall; i++)
                //{
                //    content += $"{(orbs[i].Tf.position - fakeGravitySource.Attractor.Tf.position).magnitude} ~ ";
                //}
                //DevLog.Log(DevId.Hung, content);
            }
        }
        protected override void UpdateLevelSkillPropertys()
        {
            actions.Clear();
            times.Clear();

            actions.Add(BeginPhase);
            times.Add(0);

            actions.Add(EndPhase);
            times.Add(skillData.ExistTime);

            actions.Add(StopActivation);
            times.Add(phaseTime);
        }
        protected void OnOrbTriggerEnter(ColliderController main, Collider target)
        {
            int characterMask = LayerMask.NameToLayer(Base.CONSTANTS.ENEMY_COLLIDER_LAYER);
            if (target.gameObject.layer == characterMask)
            {
                IDamageable enemy = target.gameObject.GetComponent<IDamageable>();
                if (enemy.Type == typeof(Enemy))
                {
                    float value = enemy.TakeDamage(-skillData.Damage, source);
                }
            }
        }
    }
}
