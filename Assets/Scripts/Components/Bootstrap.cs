using UnityEngine;
using UnityEngine.UI;
using Base;
using Logic;

public class Bootstrap
    : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Text MapText;
    [SerializeField] private MapComponent MapComponent;
    [SerializeField] private GameObject MapGameObject;
    [SerializeField] private GameObject ControlGameObject;
    [SerializeField] private GameObject LevelStatusNode;

    public static Bootstrap Instance { get; private set; }
    private void Awake()
    {
        Instance = this; 
    }

    public string Mode { get; private set; }
    public int LevelID { get; private set; }
    private Pusher Pusher;
    private LevelMap CurrentMap;
    private bool IsRunning;
    public void StartGame(int levelID = 0)
    {
        Log.Info("Game", "Start Game!\nMap Loading...");
        this.Mode = "Normal";
        this.LevelID = levelID;

        var MapFile = GameUtil.ConfigPath + "Crazy_Monk.sok";
        MapMgr.Instance.LoadMap(MapFile);
        this.CurrentMap = MapMgr.Instance.GetLevelMap(levelID);
        this.MapText.text = this.CurrentMap.ToString();
        this.Pusher = MapMgr.Instance.CurrentMap.Pusher;

        this.IsRunning = true;

        this.MapComponent.SetupMap(this.CurrentMap);

        this.MapGameObject.SetActive(false);
        this.ControlGameObject.SetActive(true);
        this.LevelStatusNode.SetActiveEx(false);
    }

    public void GameOver()
    {
        this.MapComponent.Reset();

        this.MapGameObject.SetActive(true);
        this.ControlGameObject.SetActive(false);
        this.LevelStatusNode.SetActiveEx(true);
    }

    protected void Update()
    {
        if (this.IsRunning)
            this.DoMove();
    }

    private void DoMove()
    {
        if (InputMgr.Instance.Up)
        {
            var r = this.Pusher.MoveUp();
            this.MapText.text = this.CurrentMap.ToString();
        }
        else if (InputMgr.Instance.Down)
        {
            var r = this.Pusher.MoveDown();
            this.MapText.text = this.CurrentMap.ToString();
            var finished = this.CurrentMap.Finished;
        }
        else if (InputMgr.Instance.Left)
        {
            var r = this.Pusher.MoveLeft();
            this.MapText.text = this.CurrentMap.ToString();
        }
        else if (InputMgr.Instance.Right)
        {
            var r = this.Pusher.MoveRight();
            this.MapText.text = this.CurrentMap.ToString();
        }
    }

    private void OnApplicationQuit()
    {
        UserDataMgr.Instance.Save();
    }
}
