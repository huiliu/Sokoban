using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Base;

public class Bootstrap
    : MonoBehaviour
{
    private void Awake()
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
    }
}
