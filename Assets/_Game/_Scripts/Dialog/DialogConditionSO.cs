using System.Collections.Generic;
using System;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public abstract class DialogStats
    {

    }

    [CreateAssetMenu(fileName = "DialogCondition", menuName = "ScriptableObjects/Dialog/DialogCondition", order = 1)]
    public class DialogConditionSO : ScriptableObject
    {
        public List<DialogConditionSO> conditions;
        public virtual bool Check<T>(T stats) where T : DialogStats
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