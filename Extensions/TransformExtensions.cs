using UnityEngine;

namespace Dragonfoxing.UnityCSharp
{
    public static class TransformExtensions
{
    // Fancy quaternion stuff for looking at a target in 2D.
    public static void LookAt2D(this Transform t, Vector3 target)
    {
        t.rotation = Quaternion.LookRotation(Vector3.forward, target - t.position);
    }

    // Overload for putting in a transform instead of a position.
    public static void LookAt2D(this Transform t, Transform target)
    {
        t.LookAt2D(target.position);
    }

    // Lerped version of LookAt2D.  lt = lerpTime, since I'm using t for This object.
    // The lower lt is, the more sluggish the lookAt becomes.
    // Multiplying your lt by Time.deltaTime before you pass it in can make it even more sluggish.
    public static void LookAt2DLerped(this Transform t, Vector3 target, float lt = 0.75f)
    {
        t.rotation = Quaternion.Lerp(t.rotation, Quaternion.LookRotation(Vector3.forward, target - t.position), lt);
    }
    
    public static void LookAt2DLerped(this Transform t, Transform target, float lt = 0.75f)
    {
        t.LookAt2DLerped(target.position, lt);
    }
    
    // Function for moving in 2D without care for direction or rigidbodies.
    public static void Translate2D(this Transform t, float x, float y)
    {
        Vector3 currentPos = t.position;

        currentPos.x += x;
        currentPos.y += y;
    
        t.position = currentPos;
    }

    // Function for moving forward in 2D.
    // Because it's 2D, we compare our rotation to Vector3.up.
    // Theoretically you can do this with just transform.forward too.
    public static void TranslateForward2D(this Transform t, float speed, bool dt = true)
    {
        var _dir = t.rotation * Vector3.up;
        var _speed = (dt) ? Time.deltaTime * speed : speed;

        t.Translate(_dir * _speed, Space.World);
    }

    /*
        Function for automating the getting of localPosition values for 2D shadow objects.
        You would use this if you have a shadow object as a child of the object it's shadowing.
        Thus, you would put the return value in for the child's transform.localPosition.
        It might seem weird to have in extension, but it's something I commonly want access to.
    */
    public static Vector3 GetFakeShadow2DPosition(this Transform t, Vector3 dir, float amount, Vector3 rootPos = new Vector3())
    {
        var newShadowPos = Vector3.zero;

        newShadowPos.y = Mathf.Lerp(-amount, amount, (dir.y + 1) / 2);
        newShadowPos.x = -Mathf.Lerp(-amount, amount, (dir.x + 1) / 2);

        return rootPos - newShadowPos;
    }
}


}
