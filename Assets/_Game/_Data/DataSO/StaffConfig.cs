using UnityEngine;

[CreateAssetMenu(fileName = "StaffConfig", menuName = "ScriptableObjects/StaffConfig")]
public class StaffConfig : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float speed;
    public float Speed => speed;
}

