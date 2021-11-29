using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
	[SerializeField]
	private LevelGen m_levelGen;

	[SerializeField]
	private PlayerHub m_playerHub;

	[SerializeField]
	private LevelDataStorage m_levelDataStorage;

	[SerializeField]
	private Transform _camera;

	private bool _isMoving;

	static Game _instance;
	public static Game Instance { get { return _instance; } }

	public UIProc m_ui;

	Game() { _instance = this; }

	private void Awake()
	{

	}

	void Start()
	{
		Application.targetFrameRate = 60;

		ADs._AdShown += AdShown;
		ADs._AdHidden += AdHidden;

		Restart();
	}


	private void Update()
	{
		if (_isMoving)
			m_playerHub.Move();

		_camera.position = m_playerHub.transform.position;
	}


	public void Restart()
	{
		int currLevel = Stats.Instance.level;

		var levelConf = m_levelDataStorage.GetLevelConf( currLevel );

		m_levelGen.OnRestart( levelConf );
		m_playerHub.OnRestart();
	}

	public void Continue()
	{

	}


	private void StartGame()
	{
		m_playerHub.OnStart();
	}

	// EVENTS

	public void onClick( bool pressed )
	{
		if (pressed && m_ui.IsMainMenuShown)
		{
			StartGame();

			m_ui.ShowMainMenu( false );
		}

		_isMoving = pressed;

		if (_isMoving)
			m_playerHub.StartRunAnim();
		else
			m_playerHub.StopRunAnim();
	}


	public void onGameOver()
	{
		//Stats.Instance.scores= ;

		m_playerHub.OnGameOver();

		m_ui.ShowGameOver( true, Stats.Instance.scores );

		SoundsMan.Play( "4pok" );
	}

	public void onFinish()
	{
		//Stats.Instance.scores= ;
		//Stats.Instance.level= ;

		m_playerHub.OnFinish();

		m_ui.ShowWin( Stats.Instance.level );

		SoundsMan.Play( "4pok" );
	}

	//

	void AdShown( bool rewarded )
	{
		UIProc.Instance.DebugOut( "AD SHOWN: " + rewarded );

		Vibro.Mute( true );
		SoundsMan.MuteSFX( true );
	}

	void AdHidden( bool rewarded )
	{
		UIProc.Instance.DebugOut( "AD HIDDEN: " + rewarded );

		Vibro.Mute( Stats.Instance.muteVibro );
		SoundsMan.MuteSFX( Stats.Instance.muteSFX );
	}

}
