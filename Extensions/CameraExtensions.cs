using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions
{
    public static Vector3 GetMousePosition(this Camera c)
    {
        var pos = c.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
