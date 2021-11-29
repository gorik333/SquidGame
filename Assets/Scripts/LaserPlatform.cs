using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct LaserSection
{
	[Header( "Enemy in section" )]
	public Enemy[] EnemyArray;
}

public class LaserPlatform : MonoBehaviour
{
	[SerializeField]
	private LaserSection[] _laserSection;

	private float _delay = 2f;


	private void Start()
	{
		StartLaser();
	}


	public void StartLaser()
	{
		StartCoroutine( Laser() );
	}


	private IEnumerator Laser()
	{
		int i = 0;

		while (true)
		{
			for (int j = 0; j < _laserSection[ i ].EnemyArray.Length; j++)
				_laserSection[ i ].EnemyArray[ j ].TurnOnLaserAttackState();

			yield return new WaitForSeconds( _delay );

			for (int j = 0; j < _laserSection[ i ].EnemyArray.Length; j++)
				_laserSection[ i ].EnemyArray[ j ].TurnOffLaserAttackState();

			i++;

			if (i > _laserSection.Length - 1)
				i = 0;
		}
	}
}
