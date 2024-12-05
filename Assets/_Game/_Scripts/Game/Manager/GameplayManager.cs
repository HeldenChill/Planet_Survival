using DesignPattern;
using UnityEngine;

namespace _Game
{
    using _Game.Character;
    using Utilities;

    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        FakeGravity environment;
        private void Awake()
        {
            SpawnEnemy();
        }
        public void SpawnEnemy(PoolType type = PoolType.NONE)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.ENEMY_ZOMBIE1);
            enemy.FakeGravityBody.Attractor = environment;
            Vector3 posSpawn = environment.Tf.position + SRandom.DirectionRandom().normalized * (environment.WorldSize + 1f);
            enemy.Tf.position = posSpawn;
        }
    }
}