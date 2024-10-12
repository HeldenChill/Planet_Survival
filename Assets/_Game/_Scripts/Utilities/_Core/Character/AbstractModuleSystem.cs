using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character
{
    [DefaultExecutionOrder(15)]
    public abstract class AbstractModuleSystem<D, P> : MonoBehaviour
        where D : AbstractDataSystem
        where P : AbstractParameterSystem
    {
        protected D Data;
        protected P Parameter;
        public abstract void Initialize(D Data, P Parameter);
        public abstract void UpdateData();

        public virtual void FixedUpdateData()
        {

        }
    }
}