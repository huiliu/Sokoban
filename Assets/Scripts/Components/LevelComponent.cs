using UnityEngine;
using UnityEngine.UI;

public struct LevelData
{
    public int ID { get; private set; }

    public LevelData(int id)
    {
        this.ID = id;
    }
}

public class LevelComponent
    : MonoBehaviour
{
    [SerializeField] private Button EnterLevel;

    private void Start()
    {
        this.EnterLevel.onClick.AddListener(this.OnClick);
    }

    private void OnClick()
    {
        Bootstrap.Instance.StartGame(this.data.ID);
    }

    private LevelData data;
    public void Setup(LevelData data)
    {
        this.data = data;

        this.Refresh();
    }

    private void Refresh()
    {

    }
}
