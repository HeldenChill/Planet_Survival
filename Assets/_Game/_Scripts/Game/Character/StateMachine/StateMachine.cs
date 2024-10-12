using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.StateMachine
{
    public class StateMachine
    {
        private BaseState currentState;
        private Dictionary<STATE, BaseState> states;
        public bool IsDebug;

        public STATE CurrentState => currentState.Id;
        public Dictionary<STATE, BaseState> States => states;

        public StateMachine()
        {
            states = new Dictionary<STATE, BaseState>();
        }

        public void Start(STATE id)
        {
            currentState = states[id];
            currentState?.Enter();
            if (IsDebug)
            {
                DevLog.Log(DevId.Hung, $"START: {id}");
            }
        }

        public void Stop()
        {
            currentState?.Exit();
            currentState = null;
        }
        public void AddState(STATE id, BaseState state)
        {
            if(!states.ContainsKey(id))
            {
                states.Add(id, state);
                states[id]._OnStateChanged += ChangeState;
            }
        }
        public void RemoveState(STATE id) 
        {
            if (states.ContainsKey(id))
            {
                states[id]._OnStateChanged -= ChangeState;
                states.Remove(id);
            }
        }
        public void ChangeState(STATE id)
        {
            if (!states.ContainsKey(id))
            {
                DevLog.Log(DevId.Hung, $"CANNOT CHANGE: {CurrentState} -> {id}");
            }
            if (IsDebug)
            {
                DevLog.Log(DevId.Hung, $"CHANGE: {CurrentState} -> {id}");
            }
            currentState?.Exit();
            currentState = states[id];
            currentState?.Enter();
        }
        public void Update()
        {
            currentState?.Update();
        }
        public void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }
    }
}