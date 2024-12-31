using Base;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSeed", menuName = "ScriptableObjects/StaffSeed")]
public class StaffSeedSO : SerializedScriptableObject
{
    //[SerializeField]
    //Dictionary<int, List<Seed>> staffSeeds;
    //[SerializeField]
    //Dictionary<int, List<List<StaffColorProperty>>> staffColorPropertys;

    [SerializeField]
    Dictionary<int, Seed> staffSeedss;
    [SerializeField]
    Dictionary<int, List<StaffColorProperty>> staffColorPropertyss;
    public Dictionary<int, Seed> StaffSeeds => staffSeedss;

    public Dictionary<int, List<StaffColorProperty>> StaffColorPropertys => staffColorPropertyss;

    public void SetSeeds(int level, Seed staffSeed)
    {
        if (!staffSeedss.ContainsKey(level))
        {
            staffSeedss.Add(level, staffSeed);
        }
        else
        {
            staffSeedss[level] = staffSeed;
        }
    }

    public void AddSeed(int level, Seed staffSeed)
    {
        if (!staffSeedss.ContainsKey(level))
        {
            staffSeedss.Add(level, staffSeed);
        }
        else
        {
            staffSeedss[level] = staffSeed;
        }
    }

    public void AddStaffColors(int level, int index, StaffColorProperty prop)
    {
        if (!staffColorPropertyss.ContainsKey(level))
        {
            staffColorPropertyss.Add(level, new List<StaffColorProperty>());
        }

        staffColorPropertyss[level].Add(prop);
    }

    public void SetStaffColors(int level, int index, StaffColorProperty prop)
    {
        staffColorPropertyss[level][index] = prop;
    }

    [Serializable]
    public class Seed
    {
#if UNITY_EDITOR
        [TableList]
#endif
        public List<VehicleProp> Vehicles;

        public Seed(List<COLOR> staffColors, List<int> staffQuantitys)
        {
            Vehicles = new List<VehicleProp>();
            for (int i = 0; i < staffColors.Count; i++)
            {
                Vehicles.Add(new VehicleProp(staffColors[i], staffQuantitys[i]));
            }
        }
    }
    [Serializable]
    public class StaffColorProperty
    {
        public List<int> seed;
        public List<int> vehicleSetCounts;
        public float totalDispersion;
        public List<float> setDispersion;
        public List<int> randMags;
        public List<bool> isRandomSeed;
        public List<COLOR> colors;
    }
    [Serializable]
    public class VehicleProp
    {
        public COLOR Color;
        public int Quantity;

        public VehicleProp(COLOR color, int quantity)
        {
            this.Color = color;
            this.Quantity = quantity;
        }

    }
}



