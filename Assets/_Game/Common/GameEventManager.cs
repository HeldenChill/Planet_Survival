using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Managers
{
    using DesignPattern;
    [DefaultExecutionOrder(-100)]
    public class GameEventManager : Dispatcher<GameEventManager>
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}