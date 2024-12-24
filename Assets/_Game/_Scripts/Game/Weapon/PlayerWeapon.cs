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
            direction = Vector3.ProjectOnPlane(direction, Data.CharacterParameterData.Tf.up);

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
            return (target, direction);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Tf.position, Tf.position + direction.normalized * 2);
        }
    }
}