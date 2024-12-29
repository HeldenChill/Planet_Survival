using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData/SkillData", order = 1)]
    public class SkillData : ScriptableObject
    {
        protected int level;
        public int Level
        {
            get => level;
            set => level = value == 0 ? 0 : value - 1;
        }
        [SerializeField]
        List<float> coolDowns;
        public float CD => coolDowns[Level];
    }
}
