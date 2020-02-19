using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragonfoxing.UnityCSharp
{
    public class CameraLerpToTarget : MonoBehaviour
    {
        public Transform target;

        private bool hasTarget;

        [Range(1, 4)]
        public float lerpTightness = 2;
    
        public float zDist = -10;
        private void Awake()
        {
            if (target == null) hasTarget = false;
            else hasTarget = true;
        }
        // Update is called once per frame
        void Update()
        {
            if (hasTarget)
            {
                var dest = target.position;
                dest.z = zDist;

                var ler = Vector3.Lerp(transform.position, dest, lerpTightness * Time.deltaTime);

                transform.position = ler;
            }
        }
    }
}

