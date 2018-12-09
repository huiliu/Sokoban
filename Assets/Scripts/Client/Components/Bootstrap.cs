using System.IO;
using UnityEngine;
using Base;

public class Bootstrap
    : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        this.InitLog();
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

    private void OnApplicationQuit()
    {
        Log.Stop();
        UserDataMgr.Instance.Save();
    }
}
