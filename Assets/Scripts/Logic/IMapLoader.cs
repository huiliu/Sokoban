using System.IO;
using System.Collections.Generic;

namespace Logic
{
    public interface IMapLoader
    {
        List<string> LoadMap(string fileName);
    }

    public class LogicMapLoader
        : IMapLoader
    {
        public List<string> LoadMap(string fileName)
        {
            var Lines = new List<string>();
            var lines = File.ReadAllLines(fileName);
            foreach(var l in lines)
            {
                var t = l.Trim();
                if (!string.IsNullOrEmpty(t))
                    Lines.Add(t);
            }

            return Lines;
        }
    }
}
