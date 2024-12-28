using UnityEngine;
using Utilities;

namespace _Game
{
    using _Game.Character;
    using DesignPattern;
    using DG.Tweening;
    using System;
    using System.Collections.Generic;
    using Utilities.Core;
    using Utilities.Timer;

    public class PlayerWeapon : BaseWeapon
    {
        int shootNum = 1;
        float betweenShootTime = 0.05f;
        float weaponRotateTime = 0.1f;

        Collider target;
        Quaternion rotToTarget;
        Vector3 shootDirection;
        Transform trackingTf;

        List<Action> actions;
        List<float> times;
        STimer activationTimer;
        protected override void Awake()
        {
            base.Awake();
            actions = new List<Action>();
            times = new List<float>();
            activationTimer = TimerManager.Ins.PopSTimer();
        }
        public override int SkillLevel 
        { 
            get => base.SkillLevel; 
            set
            {
                base.SkillLevel = value;
                switch (skillLevel)
                {
                    case 0:
                    case 1:
                        shootNum = 1;
                        break;
                    case 2:
                        shootNum = 2;
                        break;
                    case 3:
                        shootNum = 3;
                        break;
                    case 4:
                        shootNum = 4;
                        break;
                    case 5:
                        shootNum = 5;
                        break;
                }
                if(skillType == typeof(PlayerWeapon))
                {
                    UpdateLevelSkillPropertys();
                }
            }
        }
        public override void OnInit(PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            base.OnInit(Parameter, Data);
            skillType = typeof(PlayerWeapon);
        }
        public override void Equip(ICharacter source, Transform trackingTf = null)
        {
            base.Equip(source);
            if(trackingTf != null)
            {
                Tf.parent = null;
                this.trackingTf = trackingTf;
            }
        }
        public override void SkillExecute()
        {
            base.SkillExecute();
            SkillActivation();
            skillTimer.Start(1f, SkillActivation, true);
        }
        public override void SkillActivation()
        {
            target = FindTarget();
            if(target == null)
            {
                skillTimer.Stop();
                target = null;
                return;
            }
            rotToTarget = ShootDirection(target.transform.position);
            activationTimer.Start(times, actions);
        }
        private void FixedUpdate()
        {
            if(trackingTf != null)
            {
                tf.position = Vector3.Lerp(tf.position, trackingTf.position, 10f * Time.fixedDeltaTime);
            }

            if(isExecute && !skillTimer.IsStart)
            {
                target = FindTarget();
                if(target != null)
                {
                    SkillExecute();
                }
            }
        }        
        protected void Projectile()
        {
            BaseBullet bullet = SimplePool.Spawn<BaseBullet>(PoolType.TYPE1_BULLET);
            bullet.Tf.position = fireTf.position;
            bullet.Tf.rotation = fireTf.rotation;
            bullet.Damage = damage;
            bullet.Shot(source);
        }
        protected Quaternion ShootDirection(Vector3 targetPosition)
        {
            shootDirection = Vector3.ProjectOnPlane(targetPosition - Tf.position, Data.CharacterParameterData.Tf.up);
            return Quaternion.LookRotation(shootDirection, Data.CharacterParameterData.Tf.up);
        }
        protected Collider FindTarget()
        {
            if(Parameter.WIData.EnemyColliders.Count == 0) return null;
            float minSqrDistance = float.MaxValue;
            Collider target = null;
            Vector3 direction = default;

            foreach(Collider col in Parameter.WIData.EnemyColliders)
            {
                direction = col.transform.position - Tf.position;
                float sqrDistance = direction.sqrMagnitude;
                if(sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    target = col;
                }
            }
            return target;
        }
        protected override void UpdateLevelSkillPropertys()
        {          
            times.Clear();
            actions.Clear();
            float currentTime = 0;
            switch (skillLevel)
            {
                case 0:
                case 1:
                    times.Add(currentTime);
                    actions.Add(() => Tf.DORotateQuaternion(rotToTarget, weaponRotateTime)
                        .OnComplete(Action));                    
                    break;
                case 2:
                    for(int i = 0; i < shootNum; i++)
                    {
                        times.Add(currentTime);
                        actions.Add(() => Tf.DORotateQuaternion(rotToTarget, weaponRotateTime)
                            .OnComplete(Action));
                        currentTime += betweenShootTime;
                    }
                    break;
                    
            }

            void Action()
            {
                tf.rotation = ShootDirection(target.transform.position);
                Projectile();
            }
        }
        private void OnDrawGizmos()
        {
            
            if(target != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(Tf.position, target.transform.position);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Tf.position, Tf.position + shootDirection.normalized * 3);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Tf.position, Tf.position + Data.CharacterParameterData.Tf.up * 3);
            }
        }
    }
}