using DesignPattern;
using Dynamic.WorldInterface.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace _Game
{
    public class EnemyWeapon : BaseWeapon
    {
        public override void SkillActivation()
        {
            BaseBullet bullet = SimplePool.Spawn<BaseBullet>(PoolType.TYPE1_BULLET);
            bullet.Tf.position = transform.position;
            bullet.Tf.rotation = transform.rotation;
            bullet.Shot();
        }

        protected override void UpdateLevelSkillPropertys()
        {
            
        }
    }
}