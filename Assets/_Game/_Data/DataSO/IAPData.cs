using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

[CreateAssetMenu(fileName = "IAP Products", menuName = "ScriptableObjects/IAP Data")]
public class IAPData : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<IAP_ITEM, IAPItem> purchaseProducts;

    public Dictionary<IAP_ITEM, IAPItem> PurchaseProducts => purchaseProducts;
}

