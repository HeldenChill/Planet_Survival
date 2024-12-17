using UnityEngine;

namespace _Game
{
    public abstract class BaseSkill : MonoBehaviour
    {
        public abstract void SkillExecute();
        public abstract void SkillActivation();
    }
}