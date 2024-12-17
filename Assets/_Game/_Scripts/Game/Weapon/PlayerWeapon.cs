using UnityEngine;
using Utilities;

namespace _Game
{
    using DesignPattern;
    using Dynamic.WorldInterface.Data;
    using SStats;
    using Utilities.Core;
    using Utilities.Core.Character.WorldInterfaceSystem;
    public class PlayerWeapon : BaseWeapon
    {
        public override void SkillExecute()
        {}
        public override void SkillActivation()
        {
            BaseBullet bullet = SimplePool.Spawn<BaseBullet>(PoolType.TYPE1_BULLET);
            bullet.Tf.position = fireTf.position;
            bullet.Tf.rotation = fireTf.rotation;
            bullet.Damage = damage;
            bullet.Shot(source);
        }       
        public override void Equip(WorldInterfaceModule module, WorldInterfaceData data, ICharacter source)
        {
            base.Equip(module, data, source);
        }
        public override void Unequip()
        {
            base.Unequip();
        }
    }
}