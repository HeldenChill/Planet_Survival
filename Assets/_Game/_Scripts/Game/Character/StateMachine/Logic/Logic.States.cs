using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Character
{
    using Base;
    using System;
    using Utilities;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Data;
    using Utilities.StateMachine;
    #region BASE STATE

    #region GROUNDED STATE
    public abstract class GroundedState<D, P, E> : BaseLogicState<D, P, E>
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        protected GroundedState(D data, P parameter, E _event)
            : base(data, parameter, _event)
        {
        }

        public override void Enter()
        {
            Event.SetAnimBool(typeof(IdleState<D, P, E>), CONSTANTS.IS_GROUNDED_ANIM_NAME, true);
        }
        public override bool Update()
        {
            if (!base.Update()) return false;
            if (Parameter.WIData.IsGrounded && Parameter.NavData.Jump.Value)
            {
                ChangeState(STATE.JUMP);
                return false;
            }
            return true;
        }
        public override void Exit()
        {
            Event.SetAnimBool(typeof(IdleState<D, P, E>), CONSTANTS.IS_GROUNDED_ANIM_NAME, false);
        }
    }
    public abstract class IdleState<D, P, E> : GroundedState<D, P, E>
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        public override STATE Id => STATE.IDLE;

        public IdleState(D data, P parameter, E _event)
            : base(data, parameter, _event) { }
        public override void Enter()
        {
            base.Enter();
            Event.SetVelocity(Vector3.zero);           
            Event.SetAnimBool(typeof(IdleState<D, P, E>) ,CONSTANTS.IS_IDLE_ANIM_NAME, true);
        }
        public override bool Update()
        {
            if (!base.Update()) return false;
            if (Parameter.NavData.Fire.Value)
            {
                Event.OnFire();
            }
            if (Parameter.NavData.MoveDirection.sqrMagnitude > 0.0001f)
            {
                ChangeState(STATE.MOVE);
                return true;
            }
            return true;
        }
        public override void Exit()
        {
            base.Exit();
            Event.SetAnimBool(typeof(IdleState<D, P, E>), CONSTANTS.IS_IDLE_ANIM_NAME, false);
        }


    }
    public abstract class MoveState<D, P, E> : GroundedState<D, P, E>
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        protected readonly Vector3 NORMAL_PLANE_2D = -Vector3.forward;
        public MoveState(D data, P parameter, E _event)
            : base(data, parameter, _event)
        {
        }

        public override STATE Id => STATE.MOVE;

        public override void Enter()
        {
            base.Enter();
            Event.SetVelocity(Parameter.NavData.MoveDirection * Stats<CharacterStats>().Speed.Value);
            Event.SetAnimBool(typeof(IdleState<D, P, E>), CONSTANTS.IS_RUN_ANIM_NAME, true);
        }

        public override void Exit()
        {
            base.Exit();
            Event.SetAnimBool(typeof(IdleState<D, P, E>), CONSTANTS.IS_RUN_ANIM_NAME, false);
        }

        public override bool Update()
        {
            if (!base.Update()) return false;
            if (Parameter.NavData.MoveDirection.sqrMagnitude < 0.0001f)
            {
                ChangeState(STATE.IDLE);
            }
            return true;
        }

        public override bool FixedUpdate()
        {
            if (Parameter.NavData.MoveDirection.sqrMagnitude < 0.0001f)
                return false;
            Vector3 inputMoveDirection = new Vector3(Parameter.NavData.MoveDirection.x, 0, Parameter.NavData.MoveDirection.y);
            Vector3 moveDirection = Parameter.PhysicData.CharacterParameterData.Tf
                .TransformDirection(inputMoveDirection);
            UpdateSkinRotation(inputMoveDirection);
            Event.SetVelocity(moveDirection * Stats<CharacterStats>().Speed.Value);
            return base.FixedUpdate();
        }
    }
    #endregion
    public abstract class JumpState<D, P, E> : BaseLogicState<D, P, E>
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        bool isJumping = false;
        public JumpState(D data, P parameter, E _event)
            : base(data, parameter, _event)
        {
        }

        public override STATE Id => STATE.JUMP;

        public override void Enter()
        {
            Event.AddForce(Parameter.PhysicData.CharacterParameterData.Tf.up * Stats<CharacterStats>().JumpSpeed.Value);
            Event.SetAnimTrigger(typeof(IdleState<D, P, E>), CONSTANTS.JUMP_ANIM_NAME);
            isJumping = false;
        }

        public override void Exit()
        {

        }

        public override bool Update()
        {
            if (!base.Update()) return false;
            isJumping = !Parameter.WIData.IsGrounded || isJumping;
            if (!isJumping) return false;
            ChangeState(STATE.IN_AIR);
            return true;
        }
    }
    public class DieState<D, P, E> : BaseLogicState<D, P, E>
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        public DieState(D data, P parameter, E _event) : base(data, parameter, _event) { }

        public override STATE Id => STATE.DIE;

        public override void Enter()
        {
            Event.SetVelocityX(0);
            Event.OnDie();
        }

        public override void Exit()
        {

        }

        public override bool Update()
        {
            return true;
        }
    }
    public abstract class AirState<D, P, E> : BaseLogicState<D, P, E>
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        public AirState(D data, P parameter, E _event) : base(data, parameter, _event) { }

        public override STATE Id => STATE.IN_AIR;

        public override void Enter()
        {
        }

        public override void Exit()
        {           
        }

        public override bool Update()
        {
            if (!base.Update()) return false;
            if (Parameter.WIData.IsGrounded)
            {
                if (Parameter.NavData.MoveDirection.sqrMagnitude > 0.0001f)
                {
                    ChangeState(STATE.MOVE);
                }
                else
                {
                    ChangeState(STATE.IDLE);
                }
            }
            return true;
        }

        public override bool FixedUpdate()
        {
            if (Parameter.NavData.MoveDirection.sqrMagnitude < 0.0001f)
                return false;
            Vector3 inputMoveDirection = new Vector3(Parameter.NavData.MoveDirection.x, 0, Parameter.NavData.MoveDirection.y);
            Vector3 moveDirection = Parameter.PhysicData.CharacterParameterData.Tf
               .TransformDirection(inputMoveDirection);
            UpdateSkinRotation(inputMoveDirection);
            Event.SetLocalVelocityXZ(moveDirection * Stats<CharacterStats>().Speed.Value);
            return base.FixedUpdate();
        }
    }
    #endregion
    #region PLAYER STATE
    public class PlayerIdleState : IdleState<LogicData, LogicParameter, LogicEvent>
    {
        public PlayerIdleState(LogicData data, LogicParameter parameter, LogicEvent _event) : base(data, parameter, _event)
        {
        }
    }
    public class PlayerMoveState : MoveState<LogicData, LogicParameter, LogicEvent>
    {
        public PlayerMoveState(LogicData data, LogicParameter parameter, LogicEvent _event) : base(data, parameter, _event)
        {
        }
    }
    public class PlayerJumpState : JumpState<LogicData, LogicParameter, LogicEvent>
    {
        public PlayerJumpState(LogicData data, LogicParameter parameter, LogicEvent _event) : base(data, parameter, _event)
        {
        }
    }

    public class PlayerAirState : AirState<LogicData, LogicParameter, LogicEvent>
    {
        public PlayerAirState(LogicData data, LogicParameter parameter, LogicEvent _event) : base(data, parameter, _event) { }
    }

    public class PlayerDieState : DieState<LogicData, LogicParameter, LogicEvent>
    {
        public PlayerDieState(LogicData data, LogicParameter parameter, LogicEvent _event) : base(data, parameter, _event) { }
    }
    #endregion
    #region ENEMY STATE
    public class EnemyIdleState : IdleState<LogicData, LogicParameter, EnemyLogicEvent>
    {
        EnemyNavigationData NavData;
        ALERT_STATE currentAlertState;
        public EnemyIdleState(LogicData data, LogicParameter parameter, EnemyLogicEvent _event) : base(data, parameter, _event)
        {

        }

        public override STATE Id => STATE.IDLE;

        public override void Enter()
        {
            base.Enter();
            currentAlertState = ALERT_STATE.NONE;
            NavData = Parameter.NavData as EnemyNavigationData;
        }

        public override void Exit()
        {
            base.Exit();
            Event.ChangeAlertState(ALERT_STATE.NONE);
        }

        public override bool Update()
        {
            if (!base.Update()) return false;
            CheckingAlertState(NavData.AlertState);
            return true;
        }

        protected void CheckingAlertState(ALERT_STATE state)
        {
            if (currentAlertState == state) return;
            currentAlertState = state;
            switch (state)
            {
                case ALERT_STATE.NONE:
                    break;
                case ALERT_STATE.START:
                    break;
                case ALERT_STATE.MED_ALERT:
                    break;
                case ALERT_STATE.ALERT:
                    break;
            }
            Event.ChangeAlertState(currentAlertState);
        }
    }
    public class EnemyMoveState : MoveState<LogicData, LogicParameter, EnemyLogicEvent>
    {
        public EnemyMoveState(LogicData data, LogicParameter parameter, EnemyLogicEvent _event) : base(data, parameter, _event)
        {
        }
    }
    public class EnemyJumpState : JumpState<LogicData, LogicParameter, EnemyLogicEvent>
    {
        public EnemyJumpState(LogicData data, LogicParameter parameter, EnemyLogicEvent _event) : base(data, parameter, _event)
        {
        }
    }
    public class EnemyDieState : DieState<LogicData, LogicParameter, EnemyLogicEvent>
    {
        public EnemyDieState(LogicData data, LogicParameter parameter, EnemyLogicEvent _event) : base(data, parameter, _event)
        {
        }
    }
    #endregion
}