using UnityEngine;
using UnityEngine.UI;

public struct LevelData
{
    public int ID    { get; private set; }
    public int Score { get; private set; }

    public LevelData(int id)
    {
        this.ID = id;
        this.Score = -1;
    }
}

public class LevelComponent
    : MonoBehaviour
{
    [SerializeField] private Button EnterLevel;
    [SerializeField] private GameObject Tips;
    [SerializeField] private ScoreComponent ScoreNode;


    private void Awake()
    {
        this.Tips.SetActiveEx(false);
        //this.ScoreNode.SetActiveEx(false);
        this.EnterLevel.onClick.AddListener(this.OnClick);
    }

    private void OnClick()
    {
        Bootstrap.Instance.StartGame(this.LevelID);
    }

    private int LevelID = -1;
    public void SetLevelID(int id)
    {
        this.LevelID = id;
        this.Refresh();
    }

    private void Refresh()
    {
        var data = UserDataMgr.Instance.GetRecord(Bootstrap.Instance.Mode, this.LevelID);
        if (!string.IsNullOrEmpty(data.Mode))
        {
            this.ScoreNode.Setup(data.Score);
        }

        this.ScoreNode.SetActiveEx(!string.IsNullOrEmpty(data.Mode));
    }
}
