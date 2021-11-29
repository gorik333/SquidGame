using UnityEngine;
using System.Collections;

public enum ObstacleType
{
	InGroundSaw,
	OnGroundSaw
}

public class Obstacle : MonoBehaviour
{
	[SerializeField]
	private ObstacleAnimator _animator;

	[SerializeField]
	private Transform _model;

	[SerializeField]
	private ObstacleType _obstacleType;

	[ SerializeField ]
	private float _moveSpeed;

	[SerializeField]
	private float _distance;

	private float _offset;


	private void Start()
	{
		Initialize();
	}


	private void Initialize()
	{
		_offset = Random.Range( 0f, 7.5f );

		_animator.StartAnim( _obstacleType );
	}


	public void SetUp( int sideChance )
	{
		if (Random.Range( 0, 100 ) >= sideChance)
			StartCoroutine( SidesMove() );
		else
			StartCoroutine( CircleMove() );
	}


	private IEnumerator CircleMove()
	{
		while (true)
		{

			yield return new WaitForEndOfFrame();
		}
	}


	private IEnumerator SidesMove()
	{
		while (true)
		{
			Vector3 p = transform.localPosition;

			p.x = Mathf.Sin( _offset + Time.time * _moveSpeed ) * _distance;

			transform.localPosition = p;

			yield return new WaitForEndOfFrame();
		}
	}


	private void OnCollisionEnter( Collision other )
	{
		var player = other.gameObject.GetComponent<Player>();
		var playerHub = other.gameObject.GetComponentInParent<PlayerHub>();

		if (player != null && playerHub != null)
			playerHub.KillPlayer( player );
	}
}
