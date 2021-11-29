using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// iOS AppLovin MAX Unity Plugin implementation
/// </summary>
public class MaxSdkiOS : MaxSdkBase
{
    static MaxSdkiOS()
    {
        InitCallbacks();
    }

    #if UNITY_IOS
    
    public static MaxVariableServiceiOS VariableService
    {
        get { return MaxVariableServiceiOS.Instance; }
    }
        
    #region Initialization

    [DllImport("__Internal")]
    private static extern void _MaxSetSdkKey(string sdkKey);

    /// <summary>
    /// Set AppLovin SDK Key.
    ///
    /// This method must be called before any other SDK operation
    /// </summary>
    public static void SetSdkKey(string sdkKey)
    {
        _MaxSetSdkKey(sdkKey);   
    }

    [DllImport("__Internal")]
    private static extern void _MaxInitializeSdk();

    /// <summary>
    /// Initialize the default instance of AppLovin SDK.
    ///
    /// Please make sure that application's Android manifest or Info.plist should include provided
    /// AppLovin SDK key
    /// </summary>
    public static void InitializeSdk()
    {
        _MaxInitializeSdk();
    }

    [DllImport("__Internal")]
    private static extern bool _MaxIsInitialized();

    /// <summary>
    /// Check if the SDK has been initialized
    /// </summary>
    /// <returns>True if SDK has been initialized</returns>
    public static bool IsInitialized()
    {
        return _MaxIsInitialized();
    }

    #endregion

    #region User Identifier
    
    [DllImport("__Internal")]
    private static extern void _MaxSetUserId(string userId);

    /// <summary>
    /// Set an identifier for the current user. This identifier will be tied to SDK events and our optional S2S postbacks.
    /// 
    /// If you're using reward validation, you can optionally set an identifier to be included with currency validation postbacks.
    /// For example, a username or email. We'll include this in the postback when we ping your currency endpoint from our server.
    /// </summary>
    /// 
    /// <param name="userId">The user identifier to be set.</param>
    public static void SetUserId(string userId)
    {
        _MaxSetUserId(userId);
    }
    
    #endregion

    #region Mediation Debugger

    [DllImport("__Internal")]
    private static extern void _MaxShowMediationDebugger();

    /// <summary>
    /// Present the mediation debugger UI.
    /// This debugger tool provides the status of your integration for each third-party ad network.
    ///
    /// Please call this method after the SDK has initialized.
    /// </summary>
    public static void ShowMediationDebugger()
    {
        _MaxShowMediationDebugger();
    }

    #endregion
    
    #region Privacy

    [DllImport("__Internal")]
    private static extern int _MaxConsentDialogState();

    /// <summary>
    /// Get the consent dialog state for this user. If no such determination could be made, {@link ConsentDialogState#Unknown} will be returned.
    ///
    /// Note: this method should be called only after SDK has been initialized
    /// </summary>
    public static ConsentDialogState GetConsentDialogState()
    {
        if (!IsInitialized())
        {
            Debug.LogWarning("[AppLovin MAX] MAX Ads SDK has not been initialized yet. GetConsentDialogState() may return ConsentDialogState.Unknown");
        }

        return (ConsentDialogState) _MaxConsentDialogState();
    }

    [DllImport("__Internal")]
    private static extern void _MaxSetHasUserConsent(bool hasUserConsent);

    /// <summary>
    /// Set whether or not user has provided consent for information sharing with AppLovin and other providers.
    /// </summary>
    /// <param name="hasUserConsent">'true' if the user has provided consent for information sharing with AppLovin. 'false' by default.</param>
    public static void SetHasUserConsent(bool hasUserConsent)
    {
        _MaxSetHasUserConsent(hasUserConsent);
    }

    [DllImport("__Internal")]
    private static extern bool _MaxHasUserConsent();

    /// <summary>
    /// Check if user has provided consent for information sharing with AppLovin and other providers.
    /// </summary>
    /// <returns></returns>
    public static bool HasUserConsent()
    {
        return _MaxHasUserConsent();
    }

    [DllImport("__Internal")]
    private static extern void _MaxSetIsAgeRestrictedUser(bool isAgeRestrictedUser);

    /// <summary>
    /// Mark user as age restricted (i.e. under 16).
    /// </summary>
    /// <param name="isAgeRestrictedUser">'true' if the user is age restricted (i.e. under 16).</param>
    public static void SetIsAgeRestrictedUser(bool isAgeRestrictedUser)
    {
        _MaxSetIsAgeRestrictedUser(isAgeRestrictedUser);
    }

    [DllImport("__Internal")]
    private static extern bool _MaxIsAgeRestrictedUser();

    /// <summary>
    /// Check if user is age restricted.
    /// </summary>
    /// <returns></returns>
    public static bool IsAgeRestrictedUser()
    {
        return _MaxIsAgeRestrictedUser();
    }

    #endregion

    #region Banners
    
    [DllImport("__Internal")]
    private static extern void _MaxCreateBanner(string adUnitIdentifier, string bannerPosition);

    /// <summary>
    /// Create a new banner
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to create</param>
    /// <param name="bannerPosition">Banner position</param>
    public static void CreateBanner(string adUnitIdentifier, BannerPosition bannerPosition)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "create banner");
        _MaxCreateBanner(adUnitIdentifier, bannerPosition.ToString());
    }

    [DllImport("__Internal")]
    private static extern void _MaxSetBannerPlacement(string adUnitIdentifier, string placement);
    
    /// <summary>
    /// Set the banner placement for an ad unit identifier to tie the future ad events to.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to set the placement for</param>
    /// <param name="placement">Placement to set</param>
    public static void SetBannerPlacement(string adUnitIdentifier, string placement)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "set banner placement");
        _MaxSetBannerPlacement(adUnitIdentifier, placement);
    }
    
    [DllImport("__Internal")]
    private static extern void _MaxShowBanner(string adUnitIdentifier);

    /// <summary>
    /// Show banner at a position determined by the 'CreateBanner' call.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to show</param>
    public static void ShowBanner(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "show banner");
        _MaxShowBanner(adUnitIdentifier);
    }

    [DllImport("__Internal")]
    private static extern void _MaxDestroyBanner(string adUnitIdentifier);

    /// <summary>
    /// Remove banner from the ad view and destroy it
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to destroy</param>
    public static void DestroyBanner(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "destroy banner");
        _MaxDestroyBanner(adUnitIdentifier);
    }

    [DllImport("__Internal")]
    private static extern void _MaxHideBanner(string adUnitIdentifier);

    /// <summary>
    /// Hide banner
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to hide</param>
    /// <returns></returns>
    public static void HideBanner(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "hide banner");
        _MaxHideBanner(adUnitIdentifier);
    }

    #endregion

    #region Interstitials
    
    [DllImport("__Internal")]
    private static extern void _MaxLoadInterstitial(string adUnitIdentifier);

    /// <summary>
    /// Start loading an interstitial
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    public static void LoadInterstitial(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "load interstitial");
        _MaxLoadInterstitial(adUnitIdentifier);
    }

    [DllImport("__Internal")]
    private static extern bool _MaxIsInterstitialReady(string adUnitIdentifier);

    /// <summary>
    /// Check if interstitial ad is loaded and ready to be displayed
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    /// <returns>True if the ad is ready to be displayed</returns>
    public static bool IsInterstitialReady(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "check interstitial loaded");
        return _MaxIsInterstitialReady(adUnitIdentifier);
    }

    [DllImport("__Internal")]
    private static extern void _MaxShowInterstitial(string adUnitIdentifier, string placement);

    /// <summary>
    /// Present loaded interstitial. Note: if the interstitial is not ready to be displayed nothing will happen.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    public static void ShowInterstitial(string adUnitIdentifier)
    {
        ShowInterstitial(adUnitIdentifier, null);
    }
    
    /// <summary>
    /// Present loaded interstitial for a given placement to tie ad events to. Note: if the interstitial is not ready to be displayed nothing will happen.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    /// <param name="placement">The placement to tie the showing ad's events to</param>
    public static void ShowInterstitial(string adUnitIdentifier, string placement)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "show interstitial");

        if (IsInterstitialReady(adUnitIdentifier))
        {
            _MaxShowInterstitial(adUnitIdentifier, placement);
        }
        else
        {
            Debug.LogWarning("[AppLovin MAX] Not showing MAX Ads interstitial: ad not ready");
        }
    }

    #endregion

    #region Rewarded

    [DllImport("__Internal")]
    private static extern void _MaxLoadRewardedAd(string adUnitIdentifier);

    /// <summary>
    /// Start loading an rewarded ad
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the rewarded ad to load</param>
    public static void LoadRewardedAd(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "load rewarded ad");
        _MaxLoadRewardedAd(adUnitIdentifier);
    }

    [DllImport("__Internal")]
    private static extern bool _MaxIsRewardedAdReady(string adUnitIdentifier);

    /// <summary>
    /// Check if rewarded ad ad is loaded and ready to be displayed
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the rewarded ad to load</param>
    /// <returns>True if the ad is ready to be displayed</returns>
    public static bool IsRewardedAdReady(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "check rewarded ad loaded");
        return _MaxIsRewardedAdReady(adUnitIdentifier);
    }

    [DllImport("__Internal")]
    private static extern void _MaxShowRewardedAd(string adUnitIdentifier, string placement);


    /// <summary>
    /// Present loaded rewarded ad. Note: if the rewarded ad is not ready to be displayed nothing will happen.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the rewarded ad to show</param>
    public static void ShowRewardedAd(string adUnitIdentifier)
    {
        ShowRewardedAd(adUnitIdentifier, null);
    }
    
    /// <summary>
    /// Present loaded rewarded ad for a given placement to tie ad events to. Note: if the rewarded ad is not ready to be displayed nothing will happen.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    /// <param name="placement">The placement to tie the showing ad's events to</param>
    public static void ShowRewardedAd(string adUnitIdentifier, string placement)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "show rewarded ad");

        if (IsRewardedAdReady(adUnitIdentifier))
        {
            _MaxShowRewardedAd(adUnitIdentifier, placement);
        }
        else
        {
            Debug.LogWarning("[AppLovin MAX] Not showing MAX Ads rewarded ad: ad not ready");
        }
    }

    #endregion

    #region Event Tracking
   
    [DllImport("__Internal")]
    private static extern void _MaxTrackEvent(string name, string parameters);

    /// <summary>
    /// Track an event using AppLovin.
    /// </summary>
    /// <param name="name">An event from the list of pre-defined events may be found in MaxEvents.cs as part of the AppLovin SDK framework.</param>
    /// <param name="parameters">A dictionary containing key-value pairs further describing this event.</param>
    public static void TrackEvent(string name, IDictionary<string, string> parameters=null)
    {
        _MaxTrackEvent(name, MaxSdkUtils.DictToPropsString(parameters));
    }

    #endregion   
    #endif
}