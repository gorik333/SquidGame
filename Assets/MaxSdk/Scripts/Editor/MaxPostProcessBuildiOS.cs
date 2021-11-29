//
//  MaxPostProcessBuildiOS.cs
//  AppLovin MAX Unity Plugin
//
//  Created by Thomas So on 2/18/19.
//  Copyright © 2019 AppLovin. All rights reserved.
//

#if UNITY_IOS

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
#if UNITY_2017_1_OR_NEWER
using UnityEditor.iOS.Xcode.Extensions;
#endif

public static class MaxPostProcessBuildiOS
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            Debug.Log("[AppLovin MAX] Starting iOS post-process build script...");

            var projectPath = Path.Combine(buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");
            var project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));

            //
            // Add the -ObjC linker flag
            //
            var target = project.TargetGuidByName("Unity-iPhone");
            project.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");

            //
            // Add needed frameworks to Embedded Libraries
            //
            string moPubFrameworkFileGuid = project.FindFileGuidByProjectPath("Frameworks/MaxSdk/Plugins/iOS/MoPub/MoPubSDKFramework.framework");

            // The publisher might be using Unity 2018.3 which adds the frameworks directly into the `Frameworks/` directory.
            if (moPubFrameworkFileGuid == null) moPubFrameworkFileGuid = project.FindFileGuidByProjectPath("Frameworks/MoPubSDKFramework.framework");   
            
            if (moPubFrameworkFileGuid != null)
            {
#if UNITY_2017_1_OR_NEWER
                Debug.Log("[AppLovin MAX] Adding MoPubSDKFramework.framework to Embedded Binaries list in the Xcode project");
                project.AddFileToEmbedFrameworks(target, moPubFrameworkFileGuid);
#else
                Debug.Log("[AppLovin Max] Failed to add MoPubSDKFramework.framework to Embedded Binaries. Please add it manually in your Xcode project.");
#endif

                // Add `@executable_path/Frameworks` to Run Search Paths needed for embedded frameworks on older version of Unity.
                // NOTE: Unity automatically adds it for newer versions but we don't exactly know the version in which they started doing it, so we will be adding it to all versions.
                project.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
            }

            //
            // Write modified Xcode project back to original location
            //
            File.WriteAllText(projectPath, project.WriteToString());

            Debug.Log("[AppLovin MAX] Finished iOS post-process build script...");
        }

        // Rename files masked to prevent Unity compilation such as MoPub's mraid.js
        string[] noCompileFiles = Directory.GetFiles(buildPath, "*.no_compile", SearchOption.AllDirectories);
        foreach (var noCompileFile in noCompileFiles)
        {
            Debug.Log("[AppLovin MAX] Removing .no_compile mask from file: " + noCompileFile);
            File.Move(noCompileFile, noCompileFile.Replace(".no_compile", ""));
        }
    }
}

#endif
