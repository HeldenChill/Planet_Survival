using UnityEngine;


[CreateAssetMenu(fileName = "VehicleConfig", menuName = "ScriptableObjects/VehicleConfig")]
public class VehicleConfig : ScriptableObject
{
    [SerializeField]
    float speed;
    [SerializeField]
    float blockingSpeed;
    public float Speed => speed;
    public float BlockedSpeed => blockingSpeed;
}

