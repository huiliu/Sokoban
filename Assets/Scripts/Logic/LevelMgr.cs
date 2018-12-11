using System.Collections.Generic;
namespace Logic
{
    public class LevelMgr
    {
        public const string kLevelMapFile = "Configs/Minicosmos.sok";
        public LevelMgr() { }
        public void LoadLevel(string file)
        {
            this.Maps = Utils.LoadAndPurifyLevelFile(file);
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
