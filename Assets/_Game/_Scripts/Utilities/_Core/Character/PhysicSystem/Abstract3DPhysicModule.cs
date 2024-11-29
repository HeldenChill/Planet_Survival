using UnityEngine;
using Utilities.Core.Character.PhysicSystem;
using Utilities.Core.Character;

namespace Utilities.Core.Character.PhysicSystem
{
    public abstract class Abstract3DPhysicModule : AbstractModuleSystem<PhysicData, PhysicParameter>
    {
        [SerializeField]
        protected Rigidbody rb;

        public override void Initialize(PhysicData Data, PhysicParameter Parameter)
        {
            this.Data = Data;
            this.Parameter = Parameter;
        }

        public abstract void SetVelocity(Vector3 velocity);

    }
}