using DesignPattern;
using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class SphereEntity : GameUnit
    {
        [SerializeField]
        float acceleration;
        [SerializeField]
        List<GameUnit> attractUnits;
        List<Vector3> attractUnitsGravityVectors;

        private void Awake()
        {
            //attractUnits = new List<GameUnit>();
            attractUnitsGravityVectors = new List<Vector3>();
        }

        private void FixedUpdate()
        {
            attractUnitsGravityVectors.Clear();
            for(int i = 0; i < attractUnits.Count; i++)
            {
                attractUnitsGravityVectors.Add(attractUnits[i].Tf.position - Tf.position);
                attractUnits[i].Tf.rotation = Quaternion.FromToRotation(Vector3.up, attractUnitsGravityVectors[i]);
            }
        }
    }
}