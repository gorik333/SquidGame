using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
	private const float CENTER_DELAY = 2f;

	[SerializeField]
	private Transform _fallTrigger;

	[SerializeField]
	private float _moveSpeed;

	[SerializeField]
	private float _distance;

	[SerializeField]
	private bool _isGenerateOffset;

	[SerializeField]
	private float _sign = 1f;

	[SerializeField]
	private bool _isAbsolute = false;

	private float _offset;


	private void Start()
	{
		if (_isGenerateOffset)
			_offset = Random.Range( 0f, 5f );
	}


	private void Update()
	{
		Move();
	}


	private void Move()
	{
		Vector3 p = transform.localPosition;

		if (_isAbsolute)
		{
			float d = Mathf.Sin( _offset + Time.time * _moveSpeed );

			if (_fallTrigger != null && d > 0.4f)
			{
				if (!_fallTrigger.gameObject.activeSelf)
					_fallTrigger.gameObject.SetActive( true);

				Vector3 scale = _fallTrigger.transform.localScale;

				scale.x = d;

				_fallTrigger.transform.localScale = scale;
			}
			else if (_fallTrigger != null && d <= 0.4f)
			{
				if (_fallTrigger.gameObject.activeSelf)
					_fallTrigger.gameObject.SetActive( false );
			}

			d *= Mathf.Clamp01( d * CENTER_DELAY );

			p.x = Mathf.Abs( d ) * _distance * _sign;
		}
		else
			p.x = Mathf.Sin( _offset + Time.time * _moveSpeed ) * _distance;

		transform.localPosition = p;
	}


	private void OnTriggerEnter( Collider other )
	{
		var player = other.GetComponent<Player>();
		var playerHub = other.GetComponentInParent<PlayerHub>();

		if (player != null && playerHub != null)
		{
			playerHub.RemovePlayer( player );
			player.StartFalling();
		}
	}
}