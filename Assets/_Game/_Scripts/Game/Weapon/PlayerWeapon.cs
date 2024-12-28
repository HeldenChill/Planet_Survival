using UnityEngine;
using Utilities;

namespace _Game
{
    using _Game.Character;
    using DesignPattern;
    using DG.Tweening;
    using Utilities.Core;

    public class PlayerWeapon : BaseWeapon
    {
        Collider target;
        Vector3 shootDirection;
        Transform trackingTf;

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
            skillTimer.Start(1f, SkillActivation, true);
        }
        public override void SkillActivation()
        {
            target = FindTarget();
            if(target == null) return;

            Quaternion rotToTarget = ShootDirection(target.transform.position);
            Tf.DORotateQuaternion(rotToTarget, 0.1f)
                .OnComplete(Action);

            
            void Action()
            {
                tf.rotation = ShootDirection(target.transform.position);
                Projectile();
            }
        }

        private void FixedUpdate()
        {
            if(trackingTf != null)
            {
                tf.position = Vector3.Lerp(tf.position, trackingTf.position, 10f * Time.fixedDeltaTime);
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