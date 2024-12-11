using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace _Game.Character
{
    using Unity.Behavior;
    using UnityEditor.Overlays;
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

        private void OnDrawGizmos()
        {
            if(NavParameter != null && NavData != null)
            {
                Vector3 direction = new Vector3(NavData.MoveDirection.x * 2, 0, NavData.MoveDirection.y * 2);
                direction = NavData.CharacterParameterData.Tf.TransformDirection(direction);
                Gizmos.DrawLine(NavData.CharacterParameterData.Tf.position, NavData.CharacterParameterData.Tf.position + direction);
            }
        }
    }
}