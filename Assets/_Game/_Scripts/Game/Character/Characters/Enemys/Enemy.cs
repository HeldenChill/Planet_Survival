using _Game.Character;
using _Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Character
{
    using DesignPattern;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.Core.Data;

    public class Enemy : Character<EnemyStats, 
        LogicData, LogicParameter, EnemyLogicEvent,
        EnemyNavigationData, EnemyNavigationParameter>
    {
        //[SerializeField]
        //EnemyWeapon weapon;
        //public EnemyWeapon Weapon => weapon;
        public EnemyDisplayModule DisplayModule => displayModule as EnemyDisplayModule;
        public override void OnInit(CharacterStats stats = null)
        {
            base.OnInit(stats);
            ((EnemyLogicModule)LogicModule).StartModule();
        }
        protected override void Awake()
        {
            base.Awake();
            //weapon.Equip(WorldInterfaceModule, WorldInterfaceSystem.Data, this);
            NavigationSystem = new EnemyNavigationSystem(NavigationModule, CharacterData);
            takeDamageModule.OnInit(typeof(Enemy), Stats);        
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            ((EnemyNavigationSystem)NavigationSystem).ReceiveInformation(Player.Ins);
            #region LOGIC MODULE --> PHYSIC MODULE
            //LogicSystem.Event._OnAlertStateChange += DisplayModule.OnChangeAlertState;
            LogicSystem.Event._OnDie += OnDie;
            //LogicSystem.Event._OnFire += Weapon.Fire;
            //NavigationSystem.Module.StartNavigation();
            #endregion
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            #region LOGIC MODULE --> PHYSIC MODULE
            //LogicSystem.Event._OnAlertStateChange -= DisplayModule.OnChangeAlertState;
            LogicSystem.Event._OnDie -= OnDie;
            //LogicSystem.Event._OnFire -= Weapon.Fire;

            #endregion
        }  
        
        protected void OnDie()
        {
            ((EnemyLogicModule)LogicModule).StopModule();
            SimplePool.Despawn(this);
        }
    }
}