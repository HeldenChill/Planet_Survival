using _Game.Character;
using System;
using UnityEngine;
using Utilities.Timer;

namespace _Game
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField]
        protected Transform tf;
        [SerializeField]
        protected SkillData skillData;
        protected PlayerLogicParameter Parameter;
        protected PlayerLogicData Data;
        protected STimer skillTimer;
        protected bool isExecute = false;
        protected int skillLevel;
        protected Type skillType;

        public virtual int SkillLevel
        {
            get => skillLevel;
            set
            {
                skillLevel = value;
                skillData.Level = skillLevel;
            }
        }

        public Transform Tf => tf;

        protected virtual void Awake()
        {
            skillTimer = TimerManager.Ins.PopSTimer();
        }
        public virtual void OnInit(PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            this.Parameter = Parameter;
            this.Data = Data;
            isExecute = false;
        }
        public virtual void SkillExecute()
        {
            isExecute = true;
        }
        public abstract void SkillActivation();
        protected abstract void UpdateLevelSkillPropertys();
    }
}