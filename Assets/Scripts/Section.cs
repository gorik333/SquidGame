using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Section : MonoBehaviour
{
	[SerializeField]
	private float _length;

	[SerializeField]
	private Transform[] _spawnPos;

	[SerializeField]
	private Transform _figureHandler;

	[SerializeField, Header( "Doll" )]
	private Doll _doll;

	[SerializeField]
	private GatesHub _gatesHub;

	private static int s_figuresCount;


	public void SetUp( LevelConf levelConf )
	{
		SpawnObstacles( levelConf.ObstaclesConf, levelConf.Difficulty );

		if (_doll != null)
			_doll.SetUp( levelConf );
		if (levelConf.LevelType == LevelType.ThreeFigures)
			SpawnFigures( levelConf );
		if (_gatesHub != null && levelConf.LevelType == LevelType.Default)
			_gatesHub.SpawnGates( Random.Range( 1, 3 ), levelConf.Plus, levelConf.Mul, levelConf.MaxNum );
	}


	private void SpawnFigures( LevelConf levelConf )
	{
		if (s_figuresCount < levelConf.Figure.Length)
		{
			Instantiate( levelConf.Figure[ s_figuresCount % levelConf.Figure.Length ], _figureHandler );
		}

		s_figuresCount++;
	}


	private void SpawnObstacles( ObstacleConf[] conf, int difficulty )
	{
		int max = conf.Max( e => e.SpawnChance );
		int min = conf.Min( e => e.SpawnChance );

		if (min == 0)
			conf[ 0 ].SpawnChance = 1;

		var confOrdered = conf.OrderBy( e => e.SpawnChance ).ToArray();

		for (int i = 0; i < _spawnPos.Length && i < difficulty; i++)
		{
			for (int j = 0; j < confOrdered.Length; j++)
			{
				int chance = Random.Range( 1, max + min );

				if (chance <= confOrdered[ j ].SpawnChance)
				{
					Spawn( conf[ j ].ObstaclePrefab, _spawnPos[ i ].position, confOrdered[ j ].SideChance );

					break;
				}
				else
				{
					chance -= confOrdered[ j ].SpawnChance;

					j--;

					if (j < 0)
						j = 0;
				}
			}
		}
	}


	private void Spawn( GameObject prefab, Vector3 pos, int sideChance )
	{
		var obstacle = Instantiate( prefab, pos, Quaternion.identity, transform ).GetComponent<Obstacle>();

		obstacle.SetUp( sideChance );
	}


	public float PlatformLength => _length;
}
