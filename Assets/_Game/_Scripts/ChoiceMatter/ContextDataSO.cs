using UnityEngine;

namespace ChoiceMatter
{
    using System;
    using System.Collections.Generic;
    public enum SPREAKER
    {
        NONE = -1,
        OBJECT = 0,
        PLAYER = 1,
    }

    
    [Serializable]
    public class ContextResponse
    {
        public string Id;
        public string Text;
        public List<ContextDataSO> ResponseDialogs;
    }
    [CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/Dialog/DialogData", order = 1)]
    public class ContextDataSO : ScriptableObject
    {
        [SerializeField]
        protected ChoiceConditionSO condition;
        [SerializeField] 
        public ChoiceConditionSO Condition => condition;
        [SerializeField]
        protected string dialogueID;
        public string DialogueID => dialogueID;
        [SerializeField]
        protected SPREAKER speaker;
        public SPREAKER Speaker => speaker;
        [SerializeField]
        protected List<string> dialogs;
        public List<string> Dialogs => dialogs;
        [SerializeField]
        protected List<ContextResponse> responses;
        public List<ContextResponse> Responses => responses;

    }
}
