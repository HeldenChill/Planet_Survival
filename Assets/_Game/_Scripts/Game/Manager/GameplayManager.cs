using DesignPattern;
using UnityEngine;

namespace _Game
{
    using _Game.Character;
    using Utilities;
    using Utilities.Timer;

    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        float spawnTime;
        [SerializeField]
        FakeGravity environment;
        [SerializeField]
        Player player;
        STimer spawnTimer;
        private void Awake()
        {
            spawnTimer = TimerManager.Ins.PopSTimer();
            SpawnEnemy(PoolType.ENEMY_ZOMBIE1);
            spawnTimer.Start(spawnTime, SpawnEnemy, true);
            player.FakeGravityBody.Attractor = environment;
        }
        public void SpawnEnemy(PoolType type)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.ENEMY_ZOMBIE1);
            enemy.OnInit();
            enemy.FakeGravityBody.Attractor = environment;
            Vector3 posSpawn = environment.Tf.position + SRandom.DirectionRandom().normalized * (environment.WorldSize + 4f);
            enemy.Tf.position = posSpawn;
        }

        protected void SpawnEnemy()
        {
            SpawnEnemy(PoolType.ENEMY_ZOMBIE1);
        }
    }
}