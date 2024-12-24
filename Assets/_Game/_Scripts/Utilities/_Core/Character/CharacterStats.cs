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
        public virtual void OnInit<T>(T stats) where T : CharacterStats
        {
            CharacterStats stat = stats as CharacterStats;
            speed = new Stat(stat.speed.Value);
            jumpSpeed = new Stat(stat.jumpSpeed.Value);
            hp = new Stat(stat.hp.Value);
        }

        public virtual void Reset()
        {
            speed.Reset();
            jumpSpeed.Reset();
            hp.Reset();            
        }
    }
}