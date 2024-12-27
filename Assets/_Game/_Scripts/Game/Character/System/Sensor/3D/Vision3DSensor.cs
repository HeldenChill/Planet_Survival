using UnityEngine;
namespace Dynamic.WorldInterface.Sensor
{
    using Utilities.Core.Character.WorldInterfaceSystem;
    using System.Collections.Generic;
    using System;

    public class Vision3DSensor : BaseSensor
    {
        [SerializeField]
        float visionRadius;
        Collider[] colliders;
        private void Awake()
        {
            colliders = new Collider[10];
            Data.EnemyColliders = new List<Collider>();
        }
        public override void UpdateState()
        {
            Array.Clear(colliders, 0, colliders.Length);
            Data.EnemyColliders.Clear();

            Physics.OverlapSphereNonAlloc(tf.position, visionRadius, colliders, layer);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i] != null)
                {
                    Data.EnemyColliders.Add(colliders[i]);
                }
            }
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(tf.position, visionRadius);
        }
    }
}