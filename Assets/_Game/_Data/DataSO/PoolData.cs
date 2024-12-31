using DesignPattern;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolData", menuName = "ScriptableObjects/Pool")]
public class PoolData : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<PoolType, GameUnit> units;
    public Dictionary<PoolType, GameUnit> Units => units;
}
