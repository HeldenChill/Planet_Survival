using UnityEngine;
namespace _Game.Character
{
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character;
    using System.Collections.Generic;

    public class PlayerLogicSystem : CharacterLogicSystem<PlayerLogicData, PlayerLogicParameter, LogicEvent>
    {
        protected ProcessSkillModule SkillModule;
        public PlayerLogicSystem(AbstractLogicModule<PlayerLogicData, PlayerLogicParameter, LogicEvent> module, CharacterParameterData characterData) : base(module, characterData)
        {
            SkillModule = new ProcessSkillModule(this);
        }

        public void AddSkill(BaseSkill skill)
        {
            skill.OnInit(Parameter, Data);
            skill.SkillLevel = 5;
            SkillModule?.Skills.Add(skill);
        }

        public void RemoveSkill(BaseSkill skill)
        {
            SkillModule?.Skills.Remove(skill);
        }
        public class ProcessSkillModule
        {
            public PlayerLogicSystem LogicSystem;
            public List<BaseSkill> Skills;

            public ProcessSkillModule(PlayerLogicSystem logicSystem)
            {
                this.LogicSystem = logicSystem;
                Skills = new List<BaseSkill>();
            }
        }
    }

    
}