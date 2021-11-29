using UnityEngine;

public abstract class MaxSdkBase
{
    /// <summary>
    /// This enum represents whether or not the consent dialog should be shown for this user.
    /// The state where no such determination could be made is represented by {@link ConsentDialogState#Unknown}.
    /// </summary>
    public enum ConsentDialogState
    {
        /// <summary>
        /// The consent dialog state could not be determined. This is likely due to SDK failing to initialize.
        /// </summary>
        Unknown,
        
        /// <summary>
        /// This user should be shown a consent dialog.
        /// </summary>
        Applies,
        
        /// <summary>
        /// This user should not be shown a consent dialog.
        /// </summary>
        DoesNotApply
    }
    
    public enum BannerPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        Centered,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class SdkConfiguration
    {
        public ConsentDialogState ConsentDialogState;
    }

    public struct Reward
    {
        public string Label;
        public int Amount;


        public override string ToString()
        {
            return "Reward: " + Amount + " " + Label;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Label) && Amount > 0;
        }
    }
    
    protected static void ValidateAdUnitIdentifier(string adUnitIdentifier, string debugPurpose)
    {
        if (string.IsNullOrEmpty(adUnitIdentifier))
        {
            Debug.LogError("[AppLovin MAX] No MAX Ads Ad Unit ID specified for: " + debugPurpose);
        }
    }

    // Allocate the MaxSdkCallbacks singleton, which receives all callback events from the native SDKs.
    protected static void InitCallbacks()
    {
        var type = typeof(MaxSdkCallbacks);
        var mgr = new GameObject("MaxSdkCallbacks", type)
            .GetComponent<MaxSdkCallbacks>(); // Its Awake() method sets Instance.
        if (MaxSdkCallbacks.Instance != mgr)
        {
            Debug.LogWarning("[AppLovin MAX] It looks like you have the " + type.Name + " on a GameObject in your scene. Please remove the script from your scene.");
        }
    }
}