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
        [SerializeField]
        protected BaseSensor sensor;
        [SerializeField]
        protected GameObject aimEdge1;
        [SerializeField]
        protected GameObject aimEdge2;       
       
        protected Edge2SenorData fanScanSensorData;
        public override void Fire()
        {
            Vector2 direction = MathHelper.GetRandomDirection(fanScanSensorData.Edge1Direction, fanScanSensorData.Edge2Direction);
            float fireAngle = Vector2.SignedAngle(Vector2.right, direction);
            BaseBullet bullet = SimplePool.Spawn<BaseBullet>(PoolType.TYPE1_BULLET);
            bullet.Tf.position = transform.position;
            bullet.Tf.rotation = Quaternion.Euler(0, 0, fireAngle);
            bullet.Damage = damage;
            bullet.Shot(source);
        }

        public override void Equip(WorldInterfaceModule module, WorldInterfaceData data, ICharacter source)
        {
            base.Equip(module, data, source);
            this.module.AddSensor(sensor);
            fanScanSensorData = data.GetSensorData<Edge2SenorData>();
        }
        public override void Unequip()
        {
            module.RemoveSensor(sensor);
            base.Unequip();
            fanScanSensorData = null;
        }
        private void Update()
        {
            RotateAlongMouse();
            aimEdge1.transform.rotation = Quaternion.Euler(0, 0, fanScanSensorData.Edge1Angle);
            aimEdge2.transform.rotation = Quaternion.Euler(0, 0, fanScanSensorData.Edge2Angle);
        }

        protected void RotateAlongMouse()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position);

            transform.rotation = MathHelper.GetQuaternion2Vector(Vector2.right, direction);
        }
    }
}