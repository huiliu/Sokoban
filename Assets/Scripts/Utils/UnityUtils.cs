using UnityEngine;

// Unity Utilities
public static class UnityUtils
{
    public static T GetOrAddComponent<T>(this MonoBehaviour m) where T : MonoBehaviour
    {
        var t = m.gameObject.GetComponent<T>();
        if (t == null)
            t = m.gameObject.AddComponent<T>();

        return t;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
    {
        var t = gameObject.GetComponent<T>();
        if (t == null)
            t = gameObject.AddComponent<T>();

        return t;
    }

    /// <summary>
    /// 叉积
    /// 
    /// > 0 other在self的顺时针方向
    /// < 0 other在self的逆时针方向
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static float Cross(this Vector2 self, Vector2 other)
    {
        return self.x * other.y - self.y * other.x;
    }

    public static Vector2 Rotate(this Vector2 self, float angleDeg)
    {
        var RadAngle = angleDeg * Mathf.Deg2Rad;
        var x1 = self.x * Mathf.Cos(RadAngle) - self.y * Mathf.Sin(RadAngle);
        var y1 = self.x * Mathf.Sin(RadAngle) + self.y * Mathf.Cos(RadAngle);

        return new Vector2(x1, y1);
    }

    /// <summary>
    /// Vector2 -> Vector3
    ///
    /// (v2.x, v2.y, 0)
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        Vector2 n = v;
        return n;
    }

    public static Direction ToDirection(this Vector2 v)
    {
        if (v.x > 0)
        {
            if (Mathf.Abs(v.y) <= v.x || Mathf.Abs(v.y) < float.Epsilon)
                return Direction.Right;
            else if (v.y > 0)
                return Direction.Up;
            else
                return Direction.Down;
        }
        else if(v.x<0)
        {
            if (Mathf.Abs(v.y) <= -v.x || Mathf.Abs(v.y) < float.Epsilon)
                return Direction.Left;
            else if (v.y > 0)
                return Direction.Up;
            else
                return Direction.Down;
        }
        //else if (Mathf.Abs(v.x)<float.Epsilon)
        else
        {
            return v.y > 0 ? Direction.Up : v.y < 0 ? Direction.Down : Direction.None;
        }
    }
}
