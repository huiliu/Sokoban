using System;
using System.Collections.Generic;

namespace Logic
{
    public class LevelMgr
    {
        public const string kLevelMapFile = "Configs/Minicosmos.sok";
        private readonly IMapLoader MapLoader;
        public LevelMgr(IMapLoader loader)
        {
            this.MapLoader = loader;
        }

        /// <summary>
        /// 加载关卡数据
        /// </summary>
        /// <param name="file">Data File. 路径为相对于Assets/TextAssets/</param>
        public void LoadLevel(string file)
        {
            this.MapLoader.LoadMap(file, lines =>
            {
                this.Maps = Utils.LoadAndPurifyLevelFile(lines);
            });
        }

        public IList<List<string>> Maps { get; private set; }
        public LevelMap GetLevelMap(int id)
        {
            Base.Log.Assert(id < this.Maps.Count);
            /// TODO: 此函数不好，隐含着解析关卡数据文件操作
            var levelMap = Utils.ParseLevelData(this.Maps[id]);
            levelMap.LevelID = id;

            return levelMap;
        }
    }
}
