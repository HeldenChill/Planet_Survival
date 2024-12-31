using System;
using UnityEngine;

public static class PiggyBankSave
{
    [Serializable]
    private class PiggyBankSaveData
    {
        public int currentLevelProgress;
    }

    #region Save Data

    private static PiggyBankSaveData _saveData;
    private static PiggyBankSaveData SaveData => _saveData ??= LoadPiggyBankSaveData();
    private const string SaveDataKey = "PiggyBankSaveData";
    private const int PASS_LEVEL_MULTIPLE_PROGRESS = 100;

    public static int PiggyGoldProgress => GetLevelProgress * PASS_LEVEL_MULTIPLE_PROGRESS;

    private static int GetLevelProgress => SaveData.currentLevelProgress;

    private static PiggyBankSaveData LoadPiggyBankSaveData()
    {
        if (!PlayerPrefs.HasKey(SaveDataKey)) return new PiggyBankSaveData();
        string json = PlayerPrefs.GetString(SaveDataKey);
        return JsonUtility.FromJson<PiggyBankSaveData>(json);
    }

    public static void IncreasePiggyProgress()
    {
        SaveData.currentLevelProgress++;
        SavePiggyBankData();
    }

    public static void ResetPiggyProgress()
    {
        SaveData.currentLevelProgress = 0;
        SavePiggyBankData();
    }

    private static void SavePiggyBankData()
    {
        if (_saveData == null) return;
        string json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(SaveDataKey, json);
        PlayerPrefs.Save();
    }

    #endregion
}
