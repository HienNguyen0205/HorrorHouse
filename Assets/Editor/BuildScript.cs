using UnityEditor;
using UnityEngine;
using System.IO;

namespace HorrorHouse.Build
{
    public static class BuildScript
    {
        public static void BuildGame()
        {
            Debug.Log("[BUILD_CLI] Starting Windows 64-bit Build...");

            string buildDir = "Builds";
            if (!Directory.Exists(buildDir))
            {
                Directory.CreateDirectory(buildDir);
            }

            string buildPath = Path.Combine(buildDir, "HorrorHouse.exe");
            string[] scenes = new string[]
            {
                "Assets/Scenes/MainMenu.unity",
                "Assets/Scenes/GamePlay.unity",
                "Assets/Scenes/LoseScene.unity",
                "Assets/Scenes/WinScene.unity"
            };

            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = scenes;
            options.locationPathName = buildPath;
            options.target = BuildTarget.StandaloneWindows64;
            options.options = BuildOptions.None;

            var report = BuildPipeline.BuildPlayer(options);
            Debug.Log("[BUILD_CLI] Build result: " + report.summary.result + ", Output size: " + report.summary.totalSize + " bytes");
        }
    }
}
