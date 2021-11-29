/**
 * AppLovin MAX Unity Plugin C# Wrapper
 */

using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class MaxSdk :
#if UNITY_EDITOR
    MaxSdkUnityEditor
#elif UNITY_ANDROID
    MaxSdkAndroid
#else
    MaxSdkiOS
#endif
{
    
}
