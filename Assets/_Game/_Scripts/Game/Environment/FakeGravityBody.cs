/*********************************************************************************************************
 * The FakeGravityBody class should be place on any moveable object you want drawn to your world
 * *******************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DesignPattern;

[RequireComponent(typeof(Rigidbody))]
public class FakeGravityBody : MonoBehaviour {

    // inspector variables
    [SerializeField, Tooltip("Attractor object to be drawn to, if left blank first available world will be used")]
    private FakeGravity attractor;
    [SerializeField]
    private bool ignoreYVel;
    [SerializeField, Tooltip("Set object solid once settled")]
    private bool setSolid = false;
    
    // privates
    private Transform _objTransform;
    private Rigidbody _objRigidbody;
    
    // properties
    public FakeGravity Attractor { get { return attractor; } set { attractor = value; } }
    public Transform Tf => _objTransform;
    public bool IgnoreYVel => ignoreYVel;
    public Rigidbody Rb => _objRigidbody;
    [HideInInspector]
    public float Distance;
    // Use this for initialization
	private void Start () {
        // set rigidbody
        _objRigidbody = GetComponent<Rigidbody>();
        _objRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _objRigidbody.useGravity = false;
        _objTransform = transform;
        // get attractor if not provided
        if (attractor == null)
        {
            attractor = GameObject.FindGameObjectWithTag("World").GetComponent<FakeGravity>();
        }
	}

    private void FixedUpdate()
    {
        if (_objRigidbody.isKinematic)
        {
            return;
        }
        // check if object sleeping yet
        if (setSolid)
        {
            ObjectResting();
        }
        if (attractor != null)
        {
            attractor.Attract(this);
        }
    }
    /// <summary>
    /// Check if rigidbody is sleeping
    /// </summary>
    private void ObjectResting()
    {
        if(_objRigidbody.IsSleeping())
        {
            _objRigidbody.isKinematic = true;
        }
    }
}
