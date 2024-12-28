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
        protected NoviceGun weapon;
        [SerializeField]
        protected Transform weaponPosTf;
        public NoviceGun Weapon => weapon;
        protected override void Awake()
        {
            base.Awake();
            if(_instance == null)
                _instance = this;
            LogicSystem = new PlayerLogicSystem(LogicModule, CharacterData);
            
            takeDamageModule.OnInit(typeof(Player), Stats);
        }
        private void Start()
        {
            AddSkill(weapon);
            weapon.Equip(this, weaponPosTf);
            weapon.SkillExecute();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {        
            base.OnDisable();
        }
        public void Teleport(Vector2 position)
        {
            Tf.position = position;
        }

        public void AddSkill(BaseSkill skill)
        {
            ((PlayerLogicSystem)LogicSystem).AddSkill(skill);
        }

        public void RemoveSkill(BaseSkill skill)
        {
            ((PlayerLogicSystem)LogicSystem).RemoveSkill(skill);
        }
    }
}