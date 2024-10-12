using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Character
{
    using Utilities.Core.Character.NavigationSystem;
    using Base;
    public class EnemyNavigationData : NavigationData
    {
        public ALERT_STATE AlertState = ALERT_STATE.NONE;
    }
}