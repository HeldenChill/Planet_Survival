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
        {}
        [SerializeField]
        protected float attackRange;
        [SerializeField]
        HiddenStats hidden;
        public HiddenStats Hidden => hidden;
        public float AttackRange => attackRange;

        public override void OnInit<T>(T stats)
        {
            base.OnInit(stats);
            EnemyStats enemyStats = stats as EnemyStats;
            attackRange = enemyStats.AttackRange;

            hidden = new HiddenStats();
        }
    }
}