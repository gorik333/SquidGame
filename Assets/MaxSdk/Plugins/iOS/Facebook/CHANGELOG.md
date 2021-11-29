# Changelog

## 5.2.0.3
* Fix using wrong AppLovin MAX callback when failing fullscreen ad display. Using `didFailToDisplayInterstitialAdWithError:` and `didFailToDisplayRewardedAdWithError:` now.

## 5.2.0.2
* Minor adapter improvements.

## 5.2.0.1
* Set mediation provider as APPLOVIN_X.X.X:Y.Y.Y.Y where X.X.X is AppLovin's SDK version and Y.Y.Y.Y is the adapter version.

## 5.2.0.0
* Support for FAN SDK 5.2.0.
* Use new FAN SDK initialization APIs.

## 5.1.1.1
* Synchronize usage of `FBAdSettings` as that is not thread-safe.

## 5.1.1.0
* Update to Facebook SDK 5.1.1 with fix for header bidding.

## 5.1.0.1
* Turn off Facebook SDK verbose logging.

## 5.1.0.0
* Initial commit.
