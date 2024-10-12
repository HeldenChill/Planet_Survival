using Utilities.Core.Data;

namespace Utilities.Core.Character
{
    public class AbstractParameterSystem
    {
        protected CharacterStats stats;
        public ICharacter Character;

        public void SetStats<T>(T value) where T : CharacterStats
        {
            stats = value;
        }
        public T GetStats<T>() where T : CharacterStats
        {
            return (T)stats;
        }
    }
}