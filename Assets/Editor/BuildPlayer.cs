using System.IO;
using UnityEngine;
using UnityEditor;

namespace Sokoban.Editor
{
    public static class BuildPlayers
    {
        private static readonly string iOSBuildPath = Application.dataPath + "/../iOSBuild/";
        private static readonly string[] scenes = new[]
        {
            "Assets/Scenes/001.unity"
        };

        [MenuItem("Tool/Build/iOS")]
        public static void BuildiOS()
        {
            BuildPlayer(BuildTarget.iOS, iOSBuildPath);
        }

        private static void BuildPlayer(BuildTarget target, string path)
        {
            CreateAssetBundle.BuildAllAssetBundles(target);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var option = new BuildPlayerOptions();
            option.target = target;
            option.options = BuildOptions.None;
            option.locationPathName = path;
            option.scenes = scenes;

            var report = BuildPipeline.BuildPlayer(option);
            var summary = report.summary;

            if (summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.Log("Build iOS Succeeded!");
            }
            else if (summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
            {
                Debug.Log("Failed to build iOS!");
            }
        }
    }
}
