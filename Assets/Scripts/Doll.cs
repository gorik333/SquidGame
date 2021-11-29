using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Doll : MonoBehaviour
{
	private const float KILL_INTERVAL = 0.075f;
	private const float FADE_VALUE = 0.5f;

	[SerializeField]
	private Transform _dollModel;

	[SerializeField]
	private SpriteRenderer _killSector;

	[SerializeField]
	private EnterZone _enterZone;

	[SerializeField]
	private GameObject _enterZoneColl;

	[SerializeField]
	private Enemy[] _enemy;

	private List<Vector3> _joinPos = new List<Vector3>();
	private List<Player> _player = new List<Player>();

	private LevelConf _levelConf;

	private PlayerHub _playerHub;


	public void SetUp( LevelConf levelConf )
	{
		_levelConf = levelConf;

		StartRotating();
	}


	public void StartRotating()
	{
		StartCoroutine( DollRotating() );
	}


	public void JoinHub( PlayerHub playerHub )
	{
		if (playerHub != _playerHub || _playerHub == null)
			_playerHub = playerHub;
	}


	public void Join( Player player )
	{
		if (!_player.Contains( player ))
			_player.Add( player );

		if (!_joinPos.Contains( player.transform.position ))
			_joinPos.Add( player.transform.position );
	}


	public void Leave( Player player )
	{
		if (_player.Contains( player ))
		{
			int index = _player.FindIndex( e => e == player );

			_player.Remove( player );
			_joinPos.RemoveAt( index );
		}
	}


	private IEnumerator DollRotating()
	{
		float rotateTime = _levelConf.RotateTime;

		while (true)
		{
			StartCoroutine( CheckWhoMoved() );

			yield return new WaitForSeconds( _levelConf.RotateDelay );

			LookBack();

			Vector3 endValue = new Vector3( 0, 0, 0 );

			_dollModel.DORotate( endValue, rotateTime );
			_killSector.DOFade( 0, rotateTime );

			yield return new WaitForSeconds( _levelConf.LookDuration );

			_enterZoneColl.SetActive( true );

			endValue = new Vector3( 0, 180, 0 );

			_killSector.DOFade( FADE_VALUE, rotateTime );
			_dollModel.DORotate( endValue, rotateTime );
		}
	}


	private void LookBack()
	{
		StopCoroutine( CheckWhoMoved() );
		_enterZoneColl.SetActive( false );

		_joinPos.Clear();
		_player.Clear();
		_playerHub = null;
	}


	private IEnumerator CheckWhoMoved()
	{
		int enemyIndex = 0;

		while (true)
		{
			for (int i = 0; i < _player.Count; i++)
			{
				if (CheckIfMoved( i ))
					KillTheMoved( i, enemyIndex );

				enemyIndex++;

				if (enemyIndex >= _enemy.Length)
					enemyIndex = 0;

				yield return new WaitForSeconds( KILL_INTERVAL );
			}
			RemoveKilled();

			yield return new WaitForEndOfFrame();
		}
	}


	private void KillTheMoved( int i, int enemyIndex )
	{
		_enemy[ enemyIndex ].Attack( _player[ i ], KILL_INTERVAL );

		_playerHub.RemovePlayer( _player[ i ] );
		_player[ i ] = null;
		_joinPos[ i ] = Vector3.zero;
	}


	private bool CheckIfMoved( int i )
	{
		float distance = 0;

		if (_player[ i ] != null && _joinPos[ i ] != null)
			distance = Vector3.Distance( _player[ i ].transform.position, _joinPos[ i ] );

		if (distance >= 0.1f)
			return true;

		return false;
	}


	private void RemoveKilled()
	{
		for (int i = 0; i < _player.Count; i++)
		{
			if (_player[ i ] == null)
				_player.Remove( _player[ i ] );

			if (_joinPos[ i ] == Vector3.zero)
				_joinPos.Remove( _joinPos[ i ] );
		}
	}
}
