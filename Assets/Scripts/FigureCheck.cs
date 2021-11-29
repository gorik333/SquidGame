using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCheck : MonoBehaviour
{
	private const float KILL_INTERVAL = 0.2f;

	[SerializeField]
	private Enemy[] _enemy;


	private void OnTriggerEnter( Collider other )
	{
		var playerHub = other.GetComponentInParent<PlayerHub>();

		if (playerHub != null && playerHub.HubSize <= 2)
		{
			StopAllCoroutines();
			StartCoroutine( IntervalKill( playerHub ) );
		}
		else if (playerHub != null && playerHub.HubSize > 2)
			Win();
	}


	private IEnumerator IntervalKill( PlayerHub playerHub )
	{
		var players = playerHub.Hub;

		for (int i = 0; i < players.Count; i++)
		{
			_enemy[ i % _enemy.Length ].Attack( players[ i ] );
			playerHub.KillPlayer( players[ i ] );

			yield return new WaitForSeconds(KILL_INTERVAL);
		}
	}


	private void Win()
	{
		Game.Instance.onFinish();
	}
}
