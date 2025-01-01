using _Game.Character;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Utilities;

namespace _Game
{
    public class DefendOrbSkill : BaseSkill
    {
        [SerializeField]
        List<GameObject> orbs;
        [SerializeField]
        Transform orbsParentTf;

        int numOfBall;
        [SerializeField]
        protected float ballDistance = 2f;
        [SerializeField]
        protected float beginPhaseTime = 0.6f;
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
            SkillActivation();
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
            angle = 360 / numOfBall;
            for (int i = 0; i < numOfBall; i++)
            {
                orbs[i].transform.localPosition = Vector3.zero;
                Vector3 orbDirection = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * ((Vector3)MathHelper.AngleToVector(angle * i));
                orbs[i].transform.DOLocalMove(orbDirection * ballDistance, beginPhaseTime);
                orbs[i].gameObject.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
            if (isActivation)
            {
                orbsParentTf.position = Parameter.Character.Tf.position;
                orbsParentTf.Rotate(Vector3.up * skillData.Speed * Time.fixedDeltaTime * 60, Space.Self);
            }
        }
        protected override void UpdateLevelSkillPropertys()
        {
            
        }
    }
}
