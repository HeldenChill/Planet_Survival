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
        PlayerLogicData, PlayerLogicParameter, LogicEvent,
        NavigationData, NavigationParameter>
    {
        protected static Player _instance;
        public static Player Ins => _instance;

        [SerializeField]
        protected PlayerWeapon weapon;
        [SerializeField]
        protected List<BaseSkill> Skills;

        public PlayerWeapon Weapon => weapon;
        protected override void Awake()
        {
            base.Awake();
            if(_instance == null)
                _instance = this;
            weapon.Equip(WorldInterfaceModule, WorldInterfaceSystem.Data, this);
            LogicSystem = new PlayerLogicSystem(LogicModule, CharacterData);
            Skills = new List<BaseSkill>();
            Skills.Add(weapon);
            takeDamageModule.OnInit(typeof(Player), Stats);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            ((PlayerLogicSystem)LogicSystem).ReceiveInformation(Skills);
            LogicSystem.Event._SkillActivation += SkillActivation;
        }

        protected override void OnDisable()
        {        
            base.OnDisable();
            LogicSystem.Event._SkillActivation -= SkillActivation;
        }
        public void Teleport(Vector2 position)
        {
            Tf.position = position;
        }

        protected void SkillActivation(int id)
        {
            Skills[id].SkillActivation();
        }
    }
}