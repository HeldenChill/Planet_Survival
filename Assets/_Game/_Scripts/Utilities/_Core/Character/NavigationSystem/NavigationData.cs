using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character.NavigationSystem
{
    using System;
    using Utilities.Timer;
    public class Trigger
    {
        bool value;
        public bool Value
        {
            get => value;
            set
            {
                this.value = value;
                TimerManager.Ins.WaitForFrame(1, () => this.value = false);
            }
        }
    }
    public class NavigationData : AbstractDataSystem
    {
        public bool Attack1 = false;
        public bool Attack2 = false;
        public bool Attack3 = false;
        public Trigger Fire = new Trigger();
        public Trigger Jump = new Trigger();
        public bool Dash = false;
        public bool EquipItem = false;
        public Vector2 MoveDirection;
    }
}