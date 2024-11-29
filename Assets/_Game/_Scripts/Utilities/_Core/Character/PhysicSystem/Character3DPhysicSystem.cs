using UnityEngine;
using Utilities.Core.Character.PhysicSystem;
using Utilities.Core.Character;
namespace Utilities.Core.Character.PhysicSystem
{
    public class Character3DPhysicSystem : AbstractCharacterSystem<Abstract3DPhysicModule, PhysicData, PhysicParameter>
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        #region Attributes
        public Abstract3DPhysicModule MovementModule { get => module; set => module = value; }
        #endregion
        public Character3DPhysicSystem(Abstract3DPhysicModule module, CharacterParameterData characterData)
        {
            data = new PhysicData();
            Parameter = new PhysicParameter();
            data.CharacterParameterData = characterData;
            this.module = module;
            module.Initialize(data, Parameter);
        }
    }
}