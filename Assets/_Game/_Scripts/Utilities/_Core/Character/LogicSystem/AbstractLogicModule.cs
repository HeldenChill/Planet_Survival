using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character.LogicSystem
{
    public abstract class AbstractLogicModule<D, P, E> : AbstractModuleSystem<D, P>
        where D : LogicData
        where P : LogicParameter 
        where E : LogicEvent
    {
        protected E Event;
        
        public override void Initialize(D Data, P Parameter)
        {
            this.Parameter = Parameter;
            this.Data = Data;
        }

        public virtual void Initialize(D Data, P Parameter, E Event)
        {
            this.Event = Event;
            Initialize(Data, Parameter);
        }
    }
}