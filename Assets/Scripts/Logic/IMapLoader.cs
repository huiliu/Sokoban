using System;
using System.IO;
using System.Collections.Generic;

namespace Logic
{
    public interface IMapLoader
    {
        void LoadMap(string fileName, Action<List<string>> cb);
    }

    public class LogicMapLoader
        : IMapLoader
    {
        public void LoadMap(string fileName, Action<List<string>> cb)
        {
            var Lines = new List<string>();
            var lines = File.ReadAllLines(fileName);
            foreach(var l in lines)
            {
                var t = l.Trim();
                if (!string.IsNullOrEmpty(t))
                    Lines.Add(t);
            }

             cb.SafeInvoke(Lines);
        }
    }
}
