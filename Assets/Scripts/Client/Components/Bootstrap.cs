using System.IO;
using UnityEngine;
using Base;

public class Bootstrap
    : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }

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

    private IResourceMgr resourceMgr;
    private void Update()
    {
        if (this.updateMgr.UpdateStatus == UpdateStatus.Finished &&
            this.resourceMgr == null)
            this.InitResourceMgr();
    }

    private void InitResourceMgr()
    {
        this.resourceMgr = new AssetBundleMgr();
        this.resourceMgr.Init();
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

        var logfile = Path.Combine(Application.streamingAssetsPath, "../../Log/client.log");
        Log.AddOutput(new FileOutput(logfile));
        Log.AddOutput(new UnityOutput());
        Log.AddOutput(GMOutput.Instance);
        Log.AddIntegration(new UnityLogIntegration());
    }
}
