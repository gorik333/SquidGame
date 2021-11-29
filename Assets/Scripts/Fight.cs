using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DollState
{
	LookFront,
	LookBack
}

public class Fight : MonoBehaviour
{
	private Doll _doll;

	private LevelConf _levelConf;

	private List<Enemy> _enemy = new List<Enemy>();
	private List<Player> _player = new List<Player>();

	public static Fight Instance { get; private set; }


	private void Awake()
	{
		Instance = this;
	}


	public void OnRestart()
	{
		_enemy.Clear();
		_player.Clear();

		_doll = null;
	}


	public void AddPlayer( Player player )
	{
		if (!_player.Contains( player ))
			_player.Add( player );
	}


	public void AddEnemy( Enemy enemy )
	{
		if (!_enemy.Contains( enemy ))
			_enemy.Add( enemy );
	}


	public void AddDoll( Doll doll )
	{
		_doll = doll;
	}


	public void StartFight( LevelConf levelConf )
	{
		_levelConf = levelConf;

		StartCoroutine( CheckIfMovedWhenLook() );
	}


	private IEnumerator CheckIfMovedWhenLook()
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
		}
	}
}
