using System;
using System.IO;
using System.Collections.Generic;

namespace Logic
{
    public interface IMapLoader
    {
        void LoadMap(string fileName, Action<string[]> cb);
    }

    public class LogicMapLoader
        : IMapLoader
    {
        public void LoadMap(string fileName, Action<string[]> cb)
        {
            var lines = File.ReadAllLines(fileName);
             cb.SafeInvoke(lines);
        }
    }
}
