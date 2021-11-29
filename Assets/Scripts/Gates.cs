using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gates : MonoBehaviour
{
	private const float SIDE_FORCE = 10;
	private const float UP_FORCE = 7;
	private const float ROTATION_FORCE = 500;
	

	[SerializeField]
	private TextMeshPro _gatesText;

	[SerializeField]
	private Rigidbody _gatesRB;

	private int _number;

	private GatesType _type;

	private bool _isTriggered;
	private bool _isMoveRight;


	public void SetUp( GatesType type, string text, int num )
	{
		_gatesText.text = text;

		_number = num;
		_type = type;
	}


	private void OnTriggerEnter( Collider other )
	{
		PlayerHub playerHub = other.GetComponentInParent<PlayerHub>();

		if (playerHub != null && !_isTriggered)
		{
			ChangeHub( playerHub );

			DisableOtherGates();

			_isTriggered = true;

			Destroy( gameObject );
		}
	}


	private void DisableOtherGates()
	{
		Gates[] gates = transform.parent.gameObject.GetComponentsInChildren<Gates>();

		for (int i = 0; i < gates.Length; i++)
			gates[ i ].StartFalling();

	}


	public void StartFalling()
	{
		transform.parent = null;

		_isTriggered = true;

		ApplyForce();

		Destroy( gameObject, 5f );
	}


	private void ApplyForce()
	{
		_gatesRB.constraints = RigidbodyConstraints.None;

		_gatesRB.AddForce( Vector3.up * UP_FORCE, ForceMode.Impulse );

		if (_isMoveRight)
			_gatesRB.AddForce( Vector3.right * SIDE_FORCE, ForceMode.Impulse );
		else
			_gatesRB.AddForce( Vector3.left * SIDE_FORCE, ForceMode.Impulse );

		_gatesRB.AddTorque( Vector3.forward * ROTATION_FORCE, ForceMode.Impulse );
	}


	private void ChangeHub( PlayerHub hub )
	{
		int hubSize = hub.HubSize;
		int result = 0;

		switch (_type)
		{
			case GatesType.Multiplication:
				result = hubSize * _number;
				break;
			case GatesType.Addition:
				result = hubSize + _number;
				break;
			case GatesType.Division:
				result = hubSize / _number;
				break;
			case GatesType.Substraction:
				result = hubSize - _number;
				break;
		}

		int addition = hubSize - result;

		if (addition < 0)
			hub.AddPlayer( Mathf.Abs( addition ) );
		else
			hub.RemovePlayer( addition );
	}


	public bool IsMoveRight { get => _isMoveRight; set => _isMoveRight = value; }

}
