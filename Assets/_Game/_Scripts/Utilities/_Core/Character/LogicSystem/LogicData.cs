namespace Utilities.Core.Character.LogicSystem
{
    public class LogicData : AbstractDataSystem
    {
        public int RemainingJump;

        public bool IsEndAbility = false;
        public bool IsDashing = false;
        public bool CanDash = true;
        


        public bool IsInflictEffect = false;
        public bool IsGetDamage = false;
        public bool IsDeflecting = false;
        public bool IsBlocking = false;
    }
}