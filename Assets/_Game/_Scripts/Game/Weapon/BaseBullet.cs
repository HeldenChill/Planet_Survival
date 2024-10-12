using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    using _Game.Character;
    using DesignPattern;
    using Utilities.Core;

    public class BaseBullet : GameUnit
    {
        [SerializeField]
        Rigidbody2D rb;
        [SerializeField]
        float speed;
        [SerializeField]

        ICharacter source;

        public float Damage;
        public void Shot(ICharacter source = null)
        {
            this.source = source;
            rb.velocity = Tf.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            int mask = LayerMask.NameToLayer(CONSTANTS.CHAR_COLLIDER);
            if (collision.gameObject.layer == mask)
            {
                IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();
                if(enemy.Type == typeof(Enemy))
                {
                    float value = enemy.TakeDamage(-Damage, source);
                    this.Despawn();

                    if(value <= 0 && source is Player)
                    {
                        ((Player)source).Teleport(Tf.position);
                    }
                }
            }
        }
    }
}