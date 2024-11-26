using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Base.Init
{
    public class LoadStart : MonoBehaviour
    {
        [SerializeField]
        string loadSceneName;
        private void Start()
        {
            SceneGameManager.Ins.LoadingSceneAsync(loadSceneName);          
        }
    }
}