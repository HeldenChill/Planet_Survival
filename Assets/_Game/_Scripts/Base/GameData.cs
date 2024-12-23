using Base;
using Common;
using DesignPattern;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Database
{
    public static void Save<T>(T data) where T : new()
    {
        string dataString = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(typeof(T).Name, dataString);
        PlayerPrefs.Save();
    }

    public static T Load<T>() where T : new()
    {
        string key = typeof(T).Name;
        if (PlayerPrefs.HasKey(key))
        {
            return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
        }
        T data = new();
        Save(data);
        return data;
    }
}

namespace Base
{
    #region GAME DEFINE
    public static class CONSTANTS
    {
        public const string CHAR_COLLIDER_LAYER = "CharCollider";
        public const string ENEMY_COLLIDER_LAYER = "EnemyCollider";
        public const string PLAYER_COLLIDER_LAYER = "PlayerCollider";
        public const string GROUND_LAYER = "Ground";
        public const string IS_RUN_ANIM_NAME = "IsRun";
        public const string IS_IDLE_ANIM_NAME = "IsIdle";
        public const string JUMP_ANIM_NAME = "Jump";
        public const string IS_GROUNDED_ANIM_NAME = "IsGrounded";
        public const int MAX_LEVEL = 50;
    }
    #region ENUM
    public enum ALERT_STATE
    {
        NONE = -1,
        START = 0,
        MED_ALERT = 1,
        ALERT = 2,
    }
    public enum COLOR
    {
        RED = 0,
        BLUE = 1,
        GREEN = 2,
        ORANGE = 3,
        GRAY = 4,
        YELLOW = 5,
        PURPLE = 6,
        BROWN = 7,
        PINK = 8,
    }
    public enum VEHICLE_TYPE
    {
        NORMAL = 0,
        QUESTION = 1,
        ON_MOVING_BELT = 2,
    }
    public enum ITEM
    {
        REFRESH_BOOSTER = 0,
        CLEAR_BOOSTER = 1,
        SORT_BOOSTER = 2,
        GOLD = 3,
        HEART = 4,
        SLOT = 5,
        REVIVE = 6,
        
        // TEMPORARY, Change the data model to handle with list item later
        ALL_BOOSTER = 7,
        REFRESH_CLEAR_BOOSTER = 8,
    }
    #endregion
    #region GAMEPLAY DATA
    [Serializable]
    public class LevelData
    {
        public int index;
        public List<AnchorData> anchorData;
        public List<HolderSlotData> holderSlotData;
        public List<VehicleData> vehicleData;
        public List<SpawnSlotData> spawnSlotData;
        public List<MovingBeltSlotData> movingBeltSlotData;

        public LevelData() { }
        public LevelData(int index, List<HolderSlotData> holderSlotData, List<VehicleData> vehicleData
            , List<AnchorData> anchorData, List<SpawnSlotData> spawnSlotData, List<MovingBeltSlotData> movingBeltSlotData)
        {
            this.index = index;
            this.holderSlotData = holderSlotData;
            this.vehicleData = vehicleData;
            this.anchorData = anchorData;
            this.spawnSlotData = spawnSlotData;
            this.movingBeltSlotData = movingBeltSlotData;
        }
    }
    [Serializable]
    public class VehicleData
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public Vector3 boxColliderSize;
        public COLOR color;
        public PoolType skinType;
        public int capacity;
        public VEHICLE_TYPE vehicleType;

        public VehicleData(Transform tf, Vector3 boxColliderSize, COLOR color, PoolType skinType, int capacity, VEHICLE_TYPE vehicleType)
        {
            this.position = tf.position;
            this.rotation = tf.rotation.eulerAngles;
            this.scale = tf.localScale;
            this.boxColliderSize = boxColliderSize;
            this.skinType = skinType;
            this.color = color;
            this.capacity = capacity;
            this.vehicleType = vehicleType;
        }
    }
    [Serializable]
    public class SlotData
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public Vector3 boxColliderSize;
        public int capacity;

        public SlotData(Transform tf, Vector3 boxColliderSize, int capacity)
        {
            this.position = tf.position;
            this.rotation = tf.rotation.eulerAngles;
            this.scale = tf.localScale;
            this.boxColliderSize = boxColliderSize;
            this.capacity = capacity;
        }
    }

    [Serializable]
    public class HolderSlotData : SlotData
    {
        public HolderSlotData(Transform tf, Vector3 boxCollideSize, int capacity) : base(tf, boxCollideSize, capacity) { }
    }

    [Serializable]
    public class SpawnSlotData : SlotData
    {
        public List<VehicleData> vehicleData;
        public SpawnSlotData(List<VehicleData> vehicleData,Transform tf, Vector3 boxCollideSize, int capacity) : base(tf, boxCollideSize, capacity) 
        { 
            this.vehicleData = vehicleData;
        }
    }

    [Serializable]
    public class MovingBeltSlotData : SlotData
    {
        public List<VehicleData> vehicleData;
        public int vehicleShowCount;
        public MovingBeltSlotData(List<VehicleData> vehicleData, Transform tf, Vector3 boxCollideSize, int showVehicleCount, int capacity) : base(tf, boxCollideSize, capacity)
        {
            this.vehicleData = vehicleData;
            this.vehicleShowCount = showVehicleCount;
        }
    }
    [Serializable]
    public class AnchorData
    {
        public enum TYPE
        {
            NONE = -1,
            VEHICLE = 0,
            STAFF = 1
        }
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public Vector3 boxColliderSize;
        public TYPE type;

        public AnchorData(Transform tf, TYPE type)
        {
            this.position = tf.position;
            this.rotation = tf.rotation.eulerAngles;
            this.scale = tf.localScale;
            this.type = type;
        }
    }
    #endregion

    #region IAP DATA
    [Serializable]
    public class IAPItem
    {
        public string Name;
        public IAP_PRODUCT_TYPE Type;
        public string Id;
        public string Description;
        public string Price;
        public List<GameData.ItemData> rewards;
        public int TimeDuration;
    }
    #endregion
    [Serializable]
    public class ItemData
    {
#if UNITY_EDITOR
        [PreviewField(75)]
#endif
        public Sprite Icon;
        public string Name;
        public Sprite Frame;
        public ITEM Type;
        public string Description;
        public int Cost;
        public int WatchVideoCount;
    }
    #endregion
    public class GameData
    {
        public SettingData setting = new();
        public UserData user = new();

        public void InitData()
        {
            List<ITEM> items = Enum.GetValues(typeof(ITEM)).Cast<ITEM>().ToList();
            if(user.PurchasedItems == null)
            {
                user.PurchasedItems = new List<IAP_ITEM>();
            }
            if (user.ItemDatas == null)
            {
                user.ItemDatas = new ItemData[items.Count];
            }
            else
            {
                if(items.Count > user.ItemDatas.Length)
                {
                    ItemData[] newData = new ItemData[items.Count];
                    for(int i = 0; i < user.ItemDatas.Length; i++)
                    {
                        newData[i] = user.ItemDatas[i];
                    }
                    user.ItemDatas = newData;
                }
            }
            
            for(int i = 0; i < items.Count; i++)
            {
                if (user.ItemDatas[i] != null && user.ItemDatas[i].Item == items[i])
                    continue;
                else
                {
                    ItemData newData = new ItemData();
                    newData.Item = items[i];
                    newData.Quantity = 0;
                    user.ItemDatas[i] = newData;
                }
            }
        }
        public int ClaimItem(ITEM item, int value)
        {
            Locator.Ads.Analytic.EarnVirtualCurrency(item.ToString(), value, "");
            ItemData data = user.ItemDatas.First(x => x.Item == item);
            data.Quantity += value;
            return data.Quantity;
        }
        public int SpendItem(ITEM item, int value)
        {
            Locator.Ads.Analytic.SpendVirtualCurrency(item.ToString(), value, "");
            ItemData data = user.ItemDatas.First(x => x.Item == item);
            data.Quantity -= value;
            return data.Quantity;
        }
        public ItemData GetItemData(ITEM item)
        {
            return user.ItemDatas.First(x => x.Item == item);
        }
        public bool IsRemoveAds()
        {
            return user.PurchasedItems.Contains(IAP_ITEM.REMOVE_ADS);
        }
        [Serializable]
        public class UserData
        {
            // Level Progress Data
            public int normalLevelIndex;
            // Item Data
            public ItemData[] ItemDatas;
            public List<IAP_ITEM> PurchasedItems;
        }

        [Serializable]
        public class SettingData
        {
            public bool hapticOff;
            public bool isBgmMute;
            public bool isSfxMute;
        }
        [Serializable]
        public class ItemData
        {
            public ITEM Item;
            public int Quantity;
        }
    }
}