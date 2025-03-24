using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.Core.Character.WorldInterfaceSystem
{
    using Utilities.Core.Character;
    /// <summary>
    /// Responsible for updating knowledgement about the game world for DynamicObject
    /// </summary>
    public class CharacterWorldInterfaceSystem : AbstractCharacterSystem<WorldInterfaceModule, WorldInterfaceData, WorldInterfaceParameter>
    {
        #region Essential Functions
        protected CharacterWorldInterfaceSystem() { }
        public CharacterWorldInterfaceSystem(WorldInterfaceModule module, PerceptionData characterData)
        {
            this.module = module;
            data = new WorldInterfaceData();
            Parameter = new WorldInterfaceParameter();
            data.CharacterParameterData = characterData;
            module.Initialize(data, Parameter);
        }
        #endregion
    }
}