using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class WndMainMenu : Window
{

	public GameObject m_settings;

	public GameObject m_btnNoAds;
	public GameObject m_btnDubl;

	public Toggle m_sfxMute;

	public Toggle m_vibroMute;



    void Start()
    {
		m_btnDubl.SetActive( Stats.Instance.adsRemoved );
        m_btnNoAds.SetActive( !Stats.Instance.adsRemoved );

		Vibro.Mute( Stats.Instance.muteVibro );

		SoundsMan.MuteSFX( Stats.Instance.muteSFX );


		InAppPurchase.Instance.AddItem( "removead", "com.game.removead", false, "1.99USD" );

		InAppPurchase.Instance.Init();

		InAppPurchase.OnPurchaseSuccessEvent+= onPurchaseSuccess;
		InAppPurchase.OnPurchaseFailedEvent+= onPurchaseFailed;
		InAppPurchase.OnRestorePurchaseSuccessEvent+= onPurchaseSuccess;
		InAppPurchase.OnRestorePurchaseFailedEvent+= onPurchaseRestoreFailed;

		InAppPurchase.OnPurchaseSuccessEventDetailed+= onPurchaseSuccessDetailed;
    }

	public override void onShowWnd()
	{
		m_settings.SetActive( false );

		m_sfxMute.isOn= Stats.Instance.muteSFX;

		m_vibroMute.isOn= Stats.Instance.muteVibro;
	}

	public void onClickShop()
	{
	}

	public void onClickMute()
	{
		Stats.Instance.muteSFX= m_sfxMute.isOn;

		SoundsMan.MuteSFX( Stats.Instance.muteSFX );
	}

	public void onClickVibro()
	{
		Stats.Instance.muteVibro= m_vibroMute.isOn;

		Vibro.Mute( Stats.Instance.muteVibro );
	}

	public void onClickSettings()
	{
		m_settings.SetActive( !m_settings.activeSelf );

		Analytics.SendEvent( "click_SETTINGS", "level", Stats.Instance.level, "day", Stats.Instance.DaysFromStart() );
	}

	public void onClickNoAds()
	{
		if( InAppPurchase.Instance.BuyProduct( "removead" ) == false )
			UIProc.Instance.ShowMessage( "PURCHASE FAILED!\nTRY AGAIN!" );

		Analytics.SendEvent( "click_NO_ADS", "level", Stats.Instance.level, "day", Stats.Instance.DaysFromStart() );
	}

	public void onClickRestore()
	{
		InAppPurchase.Instance.RestorePurchases();

		Analytics.SendEvent( "click_SETTINGS_RESTORE", "level", Stats.Instance.level, "day", Stats.Instance.DaysFromStart() );
	}

	public void onClickRateUS()
	{
		Analytics.SendEvent( "click_SETTINGS_RATE_US", "level", Stats.Instance.level, "day", Stats.Instance.DaysFromStart() );

		if( UIProc.Instance.ShowRateUS(true) == false )
			Application.OpenURL( GetMyURL() );
	}

	public static string GetMyURL()
	{
#if UNITY_ANDROID
		return "https://play.google.com/store/apps/details?id=";
#else
		return "https://itunes.apple.com/idxxxxxx";
#endif
	}


	///-------------- IN APP EVENTS ----------------

	void onPurchaseSuccessDetailed( string product, string productId, float price, string currency, string transId )
	{
		Analytics.RevenueEvent( product, productId, price, currency, transId );

		YaMetrica.SendEvent( "payment_succeed", "inapp_name", product, "inapp_id", productId, "currency", currency, "price", price );
	}

	void onPurchaseSuccess( string product )
	{
		Analytics.SendEvent( "PURCHASE_SUCCESS", "product", product, "level", Stats.Instance.level, "day", Stats.Instance.DaysFromStart() );

		if( product == "removead" )
		{
			m_btnDubl.SetActive( true );
			m_btnNoAds.SetActive( false );

			Stats.Instance.adsRemoved= true;
			ADs.HideBanner( false );

			UIProc.Instance.m_bannerCover.HideBanner();

			UIProc.Instance.ShowMessage( "PURCHASE SUCCESSED!" );

			if( product == "removead" )
				Analytics.SendEvent( "PURCHASE_SUCCESS_NOADS", "level", Stats.Instance.level, "day", Stats.Instance.DaysFromStart() );
		}
	}

	void onPurchaseFailed()
	{
		Analytics.SendEvent( "PURCHASE_FAILED", "day", Stats.Instance.DaysFromStart() );

		UIProc.Instance.ShowMessage( "PURCHASE FAILED! TRY AGAIN!" );
	}

	void onPurchaseRestoreFailed()
	{
		UIProc.Instance.ShowMessage( "RESTORE PURCHASE FAILED!" );
	}

}
