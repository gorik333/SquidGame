using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FigureHandler : MonoBehaviour
{
	private const float MOVE_TIME = 0.3f;

	public void MoveToPlayer( Transform followTarget )
	{
		//transform.DOMove( transform.position, MOVE_TIME );

		StartCoroutine( FollowTarget( followTarget ) );
	}


	private IEnumerator FollowTarget( Transform followTarget )
	{
		//yield return new WaitForSeconds( MOVE_TIME );

		while (true)
		{
			if (followTarget == null)
				break;

			transform.position = followTarget.position;

			yield return new WaitForEndOfFrame();
		}
	}
}
