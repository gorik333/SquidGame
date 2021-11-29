using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGen : MonoBehaviour
{
	[Header( "Figure handler" )]
	[SerializeField]
	private GameObject _handler;

	[Header( "Figures" )]
	[SerializeField]
	private GameObject[] _figure;

	[Header( "Sections" )]
	[SerializeField]
	private GameObject _startSection;

	[SerializeField]
	private GameObject _middleSection;

	[SerializeField]
	private GameObject _finishDefault;

	[SerializeField]
	private GameObject _finishFigure;

	private LevelConf _levelConf;

	private List<GameObject> _spawnedItems = new List<GameObject>();

	private bool _isPrevSectionHard;


	public void OnRestart( LevelConf levelConf )
	{
		ClearAll();

		SetUp();
		_levelConf = levelConf;

		GenerateLevel();
	}


	private void SetUp()
	{
		_isPrevSectionHard = false;
	}


	public void OnGameEnd()
	{
		DisableAll();
	}


	private void DisableAll()
	{

	}


	private void GenerateLevel()
	{
		float positionZ = 0;

		for (int i = 0; i < _levelConf.LevelLength; i++)
		{
			Vector3 spawnPosition = new Vector3( 0, 0, positionZ );

			if (i == 0)
			{
				positionZ += SpawnSection( _startSection, spawnPosition, true );
			}

			if (i == 1)
			{
				positionZ += SpawnSection( _middleSection, spawnPosition );
			}

			if (i > 1 && i != _levelConf.LevelLength - 1)
			{
				positionZ += SpawnSection( GetRandomSection(), spawnPosition );
			}

			if (i == _levelConf.LevelLength - 1)
			{
				positionZ += SpawnSection( GetFinishSection(), spawnPosition, true );
			}
		}
	}


	private GameObject GetFinishSection()
	{
		switch (_levelConf.LevelType)
		{
			case LevelType.Default:
				return _finishDefault;
			case LevelType.ThreeFigures:
				return _finishFigure;
			default:
				return _finishDefault;
		}
	}



	private GameObject GetRandomSection()
	{
		var sections = _levelConf.SectionsConf;

		sections.OrderBy( e => e.SpawnChance );

		for (int i = 0; i < sections.Length; i++)
		{
			if (Random.Range( 0, 100 ) <= sections[ i ].SpawnChance
				&& ( !sections[ i ].IsHard || !_isPrevSectionHard ))
			{
				if (sections[ i ].IsHard)
					_isPrevSectionHard = true;

				return sections[ i ].SectionPrefab;
			}
		}

		_isPrevSectionHard = false;

		return _middleSection;
	}


	private float SpawnSection( GameObject sectionPrefab, Vector3 spawnPosition, bool isStartFinish = false)
	{
		GameObject objSection = Instantiate( sectionPrefab, spawnPosition, Quaternion.identity, transform );

		var section = objSection.GetComponent<Section>();

		if (!isStartFinish)
			section.SetUp( _levelConf );

		return section.PlatformLength;
	}


	public void ClearAll()
	{
		foreach (Transform t in transform)
			Destroy( t.gameObject );

		_spawnedItems.Clear();
	}
}
