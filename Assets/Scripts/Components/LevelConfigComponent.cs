using UnityEngine;

public class LevelConfigComponent
    : MonoBehaviour
{
    [SerializeField]
    private int LevelID;
    [SerializeField]
    private GameObject LevelPrefab;

    private LevelComponent LevelComponent;
    private void Awake()
    {
        var go = Instantiate(this.LevelPrefab);
        go.transform.SetParent(this.transform, false);
        go.SetActive(true);

        this.LevelComponent = go.GetComponent<LevelComponent>();
    }

    private void OnEnable()
    {
        this.LevelComponent.SetLevelID(this.LevelID);
    }
}
