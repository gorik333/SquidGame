# Changelog

## 5.3.0.1
* Check whether fullscreen ad has expired already before showing.

## 5.3.0.0
* Certified with Facebook Audience Network SDK 5.3.0 as it contains bidding improvements.

## 5.2.1.2
* Dynamically reference against Facebook SDK version number.

## 5.2.1.1
* In the Facebook adapters Unity Plugin, moved the dependencies bundled in 5.2.1.0 into `Assets/MaxSdk/Plugins/Android/Shared Dependencies`.

**Please delete the following from the `Assets/MaxSdk/Plugins/Android/Facebook` folder:**

    1. `exoplayer-core.aar`
    2. `exoplayer-dash.aar`
    3. `recyclerview-v7.aar`

## 5.2.1.0
* Certified with Facebook Audience Network SDK 5.2.1.
* Bundle in Unity Plugin the following Android dependencies:
    1. `exoplayer-core.aar`
    2. `exoplayer-dash.aar`
    3. `recyclerview-v7.aar`

## 5.2.0.1
* Set mediation provider as APPLOVIN_X.X.X:Y.Y.Y.Y where X.X.X is AppLovin's SDK version and Y.Y.Y.Y is the adapter version.

## 5.2.0.0
* Support for FAN SDK 5.2.0.
* Use new FAN SDK initialization APIs.

## 5.1.0.2
* Use unique package name in Android Manifest.

## 5.1.0.1
* Removed Redundant `activity` tags from AndroidManifest.

## 5.1.0.0
* Initial commit.
