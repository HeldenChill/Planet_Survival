using UnityEngine;
namespace Dynamic.WorldInterface.Sensor
{
    using Utilities.Core.Character.WorldInterfaceSystem;
    using System.Collections.Generic;

    public class Vision3DSensor : BaseSensor
    {
        [SerializeField]
        float visionRadius;
        Collider[] colliders;
        private void Awake()
        {
            colliders = new Collider[4];
            Data.EnemyColliders = new List<Collider>();
        }
        public override void UpdateState()
        {
            Physics.OverlapSphereNonAlloc(tf.position, visionRadius, colliders, layer);
            Data.EnemyColliders.Clear();

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