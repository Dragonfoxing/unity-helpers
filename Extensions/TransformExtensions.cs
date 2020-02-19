using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragonfoxing.UnityCSharp
{
    public static class TransformExtensions
    {
        public static void LookAt2D(this Transform t, Vector3 target)
        {
            t.rotation = Quaternion.LookRotation(Vector3.forward, target - t.position);
        }

        // Overload for putting in a transform instead of a position.
        public static void LookAt2D(this Transform t, Transform target)
        {
            t.LookAt2D(target.position);
        }
    
        public static void Translate2D(this Transform t, float x, float y)
        {
            Vector3 currentPos = t.position;

            currentPos.x += x;
            currentPos.y += y;
        
            t.position = currentPos;
        }
    
        public static void TranslateForward2D(this Transform t, float speed, bool dt = true)
        {
            var _dir = t.rotation * Vector3.up;
            var _speed = (dt) ? Time.deltaTime * speed : speed;

            t.Translate(_dir * _speed, Space.World);
        }
    }

}

