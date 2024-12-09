using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Dynamic.WorldInterface.Data
{
    using Utilities.Core.Character.WorldInterfaceSystem;
    public class ScanSensorData : SensorData
    {
        public GameObject DetectedObject;
        public Vector2 HitTargetVector;

        public GameObject AttackObject;
        public Vector2 AttackObjectVector;      
    }
}