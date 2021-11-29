## Versions

  * Max Unity Plugin 1.4.0
    * Support for explicitly loading variables.
    * Support for passing in a Dictionary of String value and parameters for analytics event tracking.
  * Max Unity Plugin 1.3.2
    * Fix race condition of publisher setting privacy setting before plugin initializes via `MaxSdk.SetSdkKey(...)`;
  * Max Unity Plugin 1.3.1
    * Guard iOS PostProcessing script with `#if UNITY_2017_1_OR_NEWER`.
    * Add support for `@executable_path/Frameworks` in Run Search Paths for MoPub's Embedded Binaries.
  * Max Unity Plugin 1.3.0
    * Support for showing ad with placements to tie events to.
    * Add support for `*no_compile` files in post-processing script for MoPub's mraid.js.
    * Do not auto-refresh banners that have not yet been shown via `MaxSdk.ShowBanner(string adUnitIdentifier)`.
    * Add support for integrations that set SDK key programmatically and not in AndroidManifest. (Android only)
    * Fix `MaxVariableServiceiOS` compiling for L2CPP.
    * Wrap iOS PostProcessing script in `#if UNITY_IOS` ... `#endif` pre-processor macros.
    * Automatically add `MoPub.framework` to "Embedded Binaries" when exporting to Xcode. (iOS only)
    * Do not re-create VariableService(iOS|Android|UnityEditor) on every `MaxSdk.VariableService`.
  * Max Unity Plugin 1.2.0
    * Add support for setting user id.
  * Max Unity Plugin 1.1.2
    * Fix empty banners due to no Internet causing touch input issues. (iOS only)
  * Max Unity Plugin 1.1.1
    * Fix some 3rd-party ad networks (e.g. Amazon) not sizing correctly on first banner impression. (iOS only)
  * Max Unity Plugin 1.1.0
    * Added APIs for retrieving booleans and strings via variable service.
    * Guard iOS code around preprocessor so it does not get compiled on Android via IL2CPP.
  * Max Unity Plugin 1.0.1
    * Explicitly check for ad formats for each SDK callback.
    * Fix banner positioning on iOS 10. (iOS only)
    * Do not set initial AdView size if failure to load. (iOS only)
  * Max Unity Plugin 1.0.0
    * Initial commit.
