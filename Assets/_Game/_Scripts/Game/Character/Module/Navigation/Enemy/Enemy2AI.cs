using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Character
{
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.StateMachine;
    public class Enemy2AI : AbstractNavigationModule<EnemyNavigationData, NavigationParameter>
    {
        public override void Initialize(EnemyNavigationData Data, NavigationParameter Parameter)
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