using System.Collections.Generic;
using System;
using UnityEngine;

namespace ChoiceMatter
{
    [Serializable]
    public abstract class ChoiceStats
    {
        
    }

    [CreateAssetMenu(fileName = "ChoiceCondition", menuName = "ScriptableObjects/ChoiceMatter/ChoiceCondition", order = 1)]
    public class ChoiceConditionSO : ScriptableObject
    {
        public List<ChoiceConditionSO> conditions;
        public virtual bool Check<T>(T stats) where T : ChoiceStats
        {
            bool result = true;
            if (conditions != null)
            {
                for (int i = 0; i < conditions.Count; i++)
                {
                    result &= conditions[i].Check(stats);
                    if (!result) break;
                }
            }
            return result;
        }
    }
}