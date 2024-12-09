using UnityEngine;
using Utilities.Core.Character.PhysicSystem;

namespace _Game.Character
{
    public class Physic3DModule : Abstract3DPhysicModule
    {
        public override void SetVelocity(Vector3 velocity)
        {
            rb.linearVelocity = velocity;
        }
        public override void SetLocalVelocityXZ(Vector3 velocity)
        {
            Vector3 parallelComponent = Vector3.Project(rb.linearVelocity, Data.CharacterParameterData.Tf.up);
            rb.linearVelocity = parallelComponent + velocity;
        }
        public override void AddForce(Vector3 force, ForceMode mode = ForceMode.Impulse)
        {
            rb.AddForce(force, mode);
        }

        public override void UpdateData()
        {
            Data.RbVelocity = rb.linearVelocity;
        }
    }
}