using UnityEngine;

public class PathTester
    : MonoBehaviour
{
    public void Output()
    {
        // C:/Users/liuh/AppData/LocalLow/DefaultCompany/Sokoban
        Debug.Log(string.Format("Application.persistentDataPath: {0}", Application.persistentDataPath));
        // D:/liuhui/Sokoban/Assets/StreamingAssets
        Debug.Log(string.Format("Application.streamingAssetsPath: {0}", Application.streamingAssetsPath));
        // C:/Users/liuh/AppData/Local/Temp/DefaultCompany/Sokoban
        Debug.Log(string.Format("Application.temporaryCachePath: {0}", Application.temporaryCachePath));
        // D:/liuhui/Sokoban/Assets
        Debug.Log(string.Format("Application.dataPath: {0}", Application.dataPath));

        MapMgr.Instance.LoadMap("Yoshio.sok");
    }
}
