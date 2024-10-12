using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace _Game.Character
{
    using Base;
    using Dynamic.WorldInterface.Data;
    using SStats;
    using Utilities;
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.Core.Data;
    using Utilities.StateMachine;
    using Utilities.Timer;
    public class NavAlertState : BaseNavigationState<EnemyNavigationData, NavigationParameter>
    {
        protected ScanSensorData scanSensorData;
        bool isSeeingObject;
        public NavAlertState(EnemyNavigationData data, NavigationParameter parameter) : base(data, parameter)
        {
        }

        public override STATE Id => STATE.NAV_ALERT;

        public override void Enter()
        {
            Data.MoveDirection = Vector2.zero;
            scanSensorData = Parameter.WIData.GetSensorData<ScanSensorData>();
            isSeeingObject = true;
            Data.AlertState = ALERT_STATE.START;
        }

        public override void Exit()
        {
            Data.AlertState = ALERT_STATE.NONE;
        }

        public override bool Update()
        {
            //DevLog.Log(DevId.Hung, $"IS SEE = {isSeeingObject}, DETECTED: {scanSensorData.DetectedObject}");
            if (isSeeingObject && scanSensorData.AttackObject)
                return false;
            else if(isSeeingObject && !scanSensorData.AttackObject)
            {
                isSeeingObject = false;
            }
            else if(!isSeeingObject && scanSensorData.AttackObject)
            {
                isSeeingObject = true;
                if(Data.AlertState != ALERT_STATE.ALERT)
                {
                    Data.AlertState = (ALERT_STATE)((int)Data.AlertState + 1);
                }
                else
                {
                    Data.AlertState = ALERT_STATE.ALERT;
                }
            }  
            return true;
        }      
    }
    public class NavAttackState : BaseNavigationState<EnemyNavigationData, NavigationParameter>
    {
        public NavAttackState(EnemyNavigationData data, NavigationParameter parameter) : base(data, parameter)
        {
        }

        public override STATE Id => STATE.NAV_ATTACK;

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override bool Update()
        {
            return true;
        }
    }
    public class NavIdleState : BaseNavigationState<EnemyNavigationData, NavigationParameter>
    {
        STimer waitTimer;
        ScanSensorData scanSensorData;
        public NavIdleState(EnemyNavigationData data, NavigationParameter parameter) : base(data, parameter)
        {
            waitTimer = TimerManager.Ins.PopSTimer();
        }

        public override STATE Id => STATE.NAV_IDLE;

        public override void Enter()
        {
            scanSensorData = Parameter.WIData.GetSensorData<ScanSensorData>();
            Data.MoveDirection = Vector2.zero;
        }

        public override void Exit()
        {
            waitTimer.Stop();
            scanSensorData = null;
        }

        public override bool Update()
        {
            if (scanSensorData == null) return false;
            if (!waitTimer.IsStart)
            {
                UpdateState();
            }
            if (scanSensorData.AttackObject)
            {
                waitTimer.Stop();
                ChangeState(STATE.NAV_ALERT);
            }
            return true;
        }

        protected virtual void UpdateState()
        {
            float waitTime = Random.Range(Stats<EnemyStats>().Hidden.MinIdleTime, Stats<EnemyStats>().Hidden.MaxIdleTime);
            waitTimer.Start(waitTime, () => ChangeState(STATE.NAV_PATROL));
        }
    }
    public class NavPatrolState : BaseNavigationState<EnemyNavigationData, NavigationParameter>
    {
        public readonly List<Vector2> CAN_MOVE_DIRS;
        public readonly float[] CHANGE_STATE_RATE;
        protected StatModifier speedModifier;

        protected STimer timer;
        protected ScanSensorData scanSensorData;
        protected DetectGroundEdgeData groundEdgeData;
        protected Vector2 direction;
        protected float changeTime;
        public NavPatrolState(EnemyNavigationData data, NavigationParameter parameter) : base(data, parameter)
        {
            CAN_MOVE_DIRS = new List<Vector2>();
            speedModifier = new StatModifier(-0.5f, StatModType.PercentAdd, 0, Parameter.Character);
            CHANGE_STATE_RATE = new float[2] { 4f, 6f };
        }

        public override STATE Id => STATE.NAV_PATROL;

        public override void Enter()
        {
            timer = TimerManager.Ins.PopSTimer();
            scanSensorData = Parameter.WIData.GetSensorData<ScanSensorData>();
            groundEdgeData = Parameter.WIData.GetSensorData<DetectGroundEdgeData>();
            Parameter.GetStats<EnemyStats>().Speed.AddModifier(speedModifier);
        }

        public override void Exit()
        {
            timer.Stop();
            Parameter.GetStats<EnemyStats>().Speed.RemoveModifier(speedModifier); 
            TimerManager.Ins.PushSTimer(timer);
            timer = null;
        }

        public override bool Update()
        {
            if (!timer.IsStart)
            {
                UpdateState();
            }

            if (scanSensorData.AttackObject != null)
            {
                ChangeState(STATE.NAV_ALERT);
            }
            if (!groundEdgeData.LeftEdgeDetected)
            {
                if(direction.x < 0)
                {
                    direction = Vector2.right;
                    Data.MoveDirection = direction;
                }
            }
            if(!groundEdgeData.RightEdgeDetected)
            {
                if(direction.x > 0)
                {
                    direction = Vector2.left;
                    Data.MoveDirection = direction;
                }
            }
            return true;
        }

        protected virtual void UpdatePropertys()
        {
            UpdateStateTime();
            UpdateStateDirection();
        }
        protected virtual void UpdateStateTime()
        {
            changeTime = Random.Range(Stats<EnemyStats>().Hidden.MinPatrolTime, Stats<EnemyStats>().Hidden.MaxPatrolTime);
        }

        protected virtual void UpdateStateDirection()
        {
            CAN_MOVE_DIRS.Clear();
            if (groundEdgeData.LeftEdgeDetected)
            {
                CAN_MOVE_DIRS.Add(Vector2.left);
            }

            if (groundEdgeData.RightEdgeDetected)
            {
                CAN_MOVE_DIRS.Add(Vector2.right);
            }

            direction = CAN_MOVE_DIRS[Random.Range(0, CAN_MOVE_DIRS.Count)];
        }
        protected virtual void UpdateState()
        {
            int index = SRandom.WheelRandom(CHANGE_STATE_RATE);

            switch (index) 
            {
                case 0:
                    ChangeState(STATE.NAV_IDLE);
                    break;
                case 1:
                    UpdatePropertys();
                    Data.MoveDirection = direction;
                    timer.Start(changeTime, UpdateState);
                    break;
            }
            
        }
    }
    public class NavGuardState : NavPatrolState
    {
        protected float TurnTime = 1;
        public NavGuardState(EnemyNavigationData data, NavigationParameter parameter) : base(data, parameter)
        {
            speedModifier = new StatModifier(-0.75f, StatModType.PercentAdd, 0, Parameter.Character);
            CHANGE_STATE_RATE[0] = 0.5f;
            CHANGE_STATE_RATE[1] = 9.5f;
        }

        public override STATE Id => base.Id;

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override bool Update()
        {
            return base.Update();
        }

        protected override void UpdateStateTime()
        {
            changeTime = Parameter.GetStats<EnemyStats>().Hidden.TurnGuardTime;
        }

        protected override void UpdateStateDirection()
        {
            if(direction.sqrMagnitude > 0.0001f)
            {
                direction *= -1;
            }
            else
            {
                base.UpdateStateDirection();
            }
        }
    }

}

