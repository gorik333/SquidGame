using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPlatform : MonoBehaviour
{
	[SerializeField]
	private GameObject _right;

	[SerializeField]
	private GameObject _left;

	[SerializeField]
	private float _moveSpeed;

	[SerializeField]
	private float _distance;

	private GameObject _current;

	private float _offset;

	private bool _isRight;
	private bool _isFirstTime;


	private void Start()
	{
		_offset = Random.Range( 0f, 7f );

		_isRight = Random.Range( 0, 100 ) > 50;

		if (_isRight)
			_current = _right;
		else
			_current = _left;

		_current.GetComponent<BoxCollider>().enabled = true;
	}


	private void Update()
	{
		Move();
	}


	private void Move()
	{
		Vector3 p = transform.localPosition;

		p.x = Mathf.Sin( _offset + Time.time * _moveSpeed ) * _distance;


		transform.localPosition = p;
	}


	private void OnTriggerEnter( Collider other )
	{
		var player = other.GetComponent<Player>();
		var playerHub = other.GetComponentInParent<PlayerHub>();

		if (player != null && playerHub != null)
		{
			if (!_isFirstTime)
				PlatformDestroy();

			playerHub.RemovePlayer( player );

			player.StartFalling();
		}
	}


	private void PlatformDestroy()
	{
		_isFirstTime = true;

		var platformParts = _current.GetComponentsInChildren<Rigidbody>();

		for (int i = 0; i < platformParts.Length; i++)
			platformParts[ i ].constraints = RigidbodyConstraints.None;
	}
}
