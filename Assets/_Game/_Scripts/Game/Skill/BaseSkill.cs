using _Game.Character;
using UnityEngine;
using Utilities.Timer;

namespace _Game
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField]
        protected Transform tf;
        protected PlayerLogicParameter Parameter;
        protected PlayerLogicData Data;
        protected STimer skillTimer;

        public Transform Tf => tf;

        private void Awake()
        {
            skillTimer = TimerManager.Ins.PopSTimer();
        }
        public void OnInit(PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            this.Parameter = Parameter;
            this.Data = Data;
        }
        public abstract void SkillExecute();
        public abstract void SkillActivation();
    }
}