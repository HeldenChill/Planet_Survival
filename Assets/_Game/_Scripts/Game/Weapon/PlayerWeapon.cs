using UnityEngine;
using Utilities;

namespace _Game
{
    using DesignPattern;
    public class PlayerWeapon : BaseWeapon
    {
        Collider target;
        Vector3 direction;
        public override void SkillExecute()
        {
            skillTimer.Start(1f, SkillActivation, true);
        }
        public override void SkillActivation()
        {
            (target, direction) = FindTarget();
            if(target == null) return;
            direction = Vector3.ProjectOnPlane(direction, Data.CharacterParameterData.Tf.up);

            tf.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
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