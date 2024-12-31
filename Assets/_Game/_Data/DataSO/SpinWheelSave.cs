using System;
using UnityEngine;

public static class SpinWheelSave
{
    [Serializable]
    private class SpinWheelSaveData
    {
        public bool isDoneSpinFreeToday;
        public int adsSpinToday;
        public int dayOfYear = DateTime.Now.DayOfYear;

        public bool IsVerifyNewDay()
        {
            if (dayOfYear == DateTime.Now.DayOfYear) return false;
            isDoneSpinFreeToday = false;
            adsSpinToday = 0;
            dayOfYear = DateTime.Now.DayOfYear;
            return true;
        }
    }

    #region Save Data

    private static SpinWheelSaveData _saveData;
    private static SpinWheelSaveData SaveData
    {
        get
        {
            if (_saveData == null)
            {
                _saveData = LoadSpinWheelSaveData();
                SaveSpinWheelData();
            }
            else
            {
                if (_saveData.IsVerifyNewDay())
                {
                    SaveSpinWheelData();
                }
            }
            return _saveData;
        }
    }
    private const string SaveDataKey = "SpinWheelSaveData";

    private static SpinWheelSaveData LoadSpinWheelSaveData()
    {
        if (!PlayerPrefs.HasKey(SaveDataKey)) return new SpinWheelSaveData();
        string json = PlayerPrefs.GetString(SaveDataKey);
        return JsonUtility.FromJson<SpinWheelSaveData>(json);
    }

    private static void SaveSpinWheelData()
    {
        if (_saveData == null) return;
        string json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(SaveDataKey, json);
        PlayerPrefs.Save();
    }

    public static bool IsDoneSpinFreeToday => SaveData.isDoneSpinFreeToday;
    public static int AdsSpinToday => SaveData.adsSpinToday;
    public static int DayOfYear => SaveData.dayOfYear;

    public static void OnSpinDoneSave()
    {
        if (!IsDoneSpinFreeToday) SaveData.isDoneSpinFreeToday = true;
        else SaveData.adsSpinToday++;
        SaveSpinWheelData();
    }

    public static void ResetSpinData()
    {
        _saveData = new SpinWheelSaveData();
        SaveSpinWheelData();
    }

    #endregion
}
