using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    using _Game.Character;
    using DesignPattern;
    using Utilities.Core;
    using Utilities.Timer;

    public class BaseBullet : GameUnit
    {
        [SerializeField]
        float despawnTime;
        [SerializeField]
        Rigidbody rb;
        [SerializeField]
        float speed;
        [SerializeField]
        ICharacter source;
        [SerializeField]
        FakeGravityBody fakeGravityBody;

        STimer timer;

        public float Damage;
        public void Shot(ICharacter source = null)
        {
            this.source = source;
            timer = TimerManager.Ins.PopSTimer();
            timer.Start(despawnTime, OnDespawn);
            rb.linearVelocity = Tf.forward * speed;
            fakeGravityBody.Attractor = ((Player)source).FakeGravityBody.Attractor;
            fakeGravityBody.Distance = (Tf.position - fakeGravityBody.Attractor.Tf.position).magnitude;
        }

        private void OnTriggerEnter(Collider collision)
        {
            int characterMask = LayerMask.NameToLayer(Base.CONSTANTS.ENEMY_COLLIDER_LAYER);
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

        public void OnDespawn()
        {
            TimerManager.Ins.PushSTimer(timer);
            timer = null;
            this.Despawn();
        }
    }
}