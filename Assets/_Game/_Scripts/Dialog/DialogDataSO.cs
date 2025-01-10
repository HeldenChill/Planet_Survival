using UnityEngine;

namespace Dialog
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
    public class DialogResponse
    {
        public string Id;
        public string Text;
        public List<DialogDataSO> ResponseDialogs;
    }
    [CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/Dialog/DialogData", order = 1)]
    public class DialogDataSO : ScriptableObject
    {
        [SerializeField]
        protected DialogConditionSO condition;
        [SerializeField] 
        public DialogConditionSO Condition => condition;
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
        protected List<DialogResponse> responses;
        public List<DialogResponse> Responses => responses;

    }
}
