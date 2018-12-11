using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class CreateAssetBundle
{
    const string kAssetBundlesExportDir = "Assets/AssetBundles";
    static List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

    [MenuItem("Tool/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        if (!Directory.Exists(kAssetBundlesExportDir))
        {
            Directory.CreateDirectory(kAssetBundlesExportDir);
        }

        builds.Clear();

        TagResource("Assets/" + ResourcePath.kPrefabPath, "*.prefab");
        TagResource("Assets/" + ResourcePath.kPrefabPath, "*.png");
        BuildPipeline.BuildAssetBundles(kAssetBundlesExportDir, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }

    static void TagResource(string path, string suffix)
    {
        var dir = new DirectoryInfo(path);
        foreach (var f in dir.GetFiles(suffix))
        {
            var fullpath = f.FullName;
            var idx = fullpath.IndexOf("Assets");
            var p = fullpath.Substring(idx);

            var b = new AssetBundleBuild();
            b.assetBundleName = p.Replace(Path.DirectorySeparatorChar, '_').Replace(".", "_");
            b.assetBundleVariant = "ab";
            b.assetNames = new string[1] { p };
            builds.Add(b);
        }

        foreach(var d in dir.GetDirectories())
        {
            TagResource(path + d.Name + "/", suffix);
        }
    }
}
