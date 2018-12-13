using System.IO;
using UnityEngine;
using Base;
using Logic;
using Sokoban.Client;

public class Bootstrap
    : MonoBehaviour
{
    [SerializeField]
    private GameObject GameNode;

    public static Bootstrap Instance { get; private set; }

    public LevelMgr LevelMgr { get; private set; }
    private UpdateMgr updateMgr;
    private void Awake()
    {
        Instance = this;
        this.InitLog();
        this.updateMgr = new UpdateMgr();
        this.updateMgr.Init();
    }

    private void OnEnable()
    {
        this.StartCoroutine(this.updateMgr.TryUpdate());
    }

    public IResourceMgr ResourceMgr { get; private set; }
    private void Update()
    {
        if (this.updateMgr.UpdateStatus == UpdateStatus.Finished &&
            this.ResourceMgr == null)
            this.InitResourceMgr();
    }

    private void InitResourceMgr()
    {
#if UNITY_EDITOR
        this.ResourceMgr = new EditorResourceMgr();
        this.ResourceMgr.Init();
#else
        this.ResourceMgr = new AssetBundleMgr();
        this.ResourceMgr.Init();
#endif

        this.LevelMgr = new LevelMgr(new LevelLoader());
        this.LevelMgr.LoadLevel(LevelMgr.kLevelMapFile);

        this.GameNode.SetActiveEx(true);
    }

    private void OnApplicationQuit()
    {
        Log.Stop();
        UserDataMgr.Instance.Save();
    }

    private void InitLog()
    {
        Log.Initialize();
#if DEBUG
        Log.SetFilterLevel(LogLevel.Info);
#endif

#if UNITY_EDITOR
        var logfile = Path.Combine(Application.dataPath, "../client.log");
#else
        var logfile = Path.Combine(Application.persistentDataPath, "../client.log");
#endif
        Log.AddOutput(new FileOutput(logfile));
        Log.AddOutput(new UnityOutput());
        Log.AddOutput(GMOutput.Instance);
        Log.AddIntegration(new UnityLogIntegration());
    }
}
