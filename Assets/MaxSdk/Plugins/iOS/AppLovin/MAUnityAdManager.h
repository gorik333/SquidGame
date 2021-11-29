//
//  MAUnityAdManager.h
//  AppLovin MAX Unity Plugin
//

#import <Foundation/Foundation.h>
#import <AppLovinSDK/AppLovinSDK.h>

NS_ASSUME_NONNULL_BEGIN

@interface MAUnityAdManager : NSObject

- (ALSdk *)initializeSdkWithCompletionHandler:(ALSdkInitializationCompletionHandler)completionHandler;

- (void)createBannerWithAdUnitIdentifier:(NSString *)adUnitIdentifier atPosition:(NSString *)bannerPosition;
- (void)setBannerPlacement:(nullable NSString *)placement forAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (void)showBannerWithAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (void)destroyBannerWithAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (void)hideBannerWithAdUnitIdentifier:(NSString *)adUnitIdentifier;

- (void)loadInterstitialWithAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (BOOL)isInterstitialReadyWithAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (void)showInterstitialWithAdUnitIdentifier:(NSString *)adUnitIdentifier placement:(NSString *)placement;

- (void)loadRewardedAdWithAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (BOOL)isRewardedAdReadyWithAdUnitIdentifier:(NSString *)adUnitIdentifier;
- (void)showRewardedAdWithAdUnitIdentifier:(NSString *)adUnitIdentifier placement:(NSString *)placement;

// Event Tracking
- (void)trackEvent:(NSString *)event parameters:(NSString *)parameters;

// Variable Service
- (void)loadVariables;

@end

NS_ASSUME_NONNULL_END
