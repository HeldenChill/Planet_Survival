using DesignPattern;
using UnityEngine;

namespace _Game
{
    using _Game.Character;
    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        FakeGravity environment;
        public void SpawnEnemy(PoolType type)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.ENEMY_ZOMBIE);
        }
    }
}