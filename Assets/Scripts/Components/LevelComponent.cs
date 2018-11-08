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

    private void Awake()
    {
        this.EnterLevel.onClick.AddListener(this.OnClick);
    }

    private void OnClick()
    {
        Bootstrap.Instance.StartGame(this.data.ID);
    }

    private LevelData data;
    private int LevelID = -1;
    public void SetLevelID(int id)
    {
        this.LevelID = id;
        this.Refresh();
    }

    private void Refresh()
    {

    }
}
