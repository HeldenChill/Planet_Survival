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

        public override void UpdateData()
        {
            Data.CharacterParameterData.RbVelocity = rb.linearVelocity;
        }
    }
}