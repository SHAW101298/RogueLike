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
    public static Vector3 GetPositionOnGround(Vector3 pos)
    {
        //Debug.Log("POS = " + pos);
        pos.y += 10f;
        //Debug.Log("POS 2 = " + pos);
        Vector3 groundPos = new Vector3();
        LayerMask terrainLayer = LayerMask.GetMask("Terrain");

        RaycastHit hit;
        Physics.Raycast(pos, Vector3.down, out hit, 20f, terrainLayer);

        groundPos = hit.point;

        //Debug.Log("GroundPos = " + groundPos);
        return groundPos;
    }
}

