using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Sokoban.Editor
{
    public class CreateAssetBundle
    {
        const string kAssetBundlesExportDir = "Assets/AssetBundles";
        static List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

        [MenuItem("Tool/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            BuildAllAssetBundles(EditorUserBuildSettings.activeBuildTarget);
        }

        public static void BuildAllAssetBundles(BuildTarget target = BuildTarget.NoTarget)
        {
            if (!Directory.Exists(kAssetBundlesExportDir))
            {
                Directory.CreateDirectory(kAssetBundlesExportDir);
            }

            builds.Clear();

            TagResource("Assets/" + ResourcePath.kPrefabPath, "*.prefab");
            TagResource("Assets/" + ResourcePath.kTexturePath, "*.png");
            TagResource("Assets/" + ResourcePath.kTextAssetPath, "*.txt");
            BuildPipeline.BuildAssetBundles(kAssetBundlesExportDir, builds.ToArray(), BuildAssetBundleOptions.None, target);

            if (target == BuildTarget.iOS ||
                target == BuildTarget.Android)
            {
                var p = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
                Directory.Delete(p, true);
                Directory.Move(kAssetBundlesExportDir, p);
            }
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

            foreach (var d in dir.GetDirectories())
            {
                TagResource(path + d.Name + "/", suffix);
            }
        }
    }
}