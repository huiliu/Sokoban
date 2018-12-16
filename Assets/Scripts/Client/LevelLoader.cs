using System;
using System.Collections.Generic;
using Logic;

namespace Sokoban.Client
{
    public class LevelLoader
        : IMapLoader
    {
        void IMapLoader.LoadMap(string fileName, Action<string[]> cb)
        {
            Bootstrap.Instance.ResourceMgr.LoadTextAsset(fileName, data =>
            {
                cb.SafeInvoke(data?.Split('\n'));
            });
        }
    }
}
