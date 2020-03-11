using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragonfoxing.UnityCSharp {
    
    public class CameraKeepTargetsInFrame : MonoBehaviour
    {
        // Our camera component.
        private Camera cam;

        // zVal defaults to the normal camera distance in new projects.
        public float zVal = -10;
        public float minOrthoSize = 5;
        public float orthoPadding = 2;

        public bool useRelativeCenter = false;
        // Option to track a primary target, aka KeepNTargetsAroundObjectInFrame.
        // If this is on, useRelativeCenter is ignored.
        public bool useTargetPosition = false;
        public Transform primaryTarget;

        public List<Transform> targets;

        private float xMin, xMax, yMin, yMax;
        private Vector3 median;

        private bool noSecondaryTargets = false;
        private void OnEnable()
        {
            cam = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            TrackFramedObjects();
            ChangePosition();
            ChangeOrthoSize();

        }

        private void TrackFramedObjects()
        {
            if (targets.Count == 0)
            {
                noSecondaryTargets = true;
                return;
            }
            else noSecondaryTargets = false;

            median = Vector3.zero;
            bool firstPass = true;

            foreach (Transform t in targets)
            {
                var x = t.position.x;
                var y = t.position.y;

                if (firstPass)
                {
                    xMin = x;
                    xMax = x;

                    yMin = y;
                    yMax = y;

                    firstPass = false;
                }
                else
                {
                    if (x < xMin) xMin = x;
                    else if (x > xMax) xMax = x;

                    if (y < yMin) yMin = y;
                    else if (y > yMax) yMax = y;
                }


                median += t.position;
            }
        }

        private void ChangePosition()
        {
            if (useTargetPosition) transform.position = GetTargetPosition();
            else if (useRelativeCenter) transform.position = GetRelativeCenter();
            else transform.position = GetAbsoluteCenter();
        }


        #region position_functions

        private Vector3 GetAbsoluteCenter()
        {
            var absoluteCenter = Vector3.zero;
            absoluteCenter.x = xMin + Mathf.Abs(xMax - xMin) / 2;
            absoluteCenter.y = yMin + Mathf.Abs(yMax - yMin) / 2;
            absoluteCenter.z = zVal;

            return absoluteCenter;
        }

        private Vector3 GetRelativeCenter()
        {
            median /= targets.Count;
            median.z = zVal;

            return median;
        }

        private Vector3 GetTargetPosition()
        {
            var newPos = primaryTarget.position;
            newPos.z = zVal;

            return newPos;
        }

        #endregion

        private void ChangeOrthoSize()
        {
            cam.orthographicSize = GetRequiredOrthoSize();
        }

        private float GetRequiredOrthoSize()
        {
            if (useTargetPosition && noSecondaryTargets) return minOrthoSize;

            var xVal = 0f;

            xVal = transform.position.x - xMin;
            xVal = (xVal > xMax - transform.position.x) ? xVal : xMax - transform.position.x;

            xVal += orthoPadding;
            xVal /= cam.aspect;

            var yVal = 0f;

            yVal = transform.position.y - yMin;
            yVal = (yVal > yMax - transform.position.y) ? yVal : yMax - transform.position.y;

            yVal += orthoPadding;

            if (xVal > minOrthoSize || yVal > minOrthoSize)
            {
                return (xVal > yVal) ? xVal : yVal;
            }
            return cam.orthographicSize = minOrthoSize;
        }
    }
}
