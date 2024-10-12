using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character.WorldInterfaceSystem
{
    public class WorldInterfaceModule : AbstractModuleSystem<WorldInterfaceData,WorldInterfaceParameter>
    {
        [SerializeField]
        List<BaseSensor> sensors;
        public override void Initialize(WorldInterfaceData Data, WorldInterfaceParameter Parameter)
        {
            this.Data = Data;
            this.Parameter = Parameter;
            for(int i = 0; i < sensors.Count; i++)
            {
                sensors[i].Initialize(this.Data,this.Parameter);
            }
        }
        public override void UpdateData()
        {
            for (int i = 0; i < sensors.Count; i++)
            {
                sensors[i].UpdateState();
            }
        }

        public void AddSensor(BaseSensor sensor)
        {
            sensor.Initialize(Data, Parameter);
            sensors.Add(sensor);          
        }

        public void RemoveSensor(BaseSensor sensor)
        {
            sensors.Remove(sensor);
        }
    }
}