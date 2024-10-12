namespace _Game.Character{
    using UnityEngine;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Data;
    using Utilities.StateMachine;
    public abstract class BaseLogicState<D, P, E> : BaseState
        where D : LogicData
        where P : LogicParameter
        where E : LogicEvent
    {
        protected P Parameter;
        protected D Data;
        protected E Event;
        private CharacterStats stats;
        public T Stats<T>() where T : CharacterStats => (T)(stats ??= Parameter.GetStats<T>());

        protected readonly Quaternion LEFT_QUATERNION = Quaternion.Euler(0, 180, 0);
        protected readonly Quaternion RIGHT_QUATERNION = default;
        public BaseLogicState(D data, P parameter, E _event)
        {
            Parameter = parameter;
            Data = data;
            Event = _event;
        }

        public override bool Update()
        {
            if(Stats<CharacterStats>().Hp.Value <= 0)
            {
                ChangeState(STATE.DIE);
                return false;
            }
            return true;
        }
        protected void UpdateRotation()
        {
            if (Parameter.NavData.MoveDirection.x > 0)
            {
                Event.SetSkinRotation(RIGHT_QUATERNION);
            }
            else
            {
                Event.SetSkinRotation(LEFT_QUATERNION);
            }
        }
    }
}