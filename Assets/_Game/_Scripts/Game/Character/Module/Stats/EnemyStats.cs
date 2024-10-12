using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    using Utilities.Core.Data;
    [CreateAssetMenu(fileName = "EnemyStatus", menuName = "Status Data/Enemy")]
    public class EnemyStats : CharacterStats
    {
        [Serializable]
        public class HiddenStats
        {
            [SerializeField]
            float maxPatrolTime;
            [SerializeField]
            float minPatrolTime;
            [SerializeField]
            float maxIdleTime;
            [SerializeField]
            float minIdleTime;
            [SerializeField]
            float turnGuardTime;

            public float MaxPatrolTime => maxPatrolTime;
            public float MinPatrolTime => minIdleTime;
            public float MaxIdleTime => maxIdleTime;
            public float MinIdleTime => minIdleTime;
            public float TurnGuardTime => turnGuardTime;
        }

        [SerializeField]
        HiddenStats hidden;
        public HiddenStats Hidden => hidden;
    }
}