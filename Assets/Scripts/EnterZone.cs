using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZone : MonoBehaviour
{
	[SerializeField]
	private Doll _doll;


	private void OnTriggerEnter( Collider other )
	{
		var player = other.GetComponent<Player>();
		var playerHub = other.GetComponentInParent<PlayerHub>();

		if (player != null)
			_doll.Join( player );

		if (playerHub != null)
			_doll.JoinHub( playerHub );
	}


	private void OnTriggerExit( Collider other )
	{
		var player = other.GetComponent<Player>();

		if (player != null)
			_doll.Leave( player );
	}
}
