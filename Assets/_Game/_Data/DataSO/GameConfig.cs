using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    int startInterLevel;
    [SerializeField]
    int interCappingTime;
    public int StartInterLevel => startInterLevel;
    public float InterCappingTime => interCappingTime;
}

