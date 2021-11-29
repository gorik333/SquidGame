//
//  MAUnityPlugin.mm
//  AppLovin MAX Unity Plugin
//

#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wdeprecated-declarations"

#import "MAUnityAdManager.h"

#define NSSTRING(_X) ( (_X != NULL) ? [NSString stringWithCString: _X encoding: NSStringEncodingConversionAllowLossy] : nil)

UIView* UnityGetGLView();

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C"
{
    static ALSdk *_sdk;
    static MAUnityAdManager *_adManager;
    static bool _isPluginInitialized = false;
    static bool _isSdkInitialized = false;
    static ALSdkConfiguration *_sdkConfiguration;
    
    // Store the user id to be set if pub attempts to set it before calling _MaxInitializeSdk()
    static NSString *_userIdentifierToSet;
    
    // Helper method to create C string copy
    static const char * cStringCopy(NSString *string);
    
    bool isPluginInitialized()
    {
        return _isPluginInitialized;
    }
    
    void maybeInitializePlugin()
    {
        if ( isPluginInitialized() ) return;
        
        _adManager = [[MAUnityAdManager alloc] init];
        _isPluginInitialized = true;
    }
    
    void _MaxSetSdkKey(const char *sdkKey)
    {
        maybeInitializePlugin();
        
        if (!sdkKey) return;
        
        NSString *sdkKeyStr = [NSString stringWithUTF8String: sdkKey];
        
        NSDictionary *infoDict = [[NSBundle mainBundle] infoDictionary];
        [infoDict setValue: sdkKeyStr forKey: @"AppLovinSdkKey"];
    }
    
    void _MaxInitializeSdk()
    {
        maybeInitializePlugin();
        
        _sdk = [_adManager initializeSdkWithCompletionHandler:^(ALSdkConfiguration *configuration) {
            _sdkConfiguration = configuration;
            _isSdkInitialized = true;
        }];
        
        // Set user id if needed
        if ( _userIdentifierToSet )
        {
            _sdk.userIdentifier = _userIdentifierToSet;
            _userIdentifierToSet = nil;
        }
    }
    
    bool _MaxIsInitialized()
    {
        return _isPluginInitialized && _isSdkInitialized;
    }
    
    void _MaxShowMediationDebugger()
    {
        [_sdk showMediationDebugger];
    }
    
    int _MaxConsentDialogState()
    {
        if (!isPluginInitialized()) return ALConsentDialogStateUnknown;
        
        return _sdkConfiguration.consentDialogState;
    }
    
    void _MaxSetUserId(const char *userId)
    {
        if (!isPluginInitialized()) return;
        
        if ( _sdk )
        {
            _sdk.userIdentifier = NSSTRING(userId);
            _userIdentifierToSet = nil;
        }
        else
        {
            _userIdentifierToSet = NSSTRING(userId);
        }
    }
    
    void _MaxSetHasUserConsent(bool hasUserConsent)
    {   
        [ALPrivacySettings setHasUserConsent: hasUserConsent];
    }
    
    bool _MaxHasUserConsent()
    {
        return [ALPrivacySettings hasUserConsent];
    }
    
    void _MaxSetIsAgeRestrictedUser(bool isAgeRestrictedUser)
    {
        [ALPrivacySettings setIsAgeRestrictedUser: isAgeRestrictedUser];
    }
    
    bool _MaxIsAgeRestrictedUser()
    {
        return [ALPrivacySettings isAgeRestrictedUser];
    }
    
    void _MaxCreateBanner(const char *adUnitIdentifier, const char *bannerPosition)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager createBannerWithAdUnitIdentifier: NSSTRING(adUnitIdentifier) atPosition: NSSTRING(bannerPosition)];
    }
    
    void _MaxSetBannerPlacement(const char *adUnitIdentifier, const char *placement)
    {
        [_adManager setBannerPlacement: NSSTRING(placement) forAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    void _MaxShowBanner(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager showBannerWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    void _MaxDestroyBanner(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager destroyBannerWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    void _MaxHideBanner(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager hideBannerWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    void _MaxLoadInterstitial(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager loadInterstitialWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    bool _MaxIsInterstitialReady(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return false;
        
        return [_adManager isInterstitialReadyWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    void _MaxShowInterstitial(const char *adUnitIdentifier, const char *placement)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager showInterstitialWithAdUnitIdentifier: NSSTRING(adUnitIdentifier) placement: NSSTRING(placement)];
    }
    
    void _MaxLoadRewardedAd(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager loadRewardedAdWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    bool _MaxIsRewardedAdReady(const char *adUnitIdentifier)
    {
        if (!isPluginInitialized()) return false;
        
        return [_adManager isRewardedAdReadyWithAdUnitIdentifier: NSSTRING(adUnitIdentifier)];
    }
    
    void _MaxShowRewardedAd(const char *adUnitIdentifier, const char *placement)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager showRewardedAdWithAdUnitIdentifier: NSSTRING(adUnitIdentifier) placement: NSSTRING(placement)];
    }
    
    void _MaxTrackEvent(const char *event, const char *parameters)
    {
        if (!isPluginInitialized()) return;
        
        [_adManager trackEvent: NSSTRING(event) parameters: NSSTRING(parameters)];
    }
    
    void _MaxLoadVariables()
    {
        if (!isPluginInitialized()) return;
        
        [_adManager loadVariables];
    }
    
    bool _MaxGetBool(const char *key, bool defaultValue)
    {
        if (!isPluginInitialized()) return defaultValue;
        
        return [_sdk.variableService boolForKey: NSSTRING(key) defaultValue: defaultValue];
    }
    
    const char * _MaxGetString(const char *key, const char *defaultValue)
    {
        if (!isPluginInitialized()) return defaultValue;
        
        return cStringCopy([_sdk.variableService stringForKey: NSSTRING(key) defaultValue: NSSTRING(defaultValue)]);
    }
    
    static const char * cStringCopy(NSString *string)
    {
        const char *value = string.UTF8String;
        return value ? strdup(value) : NULL;
    }
}

#pragma clang diagnostic pop
