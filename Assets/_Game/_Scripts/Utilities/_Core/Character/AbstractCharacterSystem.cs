using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Character
{
    using System;
    public abstract class AbstractCharacterSystem<M,D,P>
        where M : AbstractModuleSystem<D,P>
        where D : AbstractDataSystem
        where P : AbstractParameterSystem
    {
        protected M module;
        protected D data;
        protected P Parameter;
        protected virtual void UpdateData()
        {
            module.UpdateData();
        }   
        public virtual D Data
        {
            get => data;
        }
        public virtual void FixedUpdateData()
        {
            module.FixedUpdateData();
        }
        public void Run()
        {
            UpdateData();
        }
    }
}