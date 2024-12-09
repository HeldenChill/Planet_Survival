using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    using Base;
    using System;
    using Utilities.Core.Character.LogicSystem;

    public class EnemyLogicEvent : LogicEvent
    {
        public event Action<ALERT_STATE> _OnAlertStateChange;

        public void ChangeAlertState(ALERT_STATE alertState)
        {
            _OnAlertStateChange?.Invoke(alertState);
        }
    }
}