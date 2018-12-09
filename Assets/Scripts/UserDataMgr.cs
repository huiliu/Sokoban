using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Logic;

    public class UserData
    {
        public class ScoreRecord
        {
            public static ScoreRecord defaultRecord = new ScoreRecord()
            {
                Mode = "",
            };

            public string Mode { get; set; }
            public int LevelID { get; set; }
            public int Score { get; set; }
            public int MoveCount { get; set; }
        }

        // 得分
        public List<ScoreRecord> Records { get; set; }
    }

    public class UserDataMgr
    {
        public static UserDataMgr Instance { get { return instance; } }
        private static UserDataMgr instance = new UserDataMgr();
        private string UserDataFile { get { return GameUtil.PersistDataPath + "/user.xml"; } }

        private UserData UserData;
        private UserDataMgr()
        {
            if (!File.Exists(UserDataFile))
                this.CreateUserDataFile();

            var serialize = new XmlSerializer(typeof(UserData));
            using(var reader = new StreamReader(UserDataFile))
            {
                this.UserData = (UserData)serialize.Deserialize(reader);
            }
        }

        private void CreateUserDataFile()
        {
            var tmp = new UserData();
            tmp.Records = new List<UserData.ScoreRecord>();
            this.DoSave(tmp);
        }

        public void Save()
        {
            this.DoSave(this.UserData);
        }

        private void DoSave(UserData data)
        {
            var serialize = new XmlSerializer(typeof(UserData));
            using(var writer = new StreamWriter(UserDataFile))
            {
                serialize.Serialize(writer, data);
            }
        }

        public UserData.ScoreRecord GetRecord(string mode, int id)
        {
            foreach(var r in this.UserData.Records)
            {
                if (r.LevelID == id && r.Mode == mode)
                    return r;
            }

            return UserData.ScoreRecord.defaultRecord;
        }

        public void AddOrUpdate(UserData.ScoreRecord record)
        {
            var r = this.GetRecord(record.Mode, record.LevelID);
            r.LevelID = record.LevelID;
            r.Score = record.Score;
            r.MoveCount = record.MoveCount;

            if (!string.IsNullOrEmpty(r.Mode))
            {
                r.Mode = record.Mode;
                this.UserData.Records.Add(r);
            }
        }
    }
