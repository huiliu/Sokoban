using System;
using UnityEngine;
using UnityEngine.UI;
using Logic;

namespace Sokoban.Client
{
    public class GameComponent
        : MonoBehaviour
    {
        [SerializeField] private Text MapText;
        [SerializeField] private GameObject LevelNode;
        [SerializeField] private GameObject ControlUI;
        [SerializeField] private GameObject InputMgr;
        [SerializeField] private MapComponent MapComponent;
        [SerializeField] private StatusComponent StatusComponent;

        private LevelMgr LevelMgr;
        private LevelComponentEx LevelComponentEx;
        private InputComponent InputComponent;
        private CommandMgr CommandMgr;
        private void Awake()
        {

            this.InputComponent = this.InputMgr.GetComponent<InputComponent>();
            this.LevelComponentEx = this.LevelNode.GetComponentInChildren<LevelComponentEx>(true);
            this.LevelComponentEx.OnStartGame += OnStartGame;
            this.MapComponent.OnWin += OnWin;

            this.LevelMgr = new LevelMgr();
            this.LevelMgr.LoadLevel(Application.streamingAssetsPath + "/" + LevelMgr.kLevelMapFile);
            this.LevelComponentEx.ShowLevels(this.TotalLevelCount);

            this.SwitchUI(false);
        }

        private void OnWin(int obj)
        {
            var record = UserData.ScoreRecord.defaultRecord;
            record.Mode = "Easy";
            record.LevelID = this.CurrentMap.LevelID;
            record.MoveCount = this.StepCount;
            UserDataMgr.Instance.AddOrUpdate(record);

            this.IsRunning = false;
        }

        public int TotalLevelCount { get { return this.LevelMgr.Maps.Count; } }
        public int StepCount { get { return this.CommandMgr.CommandCount; } }
        public LevelMap CurrentMap { get; private set; }
        private bool IsRunning;
        private Pusher Pusher;
        private void OnStartGame(int levelID)
        {
            this.IsRunning = true;
            this.CurrentMap = this.LevelMgr.GetLevelMap(levelID);
            this.Pusher = this.CurrentMap.Pusher;
            this.MapComponent.SetupMap(this.CurrentMap);

            this.CommandMgr = new CommandMgr();
            this.InputComponent.Setup(this.Pusher);
            this.StatusComponent.Init(this);
            this.SwitchUI(true);
        }

        private void SwitchUI(bool flag)
        {
            this.MapText.SetActiveEx(flag);
            this.ControlUI.SetActiveEx(flag);
            this.MapComponent.SetActiveEx(flag);
            this.LevelNode.SetActiveEx(!flag);
        }

        /// <summary>
        /// 重新开始本关卡游戏
        /// </summary>
        public void Restart()
        {
            this.MapComponent.Reset();
            this.OnStartGame(this.CurrentMap.LevelID);
        }

        public void BackToLevel()
        {
            this.MapComponent.Reset();
            this.SwitchUI(false);
        }

        protected void Update()
        {
            if (this.IsRunning)
                this.DoMove();
        }

        private void DoMove()
        {
            do
            {
                var cmd = this.InputComponent.GetCommand();
                if (cmd == null)
                    break;

                this.CommandMgr.Execute(cmd);
                this.StatusComponent.Refresh();
            } while (true);

#if DEBUG
            this.MapText.text = this.CurrentMap.ToString();
#endif
        }
    }
}
