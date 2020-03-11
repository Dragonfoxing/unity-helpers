using UnityEngine;

namespace Dragonfoxing.UnityCSharp {
    
    public class CameraLerpBetweenTwoTargets : MonoBehaviour
    {
        // Reference to our camera component.
        private Camera cam;

        // Target references.
        public Transform firstTarget;
        public Transform secondTarget;

        // The distance 
        public float zVal = -10;

        public float minOrthoSize = 5.0f;
        public float orthoPadding = 2f;
        private void OnEnable()
        {
            cam = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            ChangePosition();
            ChangeOrthoSize();
        }

        private void ChangePosition()
        {
            var median = firstTarget.position + secondTarget.position;
            median /= 2;
            median.z = zVal;

            transform.position = median;
        }

        private void ChangeOrthoSize()
        {
            cam.orthographicSize = GetRequiredOrthoSize();
        }

        // Solving for the problem by using ortho solution as presented here:
        // https://gamedev.stackexchange.com/a/158597

        // Get the length between the two targets' x positions.
        // Add our ortho padding, then divide by two to get our half-width.
        // Divide our half-width by the camera.aspect to get the orthoHeight we need.
        private float GetRequiredOrthoSize()
        {
            var halfWidth = Mathf.Abs(firstTarget.position.x - secondTarget.position.x) + orthoPadding;
            halfWidth /= 2;
            halfWidth /= cam.aspect;

            var halfHeight = Mathf.Abs(firstTarget.position.y - secondTarget.position.y) + orthoPadding;
            halfHeight /= 2;


            // Now we have two orthographicSize values.
            // We want to use whichever is larger.
            if (halfWidth > minOrthoSize || halfHeight > minOrthoSize) return (halfWidth > halfHeight) ? halfWidth : halfHeight;

            // If neither are larger than the minOrthoSize, we default to return it instead.
            return minOrthoSize;
        }
    }
}
