using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.Core.Data;
    using Utilities.StateMachine;
    public abstract class BaseNavigationState<D, P> : BaseState 
        where D : NavigationData
        where P : NavigationParameter
    {
        protected P Parameter;
        protected D Data;

        private CharacterStats stats;
        public T Stats<T>() where T : CharacterStats => (T)(stats ??= Parameter.GetStats<T>());
        public BaseNavigationState(D data, P parameter)
        {
            Parameter = parameter;
            Data = data;
        }
    }
}

