using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character.WorldInterfaceSystem
{
    using Utilities.Core.Character;
    public abstract class SensorData { }
    public class WorldInterfaceData : AbstractDataSystem
    {
        //Can improve performance by check value change or not
        public bool IsGrounded;
        public bool IsTouchingWall;
        public bool IsTouchingCorner;
        public RaycastHit2D TouchingWallPoint;
        public RaycastHit2D TouchingGroundPoint;

        public IReadOnlyList<RaycastHit2D> WallHit2D;
        public IReadOnlyList<RaycastHit2D> CharacterHit2D;
        protected List<SensorData> sensorDatas;
        public List<SensorData> SensorDatas => sensorDatas ??= new List<SensorData>();
        public T GetSensorData<T>() where T : SensorData
        {
            return SensorDatas.Find(x => x is T) as T;
        }
    }
}