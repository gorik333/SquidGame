using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GatesType
{
	Division,
	Multiplication,
	Addition,
	Substraction
}

public class GatesHub : MonoBehaviour
{
	[SerializeField]
	private GameObject _gates;

	private List<Gates> _spawnedGates = new List<Gates>();

	private float _offset = 0;

	private const int MAX_MUL = 2;
	private const int MAX_DIV = 2;

	private const float DISTANCE_MULTIPLIER = 4f;
	private const float SPEED_MULTIPLIER = 2f;

	private const float TILE_SIZE = 3f;


	private void Update()
	{
		Move();
	}


	private void Move()
	{
		Vector3 p = transform.localPosition;

		p.x = Mathf.Sin( _offset + Time.time * SPEED_MULTIPLIER ) * DISTANCE_MULTIPLIER;

		transform.localPosition = p;

		bool isRightSide = p.x >= 0;

		for (int i = 0; i < _spawnedGates.Count; i++)
			_spawnedGates[ i ].IsMoveRight = isRightSide;
	}


	public void SpawnGates( int count, float plus, float mul, int maxNum )
	{
		if (maxNum > 0)
		{
			Spawn( count );

			GatesSetUp( plus, mul, maxNum );
		}
	}


	private void Spawn( int count )
	{
		for (int i = 0; i < count; ++i)
		{
			Vector3 p = ( Vector3.left * TILE_SIZE * ( count - 1 ) ) / 2f + Vector3.right * i * TILE_SIZE;

			GameObject go = Instantiate( _gates, transform );

			go.transform.localPosition = p;

			_spawnedGates.Add( go.GetComponent<Gates>() );
		}
	}


	private void GatesSetUp( float plus, float mul, int maxNum )
	{
		_offset = Random.Range( 0, 5f );

		for (int i = 0; i < _spawnedGates.Count; i++)
		{
			GatesType resultType;

			int resultNumber;

			if (plus > Random.Range( 0, 100 ))
			{
				if (mul > Random.Range( 0, 100 ))
				{
					resultNumber = MAX_MUL;
					resultType = GatesType.Multiplication;
				}
				else
				{
					resultNumber = Random.Range( 1, maxNum );
					resultType = GatesType.Addition;
				}
			}
			else
			{
				if (mul > Random.Range( 0, 100 ))
				{
					resultNumber = MAX_DIV;
					resultType = GatesType.Division;
				}
				else
				{
					resultNumber = Random.Range( 1, maxNum );
					resultType = GatesType.Substraction;
				}
			}

			string gatesText = GetText( resultType, resultNumber );

			_spawnedGates[ i ].SetUp( resultType, gatesText, resultNumber );
		}
	}


	private static string GetText( GatesType type, int number )
	{
		switch (type)
		{
			case GatesType.Division: return "÷" + number;
			case GatesType.Multiplication: return "*" + number;
			case GatesType.Addition: return "+" + number;
			case GatesType.Substraction: return "-" + number;
		}

		return "";
	}
}
