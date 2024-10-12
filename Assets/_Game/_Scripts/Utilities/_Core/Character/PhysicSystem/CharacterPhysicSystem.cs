using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.Core.Character.PhysicSystem
{
    public class CharacterPhysicSystem : AbstractCharacterSystem<AbstractPhysicModule,PhysicData,PhysicParameter>
    {
        #region Attributes
        public AbstractPhysicModule MovementModule { get => module; set => module = value; }
        #endregion
        public CharacterPhysicSystem(AbstractPhysicModule module, CharacterParameterData characterData)
        {
            data = new PhysicData();
            Parameter = new PhysicParameter();
            data.CharacterParameterData = characterData;
            this.module = module;
            module.Initialize(data, Parameter);
        }
    }
}