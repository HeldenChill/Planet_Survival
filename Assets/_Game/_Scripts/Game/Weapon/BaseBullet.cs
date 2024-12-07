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
        Rigidbody rb;
        [SerializeField]
        float speed;
        [SerializeField]
        ICharacter source;
        [SerializeField]
        FakeGravityBody fakeGravityBody;

        public float Damage;
        public void Shot(ICharacter source = null)
        {
            this.source = source;
            rb.linearVelocity = Tf.forward * speed;
            fakeGravityBody.Attractor = ((Player)source).FakeGravityBody.Attractor;
            fakeGravityBody.Distance = (Tf.position - fakeGravityBody.Attractor.Tf.position).magnitude;
        }

        private void FixedUpdate()
        {
            
        }
        private void OnTriggerEnter(Collider collision)
        {
            int characterMask = LayerMask.NameToLayer(Base.CONSTANTS.CHAR_COLLIDER_LAYER);
            if (collision.gameObject.layer == characterMask)
            {
                IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();
                if(enemy.Type == typeof(Enemy))
                {
                    float value = enemy.TakeDamage(-Damage, source);
                    this.Despawn();
                }
            }
            int groundMask = LayerMask.NameToLayer(Base.CONSTANTS.GROUND_LAYER);

            if (collision.gameObject.layer == groundMask)
            {
                this.Despawn();
            }
        }
    }
}