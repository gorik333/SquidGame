using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity Editor AppLovin MAX Unity Plugin implementation
/// </summary>
public class MaxSdkUnityEditor : MaxSdkBase
{
    private static bool _isInitialized;
    private static bool _hasSdkKey;
    
    public static MaxVariableServiceUnityEditor VariableService
    {
        get { return MaxVariableServiceUnityEditor.Instance; }
    }
    
    static MaxSdkUnityEditor()
    {
        InitCallbacks();
    }

    /// <summary>
    /// Set AppLovin SDK Key.
    ///
    /// This method must be called before any other SDK operation
    /// </summary>
    public static void SetSdkKey(string sdkKey)
    {
        _hasSdkKey = true;
    }
    
    #region Initialization

    /// <summary>
    /// Initialize the default instance of AppLovin SDK.
    ///
    /// Please make sure that application's Android manifest or Info.plist should include provided
    /// AppLovin SDK key 
    /// </summary>
    public static void InitializeSdk()
    {
        _ensureHaveSdkKey();
            
        _isInitialized = true;
        _hasSdkKey = true;

        ExecuteWithDelay(() =>
        {
            _isInitialized = true;

#if UNITY_EDITOR
            MaxSdkCallbacks.EmitSdkInitializedEvent();
#endif
        });
    }

    /// <summary>
    /// Check if the SDK has been initialized
    /// </summary>
    /// <returns>True if SDK has been initialized</returns>
    public static bool IsInitialized()
    {
        return _isInitialized;
    }

    #endregion

    #region User Identifier
    
    /// <summary>
    /// Set an identifier for the current user. This identifier will be tied to SDK events and our optional S2S postbacks.
    /// 
    /// If you're using reward validation, you can optionally set an identifier to be included with currency validation postbacks.
    /// For example, a username or email. We'll include this in the postback when we ping your currency endpoint from our server.
    /// </summary>
    /// 
    /// <param name="userId">The user identifier to be set.</param>
    public static void SetUserId(string userId) {}
    
    #endregion

    #region Mediation Debugger

    /// <summary>
    /// Present the mediation debugger UI.
    /// This debugger tool provides the status of your integration for each third-party ad network.
    ///
    /// Please call this method after the SDK has initialized.
    /// </summary>
    public static void ShowMediationDebugger() {}

    #endregion
   
    #region Privacy

    /// <summary>
    /// Get the consent dialog state for this user. If no such determination could be made, {@link ConsentDialogState#Unknown} will be returned.
    ///
    /// Note: this method should be called only after SDK has been initialized
    /// </summary>
    public static ConsentDialogState GetConsentDialogState()
    {
        return ConsentDialogState.Unknown;
    }
    private static bool _hasUserConsent = true;

    /// <summary>
    /// Set whether or not user has provided consent for information sharing with AppLovin and other providers.
    /// </summary>
    /// <param name="hasUserConsent">'true' if the user has provided consent for information sharing with AppLovin. 'false' by default.</param>
    public static void SetHasUserConsent(bool hasUserConsent)
    {
        _hasUserConsent = hasUserConsent;
    }

    /// <summary>
    /// Check if user has provided consent for information sharing with AppLovin and other providers.
    /// </summary>
    /// <returns></returns>
    public static bool HasUserConsent()
    {
        return _hasUserConsent;
    }

    private static bool _isAgeRestrictedUser = false;

    /// <summary>
    /// Mark user as age restricted (i.e. under 16).
    /// </summary>
    /// <param name="isAgeRestrictedUser">'true' if the user is age restricted (i.e. under 16).</param>
    public static void SetIsAgeRestrictedUser(bool isAgeRestrictedUser)
    {
        _isAgeRestrictedUser = isAgeRestrictedUser;
    }

    /// <summary>
    /// Check if user is age restricted.
    /// </summary>
    /// <returns></returns>
    public static bool IsAgeRestrictedUser()
    {
        return _isAgeRestrictedUser;
    }

    #endregion

    #region Banners

    /// <summary>
    /// Create a new banner
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to create</param>
    /// <param name="bannerPosition">Banner position</param>
    public static void CreateBanner(string adUnitIdentifier, BannerPosition bannerPosition)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "create banner");
        RequestAdUnit(adUnitIdentifier);
    }

    /// <summary>
    /// Set the banner placement for an ad unit identifier to tie the future ad events to.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to set the placement for</param>
    /// <param name="placement">Placement to set</param>
    public static void SetBannerPlacement(string adUnitIdentifier, string placement)
    {
        Debug.Log("[AppLovin MAX] Setting banner placement to '" + placement + "' for ad unit id '" + adUnitIdentifier + "'");
    }
    
    /// <summary>
    /// Show banner at a position determined by the 'CreateBanner' call.
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to show</param>
    public static void ShowBanner(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "show banner");

        if (!IsAdUnitRequested(adUnitIdentifier))
        {
            Debug.LogWarning("[AppLovin MAX] Banner '" + adUnitIdentifier + "' was not created, can not show it");
        }
    }

    /// <summary>
    /// Remove banner from the ad view and destroy it
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to destroy</param>
    public static void DestroyBanner(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "destroy banner");
    }

    /// <summary>
    /// Hide banner
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the banner to hide</param>
    /// <returns></returns>
    public static void HideBanner(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "hide banner");
    }

    #endregion

    #region Interstitials

    /// <summary>
    /// Start loading an interstitial
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    public static void LoadInterstitial(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "load interstitial");
    }

    /// <summary>
    /// Check if interstitial ad is loaded and ready to be displayed
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the interstitial to load</param>
    /// <returns>True if the ad is ready to be displayed</returns>
    public static bool IsInterstitialReady(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "check interstitial loaded");
        if (!IsAdUnitRequested(adUnitIdentifier))
        {
            Debug.LogWarning("[AppLovin MAX] Interstitial '" + adUnitIdentifier + "' was not requested, can not check if it is loaded");
            return false;
        }

        return true;
    }

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

        if (!IsAdUnitRequested(adUnitIdentifier))
        {
            Debug.LogWarning("[AppLovin MAX] Interstitial '" + adUnitIdentifier + "' was not requested, can not show it");
            return;
        }
    }

    #endregion

    #region Rewarded

    /// <summary>
    /// Start loading an rewarded ad
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the rewarded ad to load</param>
    public static void LoadRewardedAd(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "load rewarded ad");
        RequestAdUnit(adUnitIdentifier);
    }

    /// <summary>
    /// Check if rewarded ad ad is loaded and ready to be displayed
    /// </summary>
    /// <param name="adUnitIdentifier">Ad unit identifier of the rewarded ad to load</param>
    /// <returns>True if the ad is ready to be displayed</returns>
    public static bool IsRewardedAdReady(string adUnitIdentifier)
    {
        ValidateAdUnitIdentifier(adUnitIdentifier, "check rewarded ad loaded");

        if (!IsAdUnitRequested(adUnitIdentifier))
        {
            Debug.LogWarning("[AppLovin MAX] Rewarded ad '" + adUnitIdentifier + "' was not requested, can not check if it is loaded");
            return false;
        }

        return true;
    }

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
        if (!IsAdUnitRequested(adUnitIdentifier))
        {
            Debug.LogWarning("[AppLovin MAX] Rewarded ad '" + adUnitIdentifier + "' was not requested, can not show it");
            return;
        }

        ValidateAdUnitIdentifier(adUnitIdentifier, "show rewarded ad");
    }

    #endregion
    
    #region Event Tracking

    /// <summary>
    /// Track an event using AppLovin.
    /// </summary>
    /// <param name="name">An event from the list of pre-defined events may be found in MaxEvents.cs as part of the AppLovin SDK framework.</param>
    /// <param name="parameters">A dictionary containing key-value pairs further describing this event.</param>
    public static void TrackEvent(string name, IDictionary<string, string> parameters=null) {}

    #endregion
    
    #region Internal

    private static readonly HashSet<string> _requestedAdUnits = new HashSet<string>();

    private static void RequestAdUnit(string adUnitId)
    {
        _ensureInitialized();
        _requestedAdUnits.Add(adUnitId);
    }

    private static bool IsAdUnitRequested(string adUnitId)
    {
        _ensureInitialized();
        return _requestedAdUnits.Contains(adUnitId);
    }

    private static void _ensureHaveSdkKey()
    {
        if (_hasSdkKey) return;
        Debug.LogWarning("[AppLovin MAX] MAX Ads SDK did not receive SDK key. Please call Max.SetSdkKey() to assign it");
    }
    
    private static void _ensureInitialized()
    {
        _ensureHaveSdkKey();
        
        if (_isInitialized) return;
        Debug.LogWarning("[AppLovin MAX] MAX Ads SDK is not initialized by the time ad is requested. Please call Max.InitializeSdk() in your first scene");
    }

    private static void ExecuteWithDelay(Action action)
    {
        MaxSdkCallbacks.Instance.StartCoroutine(ExecuteAction(action));
    }

    private static IEnumerator ExecuteAction(Action action)
    {
        yield return null;

        action();
    }

    #endregion
}