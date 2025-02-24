using UnityEngine;

namespace _Game
{
    using System;
    using Utilities.StateMachine;

    public class BaseEffectState : BaseState
    {
        [Serializable]
        public class BlackBoard
        {
            #region IN
            public IDamageable Character;
            public BaseEffect MainEffect;
            public BaseEffect UpdateEffect;
            #endregion
        }
        public override STATE Id => STATE.NONE;

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override bool Update()
        {
            return true;
        }
    }
}
