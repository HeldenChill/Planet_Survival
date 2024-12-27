using UnityEngine;
using Utilities;

namespace _Game
{
    using DesignPattern;
    using DG.Tweening;

    public class PlayerWeapon : BaseWeapon
    {
        Collider target;
        Vector3 direction;
        Vector3 shootDirection;

        bool isTrackingTarget = false;
        public override void SkillExecute()
        {
            skillTimer.Start(1f, SkillActivation, true);
        }
        public override void SkillActivation()
        {
            isTrackingTarget = false;
            (target, direction) = FindTarget();
            if(target == null) return;
            shootDirection = Vector3.ProjectOnPlane(direction, Data.CharacterParameterData.Tf.up);

            Quaternion rotToTarget = Quaternion.FromToRotation(Vector3.forward, direction);
            Tf.DORotateQuaternion(rotToTarget, 0.1f)
                .OnComplete(Action);

            
            void Action()
            {
                tf.rotation = rotToTarget;
                Projectile();
                isTrackingTarget = true;
            }
        }

        private void FixedUpdate()
        {
            if(isTrackingTarget)
            {

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
        protected (Collider, Vector3) FindTarget()
        {
            if(Parameter.WIData.EnemyColliders.Count == 0) return (null, default);
            float minSqrDistance = float.MaxValue;
            Collider target = null;
            Vector3 minDirection = default;
            Vector3 direction = default;

            foreach(Collider col in Parameter.WIData.EnemyColliders)
            {
                direction = col.transform.position - Tf.position;
                float sqrDistance = direction.sqrMagnitude;
                if(sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    minDirection = direction;
                    target = col;
                }
            }
            return (target, minDirection);
        }
        private void OnDrawGizmos()
        {
            
            if(target != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(Tf.position, target.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(Tf.position, Tf.position + direction.normalized * 3);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Tf.position, Tf.position + shootDirection.normalized * 3);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Tf.position, Tf.position + Data.CharacterParameterData.Tf.up * 3);
            }
        }
    }
}