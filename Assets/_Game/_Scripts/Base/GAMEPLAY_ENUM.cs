using UnityEngine;

namespace Base
{
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
    public enum EFFECT
    {        
        NONE = -1,
        WIND = 1,
        BURN = 2,
        ZAP = 3,
        FREEZE = 4,
        WOUND = 5,
        POISON = 6,
        CHANGE_TEMP = 7,

        BLOOD_LOSS = 100, //WOUND + BURN
        OVERLOAD = 101, //BURN + ZAP
        ICE_BREAKING = 102, //ZAP + FREEZE
        ICE_MELTING = 103, //FREEZE + BURN
    }
    public enum HIT_IMPACT_TYPE
    {
        NONE = -1,
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
}
