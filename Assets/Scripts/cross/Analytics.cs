using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if USE_FLURRY
using FlurrySDK;
#endif


public class Analytics : MonoBehaviour
{
	public static bool _isInitialized= false;

	void Start()
	{
		Initialize();
	}

	public static bool IsEnabled()
	{
		return true;
	}

	public static bool IsInitialized()
	{
		return _isInitialized;
	}

	public static void Initialize()
	{
		if( IsEnabled() && !IsInitialized() )
		{
			_isInitialized= true;

#if USE_FLURRY

#if UNITY_IOS
			string apiKey= CoreConfig.FlurryIOS;
#else
			string apiKey= CoreConfig.FlurryAndroid;
#endif

		// Initialize Flurry once.
        new Flurry.Builder()
                  .WithCrashReporting(true)
                  .WithLogEnabled(true)
                  .WithLogLevel(Flurry.LogLevel.LogVERBOSE)
                  .Build( apiKey );

			Flurry.SetIAPReportingEnabled( true );

#endif
		}

		InitAppsflyer();

		InitTenjin();
	}

	public static void RevenueEvent( string product, string productId, float price, string currency, string transId )
	{
#if USE_FLURRY && UNITY_ANDROID
		Flurry.LogPayment( product, productId, 1, price, currency, transId, null );
#endif

#if USE_TENJIN
		TenjinPurchaseAndroid( productId, currency, 1, (double)price, null, null, null );
#endif
	}

	public static void SendEvent( string eventName, string param, object value )
	{
		SendEvent( eventName, new Dictionary<string,object>() { { param, value } } );
	}

	public static void SendEvent( string eventName, string param1, object value1, string param2, object value2 )
	{
		SendEvent( eventName, new Dictionary<string,object>() { { param1, value1 }, { param2, value2 } } );
	}

	public static void SendEvent( string eventName, string param1, object value1, string param2, object value2, string param3, object value3 )
	{
		SendEvent( eventName, new Dictionary<string,object>() { { param1, value1 }, { param2, value2 }, { param3, value3 } } );
	}

	public static void SendEvent( string eventName, string param1, object value1, string param2, object value2, string param3, object value3, string param4, object value4 )
	{
		SendEvent( eventName, new Dictionary<string,object>() { { param1, value1 }, { param2, value2 }, { param3, value3 }, { param4, value4 } } );
	}

	public static void SendEvent( string eventName, string param1, object value1, string param2, object value2, string param3, object value3, string param4, object value4, string param5, object value5 )
	{
		SendEvent( eventName, new Dictionary<string,object>() { { param1, value1 }, { param2, value2 }, { param3, value3 }, { param4, value4 }, { param5, value5 } } );
	}

	public static void SendEvent( string eventName, string param1, object value1, string param2, object value2, string param3, object value3, string param4, object value4, string param5, object value5, string param6, object value6 )
	{
		SendEvent( eventName, new Dictionary<string,object>() { { param1, value1 }, { param2, value2 }, { param3, value3 }, { param4, value4 }, { param5, value5 }, { param6, value6 } } );
	}

	public static void SendEvent( string eventName )
	{
		SendEvent( eventName, null );
	}


	public static void SendEvent( string eventName, Dictionary<string, object> parameters )
	{
		if( IsEnabled() == false )
			return;

		//if( parameters != null )
		//	parameters.Add( "day", Stats.Instance.DaysFromStart() );

		/*
		if (Debug.isDebugBuild)
		{
			string text = "";

			if( parameters != null )
			{
				foreach ( var item in parameters )
				{
					text += "\n" + item.Key + " " + item.Value;
				}
			}
			
			print("event: " + eventName + text);
		}*/

#if USE_FLURRY
		if( parameters == null )
			Flurry.LogEvent( eventName );
		else
			Flurry.LogEvent( eventName, ToStrStr( parameters ) );
#endif

#if UNITY_ANALYTICS
		if( parameters == null )
			UnityEngine.Analytics.Analytics.CustomEvent( eventName );
		else
			UnityEngine.Analytics.Analytics.CustomEvent( eventName, parameters );
#endif

#if USE_FB
//		if( parameters == null )
//			FBMan.SendEvent( eventName );
//		else
//			FBMan.SendEvent( eventName, parameters );
#endif

#if USE_APPSFLYER
/*
		if( parameters == null )
			AppsFlyer.trackRichEvent( eventName, null );
		else
		{
			Dictionary<string,string> dicStr= new Dictionary<string,string>();

			foreach(KeyValuePair<string,object> pair in parameters)
				dicStr.Add( pair.Key, pair.Value.ToString() );

			AppsFlyer.trackRichEvent( eventName, dicStr );
		}
*/
#endif

	}

	// AF

	static void InitAppsflyer()
	{
#if USE_APPSFLYER
		AppsFlyer.setAppsFlyerKey( "r9vNC83N8nYpCzYGigyjUh" );

		//For detailed logging
		//AppsFlyer.setIsDebug( true );

#if UNITY_IOS
		/* Mandatory - set your apple app ID
      	NOTE: You should enter the number only and not the "ID" prefix */
		AppsFlyer.setAppID( CoreConfig.AppID_IOS );
		AppsFlyer.trackAppLaunch();

#elif UNITY_ANDROID
		/* Mandatory - set your Android package name */
		AppsFlyer.setAppID( CoreConfig.BundleAndroid );

		/* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
		AppsFlyer.init( "r9vNC83N8nYpCzYGigyjUh", "AppsFlyerTrackerCallbacks" );
#endif

#endif
	}


	static void InitTenjin()
	{
#if USE_TENJIN
		BaseTenjin instance = Tenjin.getInstance( CoreConfig.TenjinKEY );

		instance.OptIn();

		instance.Connect();
#endif
	}

#if USE_TENJIN
	static void TenjinPurchaseAndroid(string ProductId, string CurrencyCode, int Quantity, double UnitPrice, string TransactionId, string Receipt, string Signature)
	{
		BaseTenjin instance = Tenjin.getInstance( CoreConfig.TenjinKEY );
		instance.Transaction( ProductId, CurrencyCode, Quantity, UnitPrice, TransactionId, Receipt, Signature);
	}

	static void TenjinPurchaseIOS(string ProductId, string CurrencyCode, int Quantity, double UnitPrice, string TransactionId, string Receipt)
	{
		BaseTenjin instance = Tenjin.getInstance( CoreConfig.TenjinKEY );
		instance.Transaction(ProductId, CurrencyCode, Quantity, UnitPrice, TransactionId, Receipt, null);
	}
#endif

	static public void onAppPause( bool pauseStatus )
	{
		if( pauseStatus == false )
		{
			InitTenjin();
		}
	}


	static Dictionary<string,string> ToStrStr( Dictionary<string,object> parameters )
	{
		Dictionary<string,string> dicStr= new Dictionary<string,string>();

		foreach(KeyValuePair<string,object> pair in parameters)
			dicStr.Add( pair.Key, pair.Value.ToString() );

		return dicStr;
	}

}
