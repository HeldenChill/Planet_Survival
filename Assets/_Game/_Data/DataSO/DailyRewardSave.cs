using System;
using UnityEngine;

namespace HoangHH.DailyReward
{
    public static class DailyRewardSave
    {
        [Serializable]
        private class DailyRewardSaveData
        {
            public int currentProgress;
            public int dayOfYear = DateTime.Now.DayOfYear;
            
            // save the current time in sec
            public int lastFreeClaimTime;
            
            public bool IsVerifyNewDay()
            {
                if (dayOfYear == DateTime.Now.DayOfYear) return false;
                currentProgress = 0;
                dayOfYear = DateTime.Now.DayOfYear;
                return true;
            }
        }
        
        #region Save Data
        
        private static DailyRewardSaveData _saveData;
        private static DailyRewardSaveData SaveData
        {
            get
            {
                if (_saveData == null)
                {
                    _saveData = LoadDailyRewardSaveData();
                    SaveDailyRewardData();
                }
                else
                {
                    if (_saveData.IsVerifyNewDay())
                    {
                        SaveDailyRewardData();
                    }   
                }
                return _saveData;
            }
        }
        
        private const string SaveDataKey = "DailyRewardSaveData";
        
        private static DailyRewardSaveData LoadDailyRewardSaveData()
        {
            if (!PlayerPrefs.HasKey(SaveDataKey)) return new DailyRewardSaveData();
            string json = PlayerPrefs.GetString(SaveDataKey);
            return JsonUtility.FromJson<DailyRewardSaveData>(json);
        }
        
        private static void SaveDailyRewardData()
        {
            if (_saveData == null) return;
            string json = JsonUtility.ToJson(_saveData);
            PlayerPrefs.SetString(SaveDataKey, json);
            PlayerPrefs.Save();
        }
        
        public static int GetProgress => SaveData.currentProgress;
        public static int GetLastFreeClaimTime => SaveData.lastFreeClaimTime;
        
        public static void IncreaseProgress()
        {
            SaveData.currentProgress++;
            SaveDailyRewardData();
        }
        
        public static bool CanClaimFree => Mathf.Abs((int) DateTime.Now.TimeOfDay.TotalSeconds - SaveData.lastFreeClaimTime) 
                                           >= DailyRewardDataSO.FREE_REWARD_COOLDOWN_SEC;
        
        public static void ClaimFree()
        {
            SaveData.lastFreeClaimTime = (int) DateTime.Now.TimeOfDay.TotalSeconds;
            SaveDailyRewardData();
        }
        
        public static void ResetProgress()
        {
            _saveData = new DailyRewardSaveData();
            SaveDailyRewardData();
        }
        #endregion
    }
}