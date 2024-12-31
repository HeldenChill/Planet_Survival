using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "GameplayData", menuName = "ScriptableObjects/Gameplay")]
public class GameplayData : SerializedScriptableObject
{
    [SerializeField]
    private Dictionary<COLOR, Material> vehicleMaterials;
    public Dictionary<COLOR, Material> VehicleMaterials => vehicleMaterials;
    [SerializeField]
    private Dictionary<int, Vector2> vehicleSize;
    public Dictionary<int, Vector2> VehicleSize => vehicleSize;
    [SerializeField]
    private Dictionary<VFX, ParticleSystem> vfxs;
    public Dictionary<VFX, ParticleSystem> VFXS => vfxs;
    [SerializeField]
    private Dictionary<ITEM, ItemData> items;
    public Dictionary<ITEM, ItemData> Items => items;
    [SerializeField]
    private UniversalRendererData gameplayUniversalRendererData;
    public UniversalRendererData GameplayUniversalRendererData => gameplayUniversalRendererData;
    [SerializeField]
    private List<PoolType> emojis;
    public List<PoolType> Emojis => emojis;
    [SerializeField]
    private int reviveGoldCost;
    public int ReviveGoldCost => reviveGoldCost;
    [SerializeField]
    private int winLevelGold;
    public int WinLevelGold => winLevelGold;
}
