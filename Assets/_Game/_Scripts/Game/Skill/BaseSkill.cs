using _Game.Character;
using Codice.CM.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Core;
using Utilities.Timer;

namespace _Game
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField]
        protected Transform tf;
        [SerializeField]
        protected SkillData skillData;
        protected ICharacter source;
        protected PlayerLogicParameter Parameter;
        protected PlayerLogicData Data;
        protected STimer skillTimer;
        protected bool isExecute = false;
        protected bool isActivation = false;
        protected int skillLevel;
        protected Type skillType;

        protected List<Action> actions;
        protected List<float> times;
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
        public virtual void OnInit(ICharacter source, PlayerLogicParameter Parameter, PlayerLogicData Data)
        {
            this.source = source;
            this.Parameter = Parameter;
            this.Data = Data;
            isExecute = false;
        }
        public virtual void SkillExecute()
        {
            isExecute = true;
        }
        public virtual void SkillActivation()
        {
            isActivation = true;
        }

        public virtual void StopExecute()
        {
            isExecute = false;
        }

        public virtual void StopActivation()
        {
            isActivation = false;
        }

        protected abstract void UpdateLevelSkillPropertys();
    }
}