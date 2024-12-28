using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.StateMachine
{

    public enum STATE
    {
        NONE = 0,
        IDLE = 1,
        MOVE = 2,
        JUMP = 3,
        DIE = 4,
        IN_AIR = 5,
    }
    [Serializable]
    public abstract class BaseState
    {
        public event Action<STATE> _OnStateChanged;
        public abstract STATE Id { get; }
        public abstract void Enter();
        public abstract bool Update();
        public virtual bool FixedUpdate() { return true; }
        public abstract void Exit();
        protected void ChangeState(STATE newState)
        {
            _OnStateChanged?.Invoke(newState);
        }
    }
}
