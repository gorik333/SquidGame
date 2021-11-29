using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleAnimator : MonoBehaviour
{
	#region Obstacle animation names

	private const string ON_GROUND_SAW_ROTATE = "OnGroundSawRotate";
	private const string IN_GROUND_SAW_ROTATE = "InGroundSawRotate";

	#endregion

	[SerializeField]
	private Animator _animator;


	public void StartAnim( ObstacleType type )
	{
		switch (type)
		{
			case ObstacleType.OnGroundSaw:
				_animator.SetTrigger( ON_GROUND_SAW_ROTATE );
				break;
			case ObstacleType.InGroundSaw:
				_animator.SetTrigger( IN_GROUND_SAW_ROTATE );
				break;
		}
	}
}
