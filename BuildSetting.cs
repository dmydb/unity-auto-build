using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class BuildSetting : EditorWindow
{

    private static string[] levels = {
        "Assets/Scenes/login.unity",
     };

    private const string AndroidAPKName = "Project.apk";
    private const string IphoneAPKName = "XCodeProject";
    private static BuildTarget _buildTarget = (BuildTarget)(-1);

    public static void BuildAndroidAssets()
    {
        _buildTarget = BuildTarget.Android;
        BuildRelease();
    }

    public static void BuildIosAssets()
    {
        _buildTarget = BuildTarget.iOS;
        BuildRelease();
    }

    public static void BuildRelease()
    {
        {
            BuildOptions options = BuildOptions.ShowBuiltPlayer;

            switch (_buildTarget)
            {
                case BuildTarget.iOS:
                    if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                    {
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
                    }
                    BuildPipeline.BuildPlayer(levels, IphoneAPKName, _buildTarget, options);
                    break;
                case BuildTarget.Android:
                    if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                    {
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                    }
                    EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;
                    BuildPipeline.BuildPlayer(levels, AndroidAPKName, _buildTarget, options);
                    break;
                default:
                    if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                    {
                        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;
                        BuildPipeline.BuildPlayer(levels, AndroidAPKName, EditorUserBuildSettings.activeBuildTarget, options);
                    }
                    else
                    {
                        BuildPipeline.BuildPlayer(levels, IphoneAPKName, EditorUserBuildSettings.activeBuildTarget, options);
                    }
                    break;
            }
        }
    }
}
