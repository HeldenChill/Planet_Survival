using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Base.UI
{
    using Base.Init;
    using DesignPattern;
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private RectTransform canvasParentTf;
        [SerializeField]
        private RectTransform parentCanvasTf;
        //dict for UI active
        private readonly Dictionary<Type, UICanvas> uiCanvas = new();

        //dict for quick query UI prefab
        private readonly Dictionary<Type, UICanvas> uiCanvasPrefab = new();

        //list from resource
        private UICanvas[] uiResources;
        public float DpUICanvasBanner
        {
            get
            {
                //DevLog.Log(DevId.Hung, "Scale: " + GameplayCanvas.scaleFactor + "Screen Density: " + MaxSdkUtils.GetScreenDensity());
                //return 168 / (canvasUI.scaleFactor * MaxSdkUtils.GetScreenDensity());
                float unitHeight = parentCanvasTf.rect.height;
                float pixelHeight = Screen.height;
                return 168 / pixelHeight * unitHeight;
            }
        }
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        public void UpdateBannerSpace(bool value)
        {
            if (DebugManager.Ins)
            {
                if (DebugManager.Ins.IsShowAds && value)
                {
                    parentCanvasTf.offsetMin = new Vector2(parentCanvasTf.offsetMin.x, DpUICanvasBanner);
                }
                else
                {
                    parentCanvasTf.offsetMin = new Vector2(parentCanvasTf.offsetMin.x, 0);
                }
            }
            else
            {
                if (value)
                {
                    parentCanvasTf.offsetMin = new Vector2(parentCanvasTf.offsetMin.x, DpUICanvasBanner);
                }
                else
                {
                    parentCanvasTf.offsetMin = new Vector2(parentCanvasTf.offsetMin.x, 0);
                }
            }
        }
        public int ConvertPixelToUnitHeight(float pixel)
        {
            float unitHeight = ((RectTransform)canvasParentTf).rect.height;
            float pixelHeight = Screen.height;
            return (int)(pixel / pixelHeight * unitHeight);
        }

        #region Canvas
        public T OpenUI<T>() where T : UICanvas
        {
            UICanvas canvas = GetUI<T>();
            canvas.Setup();
            canvas.Open();

            return (T)canvas;
        }

        public T OpenUI<T>(object param) where T : UICanvas
        {
            UICanvas canvas = GetUI<T>();
            canvas.Setup(param);
            canvas.Open(param);
            return (T)canvas;
        }

        public UICanvas OpenUIDirectly(UICanvas ui)
        {
            UICanvas canvas = Instantiate(ui, parentCanvasTf);
            canvas.Setup();
            canvas.Open();
            return canvas;
        }

        public UICanvas OpenUIDirectly(UICanvas ui, object param)
        {
            UICanvas canvas = Instantiate(ui, parentCanvasTf);
            canvas.Setup(param);
            canvas.Open(param);
            return canvas;
        }

        public void HideUI<T>() where T : UICanvas
        {
            if (IsOpened<T>()) GetUI<T>().Hide();
        }

        public void ShowUI<T>() where T : UICanvas
        {
            if (!IsOpened<T>()) GetUI<T>().Show();
        }

        public void CloseUI<T>() where T : UICanvas
        {
            if (IsOpened<T>()) GetUI<T>().Close();
        }

        public void CloseUIDirectly(UICanvas ui)
        {
            if (!ui.gameObject.activeInHierarchy) return;
            ui.CloseDirectly();
        }

        public bool IsOpened<T>() where T : UICanvas
        {
            return IsLoaded<T>() && uiCanvas[typeof(T)].gameObject.activeInHierarchy;
        }

        public bool IsContain(UICanvas ui)
        {
            return uiCanvas.ContainsValue(ui);
        }


        public bool IsLoaded<T>() where T : UICanvas
        {
            Type type = typeof(T);
            return uiCanvas.ContainsKey(type) && uiCanvas[type] != null;
        }

        public T GetUI<T>() where T : UICanvas
        {
            if (!IsLoaded<T>())
            {
                UICanvas canvas = Instantiate(GetUIPrefab<T>(), parentCanvasTf);
                uiCanvas[typeof(T)] = canvas;
            }

            return uiCanvas[typeof(T)] as T;
        }
        
        public void PreloadUI<T>() where T : UICanvas
        {
            if (IsLoaded<T>()) return;
            UICanvas canvas = Instantiate(GetUIPrefab<T>(), parentCanvasTf);
            canvas.gameObject.SetActive(false);
            uiCanvas[typeof(T)] = canvas;
        }


        private T GetUIPrefab<T>() where T : UICanvas
        {
            if (uiCanvasPrefab.ContainsKey(typeof(T))) return uiCanvasPrefab[typeof(T)] as T;
            uiResources ??= Resources.LoadAll<UICanvas>("UI/");

            for (int i = 0; i < uiResources.Length; i++)
                if (uiResources[i] is T)
                {
                    uiCanvasPrefab[typeof(T)] = uiResources[i];
                    break;
                }

            return uiCanvasPrefab[typeof(T)] as T;
        }

        public void UpdateAllUI()
        {
            for (int i = 0; i < backCanvas.Count; i++)
            {
                backCanvas[i].UpdateUI();
            }
        }

        public void DestroyAllUI(HashSet<UICanvas> exception)
        {
            foreach (KeyValuePair<Type, UICanvas> item in uiCanvas)
            {
                if (item.Value is null) continue;
                if (exception.Contains(item.Value)) continue;
                if (item.Value.gameObject.activeInHierarchy) item.Value.Close();
                Destroy(item.Value.gameObject);
            }
            uiCanvas.Clear();
        }
        
        #endregion
        #region Back Button
        private readonly Dictionary<UICanvas, UnityAction> backActionEvents = new();
        private readonly List<UICanvas> backCanvas = new();

        private UICanvas BackTopUI
        {
            get
            {
                UICanvas canvas = null;
                if (backCanvas.Count > 0) canvas = backCanvas[^1];

                return canvas;
            }
        }


        // private void LateUpdate()
        // {
        //     if (Input.GetKey(KeyCode.Escape) && BackTopUI != null)
        //     {
        //         BackActionEvents[BackTopUI]?.Invoke();
        //     }
        // }

        public void PushBackAction(UICanvas canvas, UnityAction action)
        {
            backActionEvents.TryAdd(canvas, action);
        }

        public void AddBackUI(UICanvas canvas)
        {
            if (!backCanvas.Contains(canvas)) backCanvas.Add(canvas);
        }

        public void RemoveBackUI(UICanvas canvas)
        {
            backCanvas.Remove(canvas);
        }

        public void HideAll()
        {
            foreach (KeyValuePair<Type, UICanvas> item in uiCanvas.Where(item =>
                         item.Value != null && item.Value.gameObject.activeInHierarchy))
                item.Value.Hide();
        }
        public void CloseAll()
        {
            foreach (KeyValuePair<Type, UICanvas> item in uiCanvas.Where(item =>
                         item.Value != null && item.Value.gameObject.activeInHierarchy))
                item.Value.Close();
        }


        /// <summary>
        ///     CLear back key when comeback index UI canvas
        /// </summary>
        public void ClearBackKey()
        {
            backCanvas.Clear();
        }
        #endregion
    }
}