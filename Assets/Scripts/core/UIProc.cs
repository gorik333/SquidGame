using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIProc : MonoBehaviour
{

	static UIProc _instance;

	public Text m_txtDebug;

	public GameUI m_gameUI;

	public Game m_game;
    
	public WndMainMenu m_mainMenu;

	public WndGameOver m_wndGameOver;

	public wndGameWin m_wndGameWin;

	public WndRateUS m_wndRateUS;

	public wndMessage m_msg;

	public BannerCover m_bannerCover;

	int m_txtDebugLines= 0;



	public static UIProc Instance { get { return _instance; } }


	UIProc()
	{
		_instance= this;
	}

	void Awake()
	{
#if UNITY_EDITOR
		//PlayerPrefs.DeleteAll();
		//Application.logMessageReceived+= HandleLog;
#endif
	}

    void Start()
    {
		bool firstLaunch= Stats.Instance.HasParam("init_ads_delay") == false;

		DebugOut( "FIRST launch: " + firstLaunch );

		DebugOut( "DAY: " + Stats.Instance.DaysFromStart() );

		DebugOut( "Anti CHEAT: " + TimeAntiCheat.Instance.GetDebugStr() );

		//YaMetrica.Init( firstLaunch );
		Analytics.Initialize();

		FBMan.Instance.InitFB();

		Invoke( "InitADS", Stats.Instance.GetParamFloat("init_ads_delay",1f) );
		Stats.Instance.SetParam( "init_ads_delay", 0.1f );

		ShowMainMenu( true );

		InvokeRepeating( "WriteFPS", 1f, 0.5f );
    }


	void InitADS()
	{
		bool showBanner= Stats.Instance.level >= Stats.Instance.GetRemoteInt( "ad_banner_show_level", 10 );

		ADs.Init( Stats.Instance.adsRemoved ? ADType.Rewarded : ADType.All, showBanner );

		UIProc.Instance.DebugOut( "INIT ADS - Show BANNER = " + showBanner );
	}

	void HandleLog(string logString, string stackTrace, LogType type)
	{
		string str= "log " + type.ToString()[0];
		if( type == LogType.Exception )
			str+= "x";
		DebugOut( str + " " + logString );
	}

	public void ShowMainMenu( bool show )
	{
		if( !show && !m_mainMenu.IsVisible )
			return;

		m_mainMenu.Show( show );
	}

	public void ShowGameOver( bool canContinue, int scores )
	{
		m_wndGameOver.Show( canContinue, scores, 0f );
	}

	public void ShowWin( int level )
	{
		m_wndGameWin.Show( level );
	}

	public void ShowMessage( string text )
	{
		m_msg.Show( text );
	}

	public bool ShowRateUS( bool force= false )
	{
		if( m_wndRateUS.CanShow(force) )
		{
			m_wndRateUS.Show(true);
			return true;
		}
		return false;
	}

	public void onClickContinue()
    {
		Analytics.SendEvent( "CLICK_CONTINUE", "day", Stats.Instance.DaysFromStart(), "level", Stats.Instance.level );

		YaMetrica.SendEvent( "video_ads_watch", "ad_type", "rewarded", "placement", "continue" );

		bool showRew=
		ADs.ShowRewardedVideo( (int obj) =>
		{
			Analytics.SendEvent( "REWARDED_OK", "day", Stats.Instance.DaysFromStart() );

			m_game.Continue();

			m_wndGameOver.Hide();
		} );

		if( showRew )
			m_wndGameOver.DisableContinue();
		else
			Analytics.SendEvent( "NO_REWARDED", "day", Stats.Instance.DaysFromStart(), "level", Stats.Instance.level );
	}


	public void onClickPlayMain()
    {
		ShowMainMenu( false );
	}

	public void onClickNext()
    {
		Stats.Instance.level++;

		m_wndGameWin.Hide();

		m_game.Restart();

		m_gameUI.UpdateUI( Stats.Instance.scores );

		ShowMainMenu( true );

		ADs.ShowInterstitialAuto();
	}

	public void onClickRestart()
    {
		m_wndGameOver.Hide();

		ShowMainMenu( true );

		m_gameUI.UpdateUI( 0 );

		m_game.Restart();
	}

	public void onClickShop()
	{
		//m_wndShop.Show();
	}

	void OnApplicationFocus( bool hasFocus )
    {
        //DebugOut( "- FOCUS: " + hasFocus.ToString() );
    }

	private void WriteFPS()
	{
		float fps = 1.0f / Time.deltaTime;
		//DebugOut( "[FPS]: " + (int)fps );
	}

	public void DebugOut( string str )
	{
		if( m_txtDebug.gameObject.activeSelf == false )
			return;

		if( m_txtDebugLines > 34 )
		{
			m_txtDebugLines= 0;
			m_txtDebug.text= "";
		}

		m_txtDebug.text+= str + "\n";
		++m_txtDebugLines;
	}


	public bool IsMainMenuShown => m_mainMenu.isActiveAndEnabled;
}
