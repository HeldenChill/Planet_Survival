using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.Core.Character.PhysicSystem
{
    public class Character2DPhysicSystem : AbstractCharacterSystem<Abstract2DPhysicModule,PhysicData,PhysicParameter>
    {
        #region Attributes
        public Abstract2DPhysicModule MovementModule { get => module; set => module = value; }
        #endregion
        public Character2DPhysicSystem(Abstract2DPhysicModule module, CharacterParameterData characterData)
        {
            data = new PhysicData();
            Parameter = new PhysicParameter();
            data.CharacterParameterData = characterData;
            this.module = module;
            module.Initialize(data, Parameter);
        }
    }
}