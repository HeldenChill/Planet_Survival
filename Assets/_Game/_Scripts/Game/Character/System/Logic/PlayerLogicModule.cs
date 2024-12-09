using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    using Utilities.Core.Character.LogicSystem;
    using Utilities.StateMachine;
    using System;

    public class PlayerLogicModule : AbstractLogicModule<LogicData, LogicParameter, LogicEvent>
    {
        StateMachine stateMachine;
        public override void Initialize(LogicData Data, LogicParameter Parameter, LogicEvent Event)
        {
            base.Initialize(Data, Parameter, Event);

            stateMachine = new StateMachine();
            stateMachine.IsDebug = false;
            stateMachine.AddState(STATE.IDLE ,new PlayerIdleState(Data, Parameter, Event));
            stateMachine.AddState(STATE.MOVE, new  PlayerMoveState(Data, Parameter, Event));  
            stateMachine.AddState(STATE.JUMP, new  PlayerJumpState(Data, Parameter, Event));
            stateMachine.AddState(STATE.IN_AIR, new PlayerAirState(Data, Parameter, Event));
        }
        private void Start()
        {
            stateMachine.Start(STATE.IDLE);

        }
        public override void UpdateData()
        {
            stateMachine.Update();
        }

        public override void FixedUpdateData()
        {
            stateMachine.FixedUpdate();
        }
    }
}