using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Managers
{
    using Base;
    using DesignPattern;
    using Common;
    using System;

    [DefaultExecutionOrder(-100)]
    public class DataManager : Singleton<DataManager>, IDataService
    {
        //[SerializeField]
        //private LevelData levelData;      
        [SerializeField]
        private GameplayData gameplayData;
        [SerializeField]
        private IAPData iapData;
        //[SerializeField]
        //private AnimAudioData animAudioData;
        [SerializeField]
        private PoolData poolData;
        [SerializeField]
        private StaffSeedSO staffColorData;
        [SerializeField]
        private GameConfig gameConfig;
        private GameData _gameData;

        public GameData GameData
        {
            get
            {
                if(_gameData == null)
                {
                    Load();
                    _gameData.InitData();
                }
                return _gameData;
            }
        }
        public int NormalLevelIndex => GameData.user.normalLevelIndex;

        private void Awake()
        {
            Locator.Data = this;
            DontDestroyOnLoad(this);
        }

        private GameData Load()
        {
            _gameData = Database.Load<GameData>();
            return _gameData;
        }
        public void Save()
        {
            Database.Save(_gameData);
        }

        public T GetSOData<T>() where T : ScriptableObject
        {
            switch (typeof(T))
            {
                //case Type type when type == typeof(LevelData):
                //    return levelData as T;
                case Type type when type == typeof(GameplayData):
                    return gameplayData as T;
                case Type type when type == typeof(StaffSeedSO):
                    return staffColorData as T;
                case Type type when type == typeof(GameConfig):
                    return gameConfig as T;
                case Type type when type == typeof(GameData):
                    return iapData as T;
                    //case Type type when type == typeof(PoolData):
                    //    return poolData as T;
                    //case Type type when type == typeof(GameConfig):
                    //    return gameConfig as T;
            }
            return null;
        }

        public T GetUnit<T>(int type) where T : class
        {
            PoolType t = (PoolType)type;
            return poolData.Units[t] as T;
        }

        public T GetData<T>(int index = 0) where T : class
        {
            switch (typeof(T))
            {
                case Type type when type == typeof(GameData):
                    return GameData as T;
                case Type type when type == typeof(LevelData):
                    string json = LoadFromJson(index);
                    LevelData levelData = JsonHelper.ItemFromJson<LevelData>(json);
                    return levelData as T;

            }
            return null;
        }


        protected string LoadFromJson(int level)
        {
            TextAsset tmp = null;
            tmp = (TextAsset)Resources.Load("LevelData/" + "Lvl_" + level);
            if (tmp != null) return tmp.ToString();
            return null;
        }
    }
}