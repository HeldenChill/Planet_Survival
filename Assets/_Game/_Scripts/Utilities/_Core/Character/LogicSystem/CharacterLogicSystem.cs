using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.Core.Character.LogicSystem
{
    using WorldInterfaceSystem;
    using NavigationSystem;
    using PhysicSystem;
    using Utilities.Core.Character;
    using System;
    using Utilities.Core.Data;

    /// <summary>
    /// Responsibility for updating interaction logic between DynamicObject and the game world
    /// </summary>
    public class CharacterLogicSystem<D, P, E> : AbstractCharacterSystem<AbstractModuleSystem<D, P>, D, P>
        where D : LogicData, new()
        where P : LogicParameter, new()
        where E : LogicEvent, new()
    {
        //Initialize
        public E Event;
        public CharacterLogicSystem(AbstractLogicModule<D, P, E> module, CharacterParameterData characterData)
        {
            data = new D();
            Parameter = new P();
            Event = new E();
            this.module = module;
            data.CharacterParameterData = characterData;
            module.Initialize(data, Parameter, Event);
        }

        #region ReceiveInformation Functions
        //Need to update this ReceiveInformation
        public void ReceiveInformation(PhysicData Data)
        {

        }
        public void ReceiveInformation(WorldInterfaceData worldInterface)
        {
            Parameter.WIData = worldInterface;
        }

        public void ReceiveInformation(NavigationData navigation)
        {
            Parameter.NavData = navigation;
        }       

        public void ReceiveInformation<T>(T stats) where T : CharacterStats
        {
            Parameter.SetStats(stats);
        }
        public void ReceiveInformation(ICharacter character)
        {
            Parameter.Character = character;
        }
        public void ReceiveInformation(Type type, string name)
        {
            Parameter.OnAnimationTriggerEvent?.Invoke(type, name);
        }

        public void ReceiveInformation(Type type,AnimationClip clip)
        {
            Parameter.OnReceiveAnimationClipData?.Invoke(type,clip);
        }
        #endregion
    }
}