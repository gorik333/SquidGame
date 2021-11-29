using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Enemy : MonoBehaviour
{
	#region Animation names

	private const string TAKE_RIFFLE_NAME = "TakeRiffle";
	private const string TAKE_RIFFLE_FORWARD_NAME = "TakeRiffleForward";

	#endregion

	[SerializeField]
	private Transform _model;

	[SerializeField]
	private Laser _laser;

	[SerializeField]
	private EntityAnimator _animator;

	[SerializeField]
	private Animator _riffleAnimator;


	private void Start()
	{
		_animator.StartAnim( EntitiesAnim.IdleEnemy );
	}


	public void TurnOnLaserAttackState()
	{
		_riffleAnimator.SetBool( TAKE_RIFFLE_FORWARD_NAME, true );

		StartCoroutine( TurnOnDelay() );
	}


	private IEnumerator TurnOnDelay()
	{
		yield return new WaitForEndOfFrame();

		yield return new WaitForSeconds( _riffleAnimator.GetCurrentAnimatorStateInfo( 0 ).length );

		_laser.TurnOn();
	}


	public void TurnOffLaserAttackState()
	{
		_riffleAnimator.SetBool( TAKE_RIFFLE_FORWARD_NAME, false );

		_laser.TurnOff();
	}


	public void Attack( Player player, float killInterval = 0.2f )
	{
		StopAllCoroutines();

		SoundsMan.Play( "4pok" );

		_model.DOLookAt( player.transform.position, killInterval / 2 );

		_animator.StartAnim( EntitiesAnim.FireEnemy );
		_riffleAnimator.SetBool( TAKE_RIFFLE_NAME, true );

		StartCoroutine( StopAttack( killInterval ) );

		player.Kill();
	}


	public IEnumerator StopAttack( float killInterval )
	{
		yield return new WaitForSeconds( killInterval + 0.1f );

		_riffleAnimator.SetBool( TAKE_RIFFLE_NAME, false );

		_animator.StopAnim( EntitiesAnim.FireEnemy );
	}
}
