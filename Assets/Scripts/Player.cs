using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	[SerializeField]
	private Transform _figurePos;

	[SerializeField]
	private EntityAnimator _animator;

	[SerializeField]
	private GameObject _deathEffect;

	[SerializeField]
	private Rigidbody _playerRB;

	[SerializeField]
	private CapsuleCollider _collider;

	private bool _hasFigure;


	public void Kill()
	{
		SpawnDeathParticles();
		SoundsMan.Play( "4pok" );

		Destroy( gameObject );
	}


	public void StartFalling()
	{
		_playerRB.constraints = RigidbodyConstraints.None;
		_playerRB.drag = 0;

		transform.parent = null;
		_collider.isTrigger = true;

		_animator.StartAnim( EntitiesAnim.PlayerFalling );

		Destroy( gameObject, 5f );
	}


	private void SpawnDeathParticles()
	{
		GameObject particles = Instantiate( _deathEffect, transform.position, Quaternion.identity );

		Destroy( particles, 2f );
	}


	private void OnTriggerEnter( Collider other )
	{
		var figure = other.GetComponent<FigureHandler>();

		if (figure != null && !_hasFigure)
		{
			_hasFigure = true;

			figure.MoveToPlayer( _figurePos );
		}
	}


	public EntityAnimator Animator => _animator;
}
