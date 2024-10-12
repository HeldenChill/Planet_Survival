using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.Core.Character.NavigationSystem
{
    using System;
    using Utilities.Core.Character;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character.WorldInterfaceSystem;
    using Utilities.Core.Data;

    /// <summary>
    /// Responsibility for navigating the Dynamic Object (Player or Agent) 
    /// </summary>
    public class CharacterNavigationSystem<D, P> : AbstractCharacterSystem<AbstractNavigationModule<D, P>, D, P>
        where D : NavigationData, new()
        where P : NavigationParameter, new()
    {
        #region System Components
        //NavigationDecision = Core
        public AbstractNavigationModule<D, P> Module { get => module; set => module = value; }
        #endregion
        #region Essential Functions
        protected CharacterNavigationSystem() { }
        //Initialize
        public CharacterNavigationSystem(AbstractNavigationModule<D, P> module, CharacterParameterData characterData)
        {
            data = new D();
            Parameter = new P();
            this.module = module;
            data.CharacterParameterData = characterData;
            base.module.Initialize(data, Parameter);
        }
        #endregion

        #region ReceiveInformation Functions
        public virtual void ReceiveInformation(WorldInterfaceData worldInterface)
        {
            Parameter.WIData = worldInterface;
        }

        public void ReceiveInformation<T>(T stats) where T : CharacterStats
        {
            Parameter.SetStats(stats);
        }

        public void ReceiveInformation(ICharacter character)
        {
            Parameter.Character = character;
        }
        #endregion
    }
}