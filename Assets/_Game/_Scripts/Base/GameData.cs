using Base;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    private const string DATA_KEY = "GameData";

    public static void SaveData(GameData data)
    {
        string dataString = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(DATA_KEY, dataString);
        PlayerPrefs.Save();
    }

    public static GameData LoadData()
    {

        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            return JsonConvert.DeserializeObject<GameData>(PlayerPrefs.GetString(DATA_KEY));
        }
        GameData gameData = new();
        SaveData(gameData);
        return gameData;
    }
}

namespace Base
{
    #region GAME
    public enum ALERT_STATE
    {
        NONE = -1,
        START = 0,
        MED_ALERT = 1,
        ALERT = 2,
    }
    #endregion
    public class GameData
    {
        public SettingData setting = new();
        public UserData user = new();

        [Serializable]
        public class UserData
        {
            // Level Progress Data
            public int normalLevelIndex;

            // Booster Data
            public int undoCount;
            public int openBoxCount;
        }

        [Serializable]
        public class SettingData
        {
            public bool hapticOff;
            public bool isBgmMute;
            public bool isSfxMute;
        }
    }
}