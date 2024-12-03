using System.Collections;
using System.Collections.Generic;


namespace _Game.Character
{
    using DesignPattern;
    using UnityEngine;
    using Utilities.Core;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.Core.Data;

    public class Player : BaseCharacter<CharacterStats, 
        LogicData, LogicParameter, LogicEvent,
        NavigationData, NavigationParameter>
    {
        //[SerializeField]
        //PlayerWeapon weapon;
        [SerializeField]
        BaseDisplayModule displayModule;
        [SerializeField]
        TakeDamageModule takeDamageModule;
        //public PlayerWeapon Weapon => weapon;
        protected override void Awake()
        {
            base.Awake();
            //weapon.Equip(WorldInterfaceModule, WorldInterfaceSystem.Data, this);
            takeDamageModule.OnInit(typeof(Player));
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            #region LOGIC MODULE --> PHYSIC MODULE
            LogicSystem.Event._SetVelocity += PhysicModule.SetVelocity;
            LogicSystem.Event._SetSkinRotation += displayModule.SetSkinRotation;
            #endregion
        }

        protected override void OnDisable()
        {        
            base.OnDisable();
            #region LOGIC MODULE --> PHYSIC MODULE
            LogicSystem.Event._SetVelocity -= PhysicModule.SetVelocity;
            LogicSystem.Event._SetSkinRotation -= displayModule.SetSkinRotation;
            #endregion
        }
        public void Teleport(Vector2 position)
        {
            Tf.position = position;
        }
    }
}