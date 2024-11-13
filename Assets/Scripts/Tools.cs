using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static Vector3 Direction(Vector3 target, Vector3 origin)
    {
        return (target - origin).normalized;
    }
    public static bool RaycastOnLayer(Vector3 origin, Vector3 direction, float distance, LayerMask layer)
    {
        bool result = Physics.Raycast(origin, direction, distance, layer);
        return result;
    }
    public static bool RaycastExceptLayer(Vector3 origin, Vector3 direction, float distance, LayerMask layer)
    {
        bool result = Physics.Raycast(origin, direction, distance, ~layer);
        return result;
    }
    public static bool CheckIfInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}

