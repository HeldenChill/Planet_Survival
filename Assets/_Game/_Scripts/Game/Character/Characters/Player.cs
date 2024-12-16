using System.Collections;
using System.Collections.Generic;


namespace _Game.Character
{
    using UnityEngine;
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character.NavigationSystem;
    using Utilities.Core.Data;

    [DefaultExecutionOrder(-10)]
    public class Player : Character<CharacterStats, 
        LogicData, LogicParameter, LogicEvent,
        NavigationData, NavigationParameter>
    {
        protected static Player _instance;
        public static Player Ins => _instance;

        [SerializeField]
        PlayerWeapon weapon;
        public PlayerWeapon Weapon => weapon;
        protected override void Awake()
        {
            base.Awake();
            if(_instance == null)
                _instance = this;
            weapon.Equip(WorldInterfaceModule, WorldInterfaceSystem.Data, this);
            takeDamageModule.OnInit(typeof(Player), Stats);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            LogicSystem.Event._OnFire += Weapon.Fire;
        }

        protected override void OnDisable()
        {        
            base.OnDisable();
            LogicSystem.Event._OnFire += Weapon.Fire;
        }
        public void Teleport(Vector2 position)
        {
            Tf.position = position;
        }
    }
}