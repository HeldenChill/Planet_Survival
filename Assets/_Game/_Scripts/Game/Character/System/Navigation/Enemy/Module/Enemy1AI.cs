using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace _Game.Character
{
    using Unity.Behavior;
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.StateMachine;
    public class Enemy1AI : AbstractNavigationModule<EnemyNavigationData, EnemyNavigationParameter>
    {
        public EnemyNavigationData NavData => Data;
        public EnemyNavigationParameter NavParameter => Parameter;
        [SerializeField]
        BehaviorGraphAgent agent;
        public override void Initialize(EnemyNavigationData Data, EnemyNavigationParameter Parameter)
        {
            base.Initialize(Data, Parameter);
        }
        public override void StartNavigation()
        {
            
        }

        public override void StopNavigation()
        {
            
        }

        public override void UpdateData()
        {
            
        }

        private void AddStates(StateMachine stateMachine)
        {
            
        }
    }
}