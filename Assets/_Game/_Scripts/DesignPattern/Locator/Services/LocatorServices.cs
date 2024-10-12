using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    #region DATA
    public interface IDataService
    {
        public T GetData<T>() where T : class;
        public T GetSOData<T>() where T : ScriptableObject;
        public T GetUnit<T>(int type) where T : class;
        public void Save();
    }
    #endregion

    #region ADS
    public interface IAds
    {
        public void Show();
        public void Hide();
        public void Load();
    }

    public interface IRewardAds : IAds
    {
        public void Show(Action callback);
    }

    public interface IInterAds : IAds
    {
        public void Show(Action callback);
    }

    public interface IBannerAds : IAds
    {
        public enum TYPE
        {
            MAX = 0,
            COLLAPSIVE = 1,
        }

        public void Show(TYPE type);
        public void Hide(TYPE type);
        public void Init();
    }
    public interface IAdsService
    {
        IAds AppOpen { get; }
        IAds Banner { get; }
        IRewardAds Reward { get; }
        IInterAds Inter { get; }
    }
    #endregion

    #region AUDIO
    public enum SFX_TYPE
    {
        NONE = 0,
        CLICK = 1,
        BOUNCE_UP = 2,
        BOUNCE_DOWN = 3,
        BOX_BLOCK = 4,
        DROP_GOLD = 5,
        COLUMN_COMPLETE = 6,
        BOX_CORRECT_END = 8,
        WIN = 9,
        LOSE = 10,
        BOX_CORRECT_1 = 11,
        BOX_CORRECT_2 = 12,
        BOX_CORRECT_3 = 13,

        REVIVE = 14,
        BOOSTER_ADD_COLUMN = 15,
        BOOSTER_SWAP = 16,
        BOOSTER_MIX = 17,
        BOX_MOVE = 18,
        BOX_CORRECT_4 = 19,
        BOX_CORRECT_5 = 20,
    }

    public enum BGM_TYPE
    {

    }

    public interface IAudioService
    {
        public void PlayBgm(BGM_TYPE type, float fadeOut = 0.3f);
        public void PlaySfx(SFX_TYPE type);
        public void StopSfx(SFX_TYPE type = SFX_TYPE.NONE);
        public void PlayRandomSfx(List<SFX_TYPE> sfxTypes);
        public void PauseBgm();
        public void UnPauseBgm();
        public void StopBgm();
        public void ToggleBgmVolume(bool isMute);
        public void ToggleSfxVolume(bool isMute);
    }
    #endregion
    #region GAME
    public interface ILevelService
    {
        public T GetLevelData<T>(int level) where T : class;
    }
    #endregion
}