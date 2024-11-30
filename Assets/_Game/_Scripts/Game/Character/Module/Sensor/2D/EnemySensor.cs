using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic.WorldInterface
{
    using Utilities.Core.Character.WorldInterfaceSystem;
    using Dynamic.WorldInterface.Sensor;
    public class EnemySensor : WorldInterfaceModule
    {
        ScanSensor ScanSensor;
        DetectGroundAndWallSensor CheckGroundSensor;
        Type1Sensor Type1Sensor;

        public override void UpdateData()
        {
            CheckGroundSensor?.UpdateState();
            ScanSensor?.UpdateState();           
            Type1Sensor?.UpdateState();
        }
    }
}
