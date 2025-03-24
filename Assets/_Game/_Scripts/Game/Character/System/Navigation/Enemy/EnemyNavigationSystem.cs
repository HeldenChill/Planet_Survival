using UnityEngine;

namespace _Game.Character
{
    using Utilities.Core.Character;
    using Utilities.Core.Character.NavigationSystem;
    public class EnemyNavigationSystem : CharacterNavigationSystem<EnemyNavigationData, EnemyNavigationParameter>
    {
        public EnemyNavigationSystem(AbstractNavigationModule<EnemyNavigationData, EnemyNavigationParameter> module, PerceptionData data) : base(module, data)
        {}
        public void ReceiveInformation(Player player)
        {
            Parameter.Player = player;
        }
    }
}

