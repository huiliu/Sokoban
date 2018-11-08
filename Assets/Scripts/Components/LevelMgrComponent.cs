using UnityEngine;
using Logic;

public class LevelMgrComponent
    : MonoBehaviour
{
    private void Start()
    {
        var levels = GetComponentsInChildren<LevelComponent>();
        var maps = MapMgr.Instance.Maps;
        Base.Log.Assert(levels.Length >= maps.Count);

        for (var i = 0; i < maps.Count; ++i)
        {
            var leveldata = new LevelData(i);
            //levels[i].Setup(leveldata);
        }
    }
}
