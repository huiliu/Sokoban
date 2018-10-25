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

    protected void Start()
    {
        
    }

    private InputMgr InputMgr;
    private Pusher Pusher;
    private Map CurrentMap;
    private bool IsRunning;
    public void StartGame()
    {
        Log.Info("Game", "Start Game!\nMap Loading...");

        MapMgr.Instance.LoadMap("00.sok");
        this.CurrentMap = MapMgr.Instance.CurrentMap;
        this.MapText.text = this.CurrentMap.ToString();
        this.Pusher = MapMgr.Instance.CurrentMap.Pusher;

        this.InputMgr = this.gameObject.GetOrAddComponent<InputMgr>();
        this.IsRunning = true;

        this.MapComponent.SetupMap(this.CurrentMap);
    }

    protected void Update()
    {
        if (this.IsRunning)
            this.DoMove();
    }

    private void DoMove()
    {
        if (this.InputMgr.Up)
        {
            var r = this.Pusher.MoveUp();
            this.MapText.text = this.CurrentMap.ToString();
        }
        else if (this.InputMgr.Down)
        {
            var r = this.Pusher.MoveDown();
            this.MapText.text = this.CurrentMap.ToString();
            var finished = this.CurrentMap.Finished;
        }
        else if (this.InputMgr.Left)
        {
            var r = this.Pusher.MoveLeft();
            this.MapText.text = this.CurrentMap.ToString();
        }
        else if (this.InputMgr.Right)
        {
            var r = this.Pusher.MoveRight();
            this.MapText.text = this.CurrentMap.ToString();
        }
    }
}
