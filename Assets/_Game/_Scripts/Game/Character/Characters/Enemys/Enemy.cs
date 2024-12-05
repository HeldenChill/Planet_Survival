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

    public class Enemy : Character<EnemyStats, 
        LogicData, LogicParameter, EnemyLogicEvent,
        EnemyNavigationData, NavigationParameter>
    {
        //[SerializeField]
        //EnemyWeapon weapon;
        //public EnemyWeapon Weapon => weapon;
        public EnemyDisplayModule DisplayModule => displayModule as EnemyDisplayModule;
        protected override void Awake()
        {
            base.Awake();
            //weapon.Equip(WorldInterfaceModule, WorldInterfaceSystem.Data, this);
            takeDamageModule.OnInit(typeof(Enemy));
        }
        protected override void OnEnable()
        {
            base.OnEnable();
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
            Destroy(gameObject);
        }
    }
}