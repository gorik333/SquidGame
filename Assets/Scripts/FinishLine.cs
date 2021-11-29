using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var player = other.GetComponent<Player>();
		var playerHub = other.GetComponentInParent<PlayerHub>();

		if (player != null)
			Game.Instance.onFinish();

		if (playerHub != null)
			playerHub.StopRunAnim();
	}
}
