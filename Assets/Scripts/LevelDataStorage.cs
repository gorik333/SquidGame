using UnityEngine;

public enum ObstacleMoveType
{
	Circle,
	Sides
}

public enum LevelType
{
	Default, 
	ThreeFigures
}

[System.Serializable]
public struct ObstacleConf
{
	[Header( "Obstacle prefab" )]
	public GameObject ObstaclePrefab;

	[Header( "Spawn chance" ), Range( 0, 100 )]
	public int SpawnChance;

	[Header( "Horizontal (0) - circle (100) movement" ), Range( 0, 100 )]
	public int SideChance;
}

[System.Serializable]
public struct SectionConf
{
	[Header( "Section prefab" )]
	public GameObject SectionPrefab;

	[Header( "Spawn chance" ), Range( 0, 100 )]
	public int SpawnChance;

	[Header( "Next obstacle is easy, because this is hard" )]
	public bool IsHard;
}

[System.Serializable]
public struct LevelConf
{
	[Header( "Main settings" )]
	public LevelType LevelType;

	public int LevelLength;

	[Range( 0, 3 )]
	public int Difficulty;

	[Header( "Obstacles" )]
	public ObstacleConf[] ObstaclesConf;

	[Header( "Sections" )]
	public SectionConf[] SectionsConf;

	[Header( "Figures" )]
	public GameObject[] Figure;

	[Header( "Gates" )]
	public float Plus;
	public float Mul;
	public int MaxNum;

	[Header( "Doll" )]
	public float RotateTime;
	public float RotateDelay;
	public float LookDuration;
}


public class LevelDataStorage : MonoBehaviour
{
	[SerializeField]
	private int _selectedLevel;

	[SerializeField]
	private LevelConf[] _levelConf;


	public LevelConf GetLevelConf( int level )
	{
#if UNITY_EDITOR
		if (_selectedLevel >= 0)
			return _levelConf[ _selectedLevel ];
#endif
		return _levelConf[ level % _levelConf.Length ];
	}
}
