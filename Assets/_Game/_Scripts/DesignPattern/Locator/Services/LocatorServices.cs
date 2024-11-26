using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Common
{
    #region DATA
    public interface IDataService
    {
        public T GetData<T>(int index = 0) where T : class;
        public T GetSOData<T>() where T : ScriptableObject;
        public T GetUnit<T>(int type) where T : class;
        public void Save();
    }
    #endregion
    #region ADS
    [SerializeField]
    public interface IAds
    {
        public void Show();
        public void Hide();
        public void Load();
    }
    public interface IRewardAds : IAds
    {
        public Action _OnTriggerLoadAds { get; }
        public Action<Action> _OnAddLoadAds { get; }
        public void Show(Action rewardCallBack, Action hiddenCallBack = null, Placement placement = Placement.NONE);
    }
    public interface IInterAds : IAds
    {
        public Action _OnTriggerLoadAds { get; }
        public Action<Action> _OnAddLoadAds { get; }
        public void Show(Action callback);
    }
    public interface IBannerAds : IAds
    {
        public enum TYPE
        {
            MAX = 0,
            COLLAPSIVE = 1,
            ADS_MOB = 2,
        }

        public void Show(TYPE type);
        public void Hide(TYPE type);
        public void Init();
    }
    public interface IAnalytic
    {
        public abstract void AdsRewardOffer(Placement place);
        public abstract void AdsRewardClick(Placement place);
        public abstract void AdsRewardShow(Placement place);
        public abstract void AdsRewardFail(Placement place, string error);
        public abstract void AdsRewardComplete(Placement place, string type);
        public abstract void AdsRewardLoad();

        public abstract void AdsInterFail(string error);

        public abstract void AdsInterLoad();

        public abstract void AdsInterShow(Placement place);
        public abstract void AdsInterClick();


        //public void ResourceSpend(BoosterType type, Resource.Placement place, int amount)
        //{
        //    if (!isFirebaseInit) return;

        //    FirebaseAnalytics.LogEvent("resource_spend", new Parameter[]
        //    {
        //        new Parameter("name", type.ToString()),
        //        new Parameter("placement", place.ToString()),
        //        new Parameter("value", amount)
        //    });
        //}

        public abstract void FireUserProps();

        public abstract void Day();
        public abstract void AppsFlyerTrackEvent(string name);

        public abstract void AppsFlyerTrackParamEvent(string name, Dictionary<string, string> param);

        public abstract void LevelTrackEvent(LEVEL_STATE state, int value);
        public abstract void EarnVirtualCurrency(string name, long value, string source);
        public abstract void SpendVirtualCurrency(string name, long value, string itemName);
    }
    public interface IAdsService
    {
        IAds AppOpen { get; }
        IAds Banner { get; }
        IRewardAds Reward { get; }
        IInterAds Inter { get; }
        IAnalytic Analytic { get; }
    }
    #endregion
    #region ADS
    public enum Placement
    {
        NONE = 0,
        IN_GAME = 1,
    }

    public enum LEVEL_STATE
    {
        START = 0,
        COMPLETE = 1,
        FAIL = 2,
    }
    #endregion
    #region IAP
    public enum IAP_ITEM
    {
        PIGGY_BANK = 0,
        REMOVE_ADS = 1,
        STARTER_PACK = 2,
        LIMITED_PACK = 3,
        BIG_BUNDLE = 4,
        GREAT_BUNDLE = 5,
        ULTRA_BUNDLE = 6,
        SUPERIOR_BUNDLE = 7,
        LENGENDARY_BUNDLE = 8,
        GOLD_PACK1 = 9,
        GOLD_PACK2 = 10,
        GOLD_PACK3 = 11,
        GOLD_PACK4 = 12,
        GOLD_PACK5 = 13,
        GOLD_PACK6 = 14,

    }

    public enum IAP_PRODUCT_TYPE
    {
        CONSUMABLE = 0,
        NON_CONSUMABLE = 1,
        SUBSCRIPTION = 2,
    }

    public interface IIAPService
    {
        public void Purchase(IAP_ITEM item, Action onPuchaseCompleted, Action onPurchaseFail = null);
    }
    #endregion
    #region AUDIO
    public enum SFX_TYPE
    {
        NONE = 0,
        CLICK = 1,
        BOOSTER_CLAIM = 2,
        SPIN_TICK = 3,
        SPIN_END = 4,
        COIN = 5,
        POPUP_HIDE = 6,
        UNLOCK_BOAT = 7,
        POPUP_SHOW = 8,
        ITEM_CLAIM = 9,
        REFRESH_BOOSTER = 10,
        SORT_BOOSTER = 11,
        BOAT_MOVE_1 = 12,
        BOAT_MOVE_2 = 13,
        HUMAN_POP_1 = 14,
        HUMAN_POP_2 = 15,
        HUMAN_POP_3 = 16,
        HUMAN_POP_4 = 17,
        BOAT_PASSING = 18,
        HOLDER_UNLOCK = 19,

        BOAT_COLLISION_1 = 20,
        BOAT_COLLISION_2 = 21,
        BOAT_COLLISION_3 = 22,

        UNLOCK = 23,
        WIN = 24,
        LOSE = 25,
        CLEAR_BOOSTER = 26,
        BOAT_DIVING = 27,
        BOAT_SURFACE = 28,
        BOAT_WADE = 29,
        BOAT_SPLASH_WATER = 30,
        PASSENGER_SWAP = 31,
        BOAT_SWAP = 32,
        
        SPIN_WHEEL = 50,

        KEY_ROTATE = 100,
    }

    public enum BGM_TYPE
    {
        NONE = -1,
        WAVE = 0,
        HOME = 1,
    }

    public interface IAudioService
    {
        public void PlayBgm(BGM_TYPE type, float fadeOut = 0.3f);
        public void PlaySfx(SFX_TYPE type);
        public AudioSource PlayLoopSfx(SFX_TYPE type, float fadeIn = 0.1f);
        public void StopSfx(SFX_TYPE type = SFX_TYPE.NONE);
        public void StopLoopSfx(AudioSource source, float fadeOut = 0.1f);
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
        public T GetLevelData<T>(int level = 0) where T : class;
    }
    public interface IGameplayService
    {
        public void ConstructLevel(int level);
        public void DestructLevel();
        public void UsingBooster(int type);
        public int IsCanUseBooster(int type);
        public void Revive();
    }
    #endregion
}