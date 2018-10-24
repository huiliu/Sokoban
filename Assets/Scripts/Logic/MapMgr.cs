using System.Collections.Generic;
namespace Logic
{
    public class MapMgr
    {
        private static MapMgr instance = new MapMgr();
        public static MapMgr Instance { get { return instance; } }
        private MapMgr() { }

        Dictionary<string, List<Map>> maps = new Dictionary<string, List<Map>>();
        public void LoadMap(string file)
        {
            Base.Log.Assert(!this.maps.ContainsKey(file));
            this.maps[file] = Utils.LoadMap(file);
            this.CurrentMap = this.maps[file][0];
        }

        public Map CurrentMap { get; private set; }
    }
}
