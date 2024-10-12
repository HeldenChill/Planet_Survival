using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public interface IDataService
    {
        public T GetUnit<T>(PoolType type) where T : GameUnit;
    }
}