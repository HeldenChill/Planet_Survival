using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character.NavigationSystem
{
    public abstract class AbstractNavigationModule<D, P> : AbstractModuleSystem<D, P>
        where D: NavigationData
        where P : NavigationParameter
    { 
        public override void Initialize(D Data,P Parameter)
        {
            this.Data = Data;
            this.Parameter = Parameter;
        }

        public abstract void StartNavigation();
        public abstract void StopNavigation();
        public virtual void Reset()
        {
            //Reset Data;
        }
    }
}