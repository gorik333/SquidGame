using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoreConfig
{

	//CORE

	static public string BundleIOS= "join.crowd.fight";

	static public string BundleAndroid= "join.crowd.fight";

	static public string AppID_IOS= "";


	//FLURRY

	static public string FlurryIOS= "";

	static public string FlurryAndroid= "H8VTNB5FHHTWFNFWKXFS";

	//TENJIN

	static public string TenjinKEY= "";





	//FUNCS

	public static string GetMyURL()
	{
#if UNITY_ANDROID
		return "https://play.google.com/store/apps/details?id=" + BundleAndroid;
#else
		return "https://itunes.apple.com/id" + AppID_IOS;
#endif
	}

}
