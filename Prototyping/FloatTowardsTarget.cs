using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragonfoxing.UnityCSharp
{
    public class FloatTowardsTarget : GameEntity
    {
        public Transform target;
        public float gravitySpeed = 1f;

        public void Update()
        {
            if (!Paused)
            {
                Vector3 dir = target.position - transform.position;
                
                transform.position += (gravitySpeed * Time.deltaTime * dir.normalized);
            }
        }

        
    }

}
