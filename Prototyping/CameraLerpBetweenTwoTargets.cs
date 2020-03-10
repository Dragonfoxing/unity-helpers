using UnityEngine;

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
        var xMedian = Mathf.Abs(firstTarget.position.x - secondTarget.position.x) + orthoPadding;
        xMedian /= 2;

        var orthoSize = xMedian / cam.aspect;
        return (orthoSize > minOrthoSize) ? orthoSize : minOrthoSize;
    }
}
