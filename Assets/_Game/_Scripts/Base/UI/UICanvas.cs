using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Base.UI
{
    public class UICanvas : MonoBehaviour
    {
        //public bool IsAvoidBackKey = false;
        [FormerlySerializedAs("IsDestroyOnClose")]
        public bool isDestroyOnClose;

        [SerializeField] private bool useAnimator;


        private string _currentAnim = " ";

        private RectTransform _mRectTransform;


        private Canvas _canvas;

        protected Canvas Canvas
        {
            get
            {
                _canvas = _canvas ? _canvas : GetComponent<Canvas>();
                return _canvas;
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Setup(object param = null)
        {
            UIManager.Ins.AddBackUI(this);
            UIManager.Ins.PushBackAction(this, BackKey);
        }

        protected virtual void BackKey()
        {

        }

        public virtual void Open(object param = null)
        {
            UpdateUI();
            gameObject.SetActive(true);
        }



        public virtual void UpdateUI() { }

        public virtual void Close()
        {
            UIManager.Ins.RemoveBackUI(this);
            OnClose();
        }

        public virtual void CloseDirectly(object param = null)
        {
            if (UIManager.Ins.IsContain(this))
            {
                UIManager.Ins.RemoveBackUI(this);
            }
            else OnClose();
        }

        private void OnClose()
        {
            gameObject.SetActive(false);
            if (isDestroyOnClose) Destroy(gameObject);
        }
    }
}