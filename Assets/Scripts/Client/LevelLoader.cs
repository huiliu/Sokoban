using System;
using System.Collections.Generic;
using Logic;

namespace Sokoban.Client
{
    public class LevelLoader
        : IMapLoader
    {
        void IMapLoader.LoadMap(string fileName, Action<List<string>> cb)
        {
            Bootstrap.Instance.ResourceMgr.LoadTextAsset(fileName, data =>
            {
                var lines = new List<string>();
                do
                {
                    if (string.IsNullOrEmpty(data))
                        break;

                    foreach (var l in data.Split('\n'))
                    {
                        //var t = l.Trim();
                        if (!string.IsNullOrEmpty(l))
                            lines.Add(l);
                    }
                } while (false);

                cb.SafeInvoke(lines);
            });
        }
    }
}
