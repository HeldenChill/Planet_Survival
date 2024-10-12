using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Core.Data
{
    using SStats;
    [CreateAssetMenu(fileName = "PlayerStatus", menuName = "Status Data/Character")]
    public class CharacterStats : ScriptableObject
    {
        public const int WALK_SPEED = 2;
        [SerializeField]
        protected Stat speed;
        [SerializeField]
        protected Stat jumpSpeed;
        [SerializeField]
        protected Stat hp;        
        public Stat Speed => speed;
        public Stat JumpSpeed => jumpSpeed;
        public Stat Hp => hp;
        private void OnEnable()
        {
            speed.isDirty = true;
            jumpSpeed.isDirty = true;
            hp.isDirty = true;
        }
    }
}