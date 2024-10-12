using _Game.Character;
using _Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Character
{
    using Utilities.Core;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character.NavigationSystem;

    public class Enemy : BaseCharacter<EnemyStats, 
        LogicData, LogicParameter, EnemyLogicEvent,
        EnemyNavigationData, NavigationParameter>
    {
        [SerializeField]
        EnemyDisplayModule displayModule;
        [SerializeField]
        EnemyWeapon weapon;
        [SerializeField]
        TakeDamageModule takeDamageModule;
        public EnemyWeapon Weapon => weapon;
        protected override void Awake()
        {
            base.Awake();
            weapon.Equip(WorldInterfaceModule, WorldInterfaceSystem.Data, this);
            takeDamageModule.OnInit(typeof(Enemy));
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            #region LOGIC MODULE --> PHYSIC MODULE
            LogicSystem.Event._SetVelocity += PhysicModule.SetVelocity;
            LogicSystem.Event._SetVelocityTime += PhysicModule.SetVelocity;
            LogicSystem.Event._SetVelocityFrame += PhysicModule.SetVelocity;

            LogicSystem.Event._SetVelocityX += PhysicModule.SetVelocityX;
            LogicSystem.Event._SetVelocityXTime += PhysicModule.SetVelocityX;
            LogicSystem.Event._SetVelocityXFrame += PhysicModule.SetVelocityX;

            LogicSystem.Event._SetVelocityY += PhysicModule.SetVelocityY;
            LogicSystem.Event._SetVelocityYTime += PhysicModule.SetVelocityY;
            LogicSystem.Event._SetVelocityYFrame += PhysicModule.SetVelocityY;
            LogicSystem.Event._OnAlertStateChange += displayModule.OnChangeAlertState;
            LogicSystem.Event._OnFire += Weapon.Fire;

            LogicSystem.Event._SetSkinRotation += displayModule.SetSkinRotation;
            LogicSystem.Event._OnDie += OnDie;
            NavigationSystem.Module.StartNavigation();
            #endregion
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            #region LOGIC MODULE --> PHYSIC MODULE
            LogicSystem.Event._SetVelocity -= PhysicModule.SetVelocity;
            LogicSystem.Event._SetVelocityTime -= PhysicModule.SetVelocity;
            LogicSystem.Event._SetVelocityFrame -= PhysicModule.SetVelocity;

            LogicSystem.Event._SetVelocityX -= PhysicModule.SetVelocityX;
            LogicSystem.Event._SetVelocityXTime -= PhysicModule.SetVelocityX;
            LogicSystem.Event._SetVelocityXFrame -= PhysicModule.SetVelocityX;

            LogicSystem.Event._SetVelocityY -= PhysicModule.SetVelocityY;
            LogicSystem.Event._SetVelocityYTime -= PhysicModule.SetVelocityY;
            LogicSystem.Event._SetVelocityYFrame -= PhysicModule.SetVelocityY;
            LogicSystem.Event._OnAlertStateChange -= displayModule.OnChangeAlertState;

            LogicSystem.Event._SetSkinRotation -= displayModule.SetSkinRotation;
            LogicSystem.Event._OnDie -= OnDie;
            LogicSystem.Event._OnFire -= Weapon.Fire;

            #endregion
        }  
        
        protected void OnDie()
        {
            Destroy(gameObject);
        }
    }
}