using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	private GameObject _laserSimulation;

	private List<Player> _touchedPlayer = new List<Player>();

	private PlayerHub _playerHub;

	private float _killInterval;


	public void TurnOn( float killInterval = 0.02f )
	{
		_laserSimulation.SetActive( true );

		_killInterval = killInterval;

		StartCoroutine( KillPlayers() );
	}


	public void TurnOff()
	{
		_laserSimulation.SetActive( false );

		_touchedPlayer.Clear();
		_playerHub = null;

		StopAllCoroutines();
	}


	private IEnumerator KillPlayers()
	{
		while (true)
		{
			for (int i = 0; i < _touchedPlayer.Count; i++)
			{
				if (_playerHub != null && _touchedPlayer[ i ] != null)
				{
					_playerHub.KillPlayer( _touchedPlayer[ i ] );


					yield return new WaitForSeconds( _killInterval );
				}
				else

					yield return new WaitForEndOfFrame();
			}

			yield return new WaitForEndOfFrame();
		}
	}


	private void OnTriggerEnter( Collider other )
	{
		var player = other.GetComponent<Player>();
		var playerHub = other.GetComponentInParent<PlayerHub>();

		if (player != null && playerHub != null)
		{
			_playerHub = playerHub;
			if (!_touchedPlayer.Contains( player ))
				_touchedPlayer.Add( player );
		}
	}


	private void OnTriggerExit( Collider other )
	{
		var player = other.GetComponent<Player>();

		if (_touchedPlayer.Contains( player ))
			_touchedPlayer.Remove( player );

		_playerHub = null;
	}
}
