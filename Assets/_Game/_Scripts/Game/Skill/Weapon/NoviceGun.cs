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

    public class NoviceGun : BaseWeapon
    {

        protected readonly Vector3 RECOIL_POSITION = new Vector3(0, 0, -0.3f);
        int shootNum = 1;
        float betweenShootTime = 0.15f;
        float weaponRotateTime = 0.1f;
        float recoilTime = 0.08f;

        List<Collider> targets;
        Quaternion rotToTarget;
        Vector3 shootDirection;
        Transform trackingTf;

        List<Action> actions;
        List<float> times;
        STimer activationTimer;
        protected override void Awake()
        {
            base.Awake();
            targets = new List<Collider>();
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
                if (skillType == typeof(NoviceGun))
                {
                    UpdateLevelSkillPropertys();
                }
            }
        }
        public override void OnInit(PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            base.OnInit(Parameter, Data);
            skillType = typeof(NoviceGun);
        }
        public override void Equip(ICharacter source, Transform trackingTf = null)
        {
            base.Equip(source);
            if (trackingTf != null)
            {
                Tf.parent = null;
                this.trackingTf = trackingTf;
            }
        }
        public override void SkillExecute()
        {
            base.SkillExecute();
            SkillActivation();
            skillTimer.Start(skillData.CD, SkillActivation, true);
        }
        public override void SkillActivation()
        {
            FindTarget();
            if (targets.Count == 0)
            {
                skillTimer.Stop();
                return;
            }
            activationTimer.Start(times, actions);
        }
        private void FixedUpdate()
        {
            if (trackingTf != null)
            {
                tf.position = Vector3.Lerp(tf.position, trackingTf.position, 10f * Time.fixedDeltaTime);
            }

            if (isExecute && !skillTimer.IsStart)
            {
                if (Parameter.WIData.EnemyColliders.Count > 0)
                {
                    SkillExecute();
                }
            }
        }
        protected void Projectile(Quaternion rotation)
        {
            BaseBullet bullet = SimplePool.Spawn<BaseBullet>(PoolType.TYPE1_BULLET);
            bullet.Tf.position = fireTf.position;
            bullet.Tf.rotation = rotation;
            bullet.Damage = skillData.Damage;
            bullet.Shot(source);
        }
        protected void TurnDirection(Vector3 targetPosition)
        {
            shootDirection = Vector3.ProjectOnPlane(targetPosition - Tf.position, Data.CharacterParameterData.Tf.up);
            rotToTarget = Quaternion.LookRotation(shootDirection, Data.CharacterParameterData.Tf.up);
        }
        protected void FindTarget()
        {
            if (Parameter.WIData.EnemyColliders.Count == 0)
            {
                targets.Clear();
                return;
            }
            int count = shootNum < Parameter.WIData.EnemyColliders.Count ? shootNum : Parameter.WIData.EnemyColliders.Count;
            Parameter.WIData.EnemyColliders.Sort((a, b) =>
            (a.transform.position - Tf.position).sqrMagnitude.CompareTo((b.transform.position - Tf.position).sqrMagnitude));

            for (int i = 0; i < count; i++)
            {
                targets.Add(Parameter.WIData.EnemyColliders[i]);
            }
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
                    actions.Add(() =>
                    {
                        Shoot();                          
                    });
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    for (int i = 0; i < shootNum; i++)
                    {
                        times.Add(currentTime);
                        actions.Add(() =>
                        {
                            Shoot();
                        });
                        currentTime += betweenShootTime;
                    }
                    break;

            }
            void Shoot()
            {
                Transform targetTf = null;
                if (targets.Count > 0)
                {
                    TurnDirection(targets[0].transform.position);
                    targetTf = targets[0].transform;
                    Tf.DORotateQuaternion(rotToTarget, weaponRotateTime).OnComplete(() => Projectile(targetTf));
                    targets.RemoveAt(0);
                }
                else
                {
                    TurnDirection(Data.CharacterParameterData.Tf.position + Data.CharacterParameterData.SkinTf.forward);
                    Tf.DORotateQuaternion(rotToTarget, weaponRotateTime).OnComplete(() => Projectile(targetTf));
                }
            }
            void Projectile(Transform targetTranform = null)
            {
                if (targetTranform)
                {
                    TurnDirection(targetTranform.position);
                }
                else
                {
                    TurnDirection(Data.CharacterParameterData.Tf.position + Data.CharacterParameterData.SkinTf.forward);
                }
                skinTf.DOLocalMove(RECOIL_POSITION, recoilTime / 2).SetLoops(2, LoopType.Yoyo);
                tf.rotation = rotToTarget;
                this.Projectile(fireTf.rotation);
            }
        }
        private void OnDrawGizmos()
        {
            if (targets != null && targets.Count > 0)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(Tf.position, targets[0].transform.position);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Tf.position, Tf.position + shootDirection.normalized * 3);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Tf.position, Tf.position + Data.CharacterParameterData.Tf.up * 3);
            }
        }
    }
}