using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntitiesAnim
{
	Run,
	IdleEnemy,
	FireEnemy,
	PlayerFalling
}


public class EntityAnimator : MonoBehaviour
{
	#region Anim names

	private const string RUN_PLAYER_NAME = "RunPlayer";
	private const string IDLE_ENEMY_NAME = "IdleEnemy";
	private const string FIRE_ENEMY_NAME = "FireEnemy";
	private const string PLAYER_FALLING_NAME = "FallingPlayer";

	#endregion

	[SerializeField]
	private Animator _animator;


	public void StartAnim( EntitiesAnim type )
	{
		switch (type)
		{
			case EntitiesAnim.Run:
				_animator.SetBool( RUN_PLAYER_NAME, true );
				break;
			case EntitiesAnim.IdleEnemy:
				_animator.SetTrigger( IDLE_ENEMY_NAME );
				break;
			case EntitiesAnim.FireEnemy:
				_animator.SetBool( FIRE_ENEMY_NAME, true );
				break;
			case EntitiesAnim.PlayerFalling:
				_animator.SetTrigger( PLAYER_FALLING_NAME );
				break;
			default:
				Debug.Log( "NO ANIMATION" );
				break;
		}
	}


	public void StopAnim( EntitiesAnim type )
	{
		switch (type)
		{
			case EntitiesAnim.Run:
				_animator.SetBool( RUN_PLAYER_NAME, false );
				break;
			case EntitiesAnim.FireEnemy:
				_animator.SetBool( FIRE_ENEMY_NAME, false );
				break;
			default:
				Debug.Log( "NO ANIMATION" );
				break;
		}
	}
}
