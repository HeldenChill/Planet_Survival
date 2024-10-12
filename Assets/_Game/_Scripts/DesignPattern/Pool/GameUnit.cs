using UnityEngine;

namespace DesignPattern
{
    public abstract class GameUnit : MonoBehaviour
    {
        [SerializeField]
        private Transform tf;
        [SerializeField]
        private PoolType poolType;
        public PoolType PoolType => poolType;
        public Transform Tf => tf;
        public RectTransform RectTf => (RectTransform)tf;
    }
}
