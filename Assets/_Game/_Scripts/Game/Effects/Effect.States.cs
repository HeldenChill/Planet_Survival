using UnityEngine;

namespace _Game
{
    public class NoneEffectState : BaseEffectState
    {
        public override bool Update()
        {
            if (!base.Update()) return false;
            return true;
        }
    }

    public class TriggerEffectState : BaseEffectState
    {
        public override bool Update()
        {
            if (!base.Update()) return false;
            return true;
        }
    }
    public class CombineEffectState : BaseEffectState
    {
        public override bool Update()
        {
            if (!base.Update()) return false;
            return true;
        }
    }
}
