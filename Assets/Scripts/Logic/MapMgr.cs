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
            this.maps["current"] = Utils.LoadMap(file);
            this.CurrentMap = this.GetLevelMap(0);
        }

        public List<Map> Maps { get { return this.maps["current"]; } }
        public Map CurrentMap { get; private set; }

        public Map GetLevelMap(int id)
        {
            var curMap = this.maps["current"];
            Base.Log.Assert(id < curMap.Count);
            return curMap[id];
        }
    }
}
