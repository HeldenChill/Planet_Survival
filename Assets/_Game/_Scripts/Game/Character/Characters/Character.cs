using UnityEngine;
using Utilities.Core.Character.LogicSystem;
using Utilities.Core.Character.NavigationSystem;
using Utilities.Core.Data;
using Utilities.Core;

namespace _Game.Character
{   
    public class Character<T, LD, LP, LE, ND, NP> : BaseCharacter<T, LD, LP, LE, ND, NP>, ICharacter
        where T : CharacterStats
        where LD : LogicData, new()
        where LP : LogicParameter, new()
        where LE : LogicEvent, new()
        where ND : NavigationData, new()
        where NP : NavigationParameter, new()
    {
        [SerializeField]
        protected BaseDisplayModule displayModule;
        [SerializeField]
        protected TakeDamageModule takeDamageModule;
        [SerializeField]
        protected FakeGravityBody fakeGravityBody;
        public FakeGravityBody FakeGravityBody => fakeGravityBody;

        public bool IsDie => Stats.Hp.Value >= 0;

        public virtual void OnInit(CharacterStats stats = null)
        {
            if (stats == null)
                Stats?.Reset();
            else
            {
                Stats?.OnInit(stats);
            }
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            NavigationSystem.ReceiveInformation(this);
            LogicSystem.ReceiveInformation(this);

            #region LOGIC MODULE --> PHYSIC MODULE
            LogicSystem.Event._SetVelocity += PhysicModule.SetVelocity;
            LogicSystem.Event._SetLocalVelocityXZ += PhysicModule.SetLocalVelocityXZ;
            LogicSystem.Event._AddForce += PhysicModule.AddForce;
            LogicSystem.Event._SetSkinRotation += displayModule.SetSkinRotation;
            LogicSystem.Event._SetSkinLocalRotation += displayModule.SetSkinLocalRotation;
            LogicSystem.Event._SetAnimBool += displayModule.SetAnimBool;
            LogicSystem.Event._SetAnimTrigger += displayModule.SetAnimTrigger;
            #endregion
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            #region LOGIC MODULE --> PHYSIC MODULE
            LogicSystem.Event._SetVelocity -= PhysicModule.SetVelocity;
            LogicSystem.Event._SetLocalVelocityXZ -= PhysicModule.SetLocalVelocityXZ;
            LogicSystem.Event._AddForce -= PhysicModule.AddForce;
            LogicSystem.Event._SetSkinRotation -= displayModule.SetSkinRotation;
            LogicSystem.Event._SetSkinLocalRotation -= displayModule.SetSkinLocalRotation;
            LogicSystem.Event._SetAnimBool -= displayModule.SetAnimBool;
            LogicSystem.Event._SetAnimTrigger -= displayModule.SetAnimTrigger;
            #endregion
        }       
    }
}