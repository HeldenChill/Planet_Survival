using UnityEngine;
using Utilities.Core.Character.NavigationSystem;

namespace Utilities.Core.Character.LogicSystem
{
    using System;
    using Utilities.Core.Character.WorldInterfaceSystem;

    public class LogicParameter : AbstractParameterSystem
    {
        public Action<Type, string> OnAnimationTriggerEvent;
        public Action<Type, AnimationClip> OnReceiveAnimationClipData;
        public NavigationData NavData;
        public WorldInterfaceData WIData;
    }
}