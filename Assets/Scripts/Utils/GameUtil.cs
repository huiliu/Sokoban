using Logic;
using UnityEngine;

public static class GameUtil
{
    public static float MapLayerZ = 0f;
    public static float EntityLayerZ = -0.1f;

    public static Vector3 ToGamePosition(this Point cellPos)
    {
        var v = Vector3.zero;
        v.x = -3.5f + cellPos.col;
        v.y = - cellPos.row;
        return v;
    }

    public static Vector3 ToMapLayerPosition(this Point cellPos)
    {
        var v = cellPos.ToGamePosition();
        v.z = MapLayerZ;

        return v;
    }

    public static Vector3 ToEntityLayerPosition(this Point cellPos)
    {
        var v = cellPos.ToGamePosition();
        v.z = EntityLayerZ;

        return v;
    }
}
