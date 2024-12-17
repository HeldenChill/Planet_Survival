using UnityEngine;
namespace _Game.Character
{
    using Utilities.Core.Character.LogicSystem;
    using Utilities.Core.Character;
    using System.Collections.Generic;

    public class PlayerLogicSystem : CharacterLogicSystem<PlayerLogicData, PlayerLogicParameter, LogicEvent>
    {
        public PlayerLogicSystem(AbstractLogicModule<PlayerLogicData, PlayerLogicParameter, LogicEvent> module, CharacterParameterData characterData) : base(module, characterData)
        {
        }

        public void ReceiveInformation(List<BaseSkill> Skills)
        {
            Parameter.Skills = Skills;
        }
    }
}