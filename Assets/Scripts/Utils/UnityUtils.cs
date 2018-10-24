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
}
