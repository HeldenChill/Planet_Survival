using DesignPattern;
using System.Collections.Generic;
using UnityEngine;

namespace _Game
{
    public class SphereEntity : GameUnit
    {
        [SerializeField]
        List<GameUnit> attractUnits;
        List<Vector3> attractUnitsGravityVectors;
        List<float> unitDistances;

        private void Awake()
        {
            //attractUnits = new List<GameUnit>();
            attractUnitsGravityVectors = new List<Vector3>();
            unitDistances = new List<float>();
            for(int i = 0; i < attractUnits.Count; i++)
            {
                unitDistances.Add((attractUnits[i].Tf.position - Tf.position).magnitude);
            }
        }

        private void FixedUpdate()
        {
            attractUnitsGravityVectors.Clear();
            for(int i = 0; i < attractUnits.Count; i++)
            {
                attractUnitsGravityVectors.Add(attractUnits[i].Tf.position - Tf.position);
                Vector2 forwardDirectionPlane = attractUnits[i].Tf.forward;
                attractUnits[i].Tf.rotation = Quaternion.FromToRotation(Vector3.up, attractUnitsGravityVectors[i]);
                attractUnits[i].Tf.position = unitDistances[i] * attractUnitsGravityVectors[i].normalized + Tf.position;
            }
        }
    }
}