using _Game.Character;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Utilities;
using Utilities.Core;
using Utilities.Timer;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

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
        protected float beginPhaseTime = 0.6f;
        FakeGravityBody fakeGravitySource;
        protected override void Awake()
        {
            base.Awake();
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
            fakeGravitySource = source.GetVariable<FakeGravityBody>();
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
            skillTimer.Start(1f, SkillActivation);
            //SkillActivation();
        }
        public override void SkillActivation()
        {
            base.SkillActivation();
            orbsParentTf.gameObject.SetActive(true);
            BeginPhase();
        }
        protected void BeginPhase()
        {
            float angle;
            float distanceFromAttractor = (source.Tf.position - fakeGravitySource.Attractor.Tf.position).magnitude;
            DevLog.Log(DevId.Hung, distanceFromAttractor.ToString());

            angle = 360 / numOfBall;
            for (int i = 0; i < numOfBall; i++)
            {
                orbs[i].Tf.localPosition = Vector3.zero;
                Vector3 orbFirstPosition = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * ((Vector3)MathHelper.AngleToVector(angle * i)) * ballDistance;
                Vector3 orbDirection = (orbFirstPosition - source.Tf.InverseTransformPoint(fakeGravitySource.Attractor.Tf.position)).normalized;
                Vector3 orbPosition = orbDirection * distanceFromAttractor;

                orbs[i].Tf.localPosition = orbFirstPosition;
                //orbs[i].Tf.DOLocalMove(orbFirstPosition, beginPhaseTime);
                orbs[i].gameObject.SetActive(true);
            }
        }
        private void FixedUpdate()
        {
            if (isActivation)
            {
                orbsParentTf.position = source.Tf.position;
                orbsParentTf.Rotate(Vector3.up * skillData.Speed * Time.fixedDeltaTime * 60, Space.Self);

                string content = "";
                for(int i = 0; i < numOfBall; i++)
                {
                    content += $"{(orbs[i].Tf.position - fakeGravitySource.Attractor.Tf.position).magnitude} ~ ";
                }
                DevLog.Log(DevId.Hung, content);
            }
        }
        protected override void UpdateLevelSkillPropertys()
        {
            
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
