using System.Collections.Generic;
namespace Logic
{
    public class MapMgr
    {
        private static MapMgr instance = new MapMgr();
        public static MapMgr Instance { get { return instance; } }
        private MapMgr() { }

        public void LoadMap(string file)
        {
            this.Maps = Utils.LoadMap(file);
        }

        public List<LevelMap> Maps { get; private set; }

        public LevelMap GetLevelMap(int id)
        {
            Base.Log.Assert(id < this.Maps.Count);
            return this.Maps[id];
        }
    }
}
