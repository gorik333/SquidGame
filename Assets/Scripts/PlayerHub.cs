using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : MonoBehaviour
{
	[SerializeField]
	private GameObject _player;

	private const float MOVE_SPEED = 10;
	private const float THROW_RADIUS = 1f;

	private Vector3 _startPos;

	private bool _isCanMove;
	private bool _isMoving;

	private List<Player> _spawnedPlayer = new List<Player>();


	private void Awake()
	{
		_startPos = transform.position;
	}


	private void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown( KeyCode.A ))
			AddPlayer( 1 );

		if (Input.GetKey( KeyCode.D ))
			AddPlayer( 1 );
#endif
	}


	public void AddPlayer( int count )
	{
		if (count <= 0)
			count = 1;

		for (int i = 0; i < count; i++)
		{
			var player = Instantiate( _player, transform ).GetComponent<Player>();
			var newPos = player.transform.position + new Vector3( Random.Range( -THROW_RADIUS, THROW_RADIUS ), 0, Random.Range( -THROW_RADIUS, THROW_RADIUS ) );

			player.transform.position = newPos;

			_spawnedPlayer.Add( player );

			if (_isMoving)
				player.Animator.StartAnim( EntitiesAnim.Run );
		}
	}


	public void RemovePlayer( Player player )
	{
		_spawnedPlayer.Remove( player );

		CheckIfAllDead();
	}


	public void KillPlayer( Player player )
	{
		_spawnedPlayer.Remove( player );

		player.Kill();

		CheckIfAllDead();
	}


	public void RemovePlayer( int count )
	{
		if (count >= _spawnedPlayer.Count)
			count = _spawnedPlayer.Count;

		List<Player> toDestroy = new List<Player>();

		for (int i = 0; i < count; i++)
			toDestroy.Add( _spawnedPlayer[ i ] );

		for (int i = 0; i < toDestroy.Count; i++)
		{
			if (_spawnedPlayer.Contains( toDestroy[ i ] ))
				_spawnedPlayer.Remove( toDestroy[ i ] );
		}

		for (int i = 0; i < toDestroy.Count; i++)
			Destroy( toDestroy[ i ].gameObject );

		CheckIfAllDead();
	}


	private void CheckIfAllDead()
	{
		if (_spawnedPlayer.Count == 0)
			Die();
	}


	private void Die()
	{
		Game.Instance.onGameOver();
	}


	public void Move()
	{
		if (_isCanMove)
			transform.Translate( MOVE_SPEED * Time.deltaTime * Vector3.forward );
	}


	public void StartRunAnim()
	{
		_isMoving = true;

		for (int i = 0; i < _spawnedPlayer.Count; i++)
			_spawnedPlayer[ i ].Animator.StartAnim( EntitiesAnim.Run );
	}


	public void StopRunAnim()
	{
		_isMoving = false;

		for (int i = 0; i < _spawnedPlayer.Count; i++)
			_spawnedPlayer[ i ].Animator.StopAnim( EntitiesAnim.Run );
	}


	public void OnStart()
	{
		_isCanMove = true;
	}


	public void OnGameOver()
	{
		_isCanMove = false;

		_spawnedPlayer.Clear();
	}


	public void OnFinish()
	{
		_isCanMove = false;
	}


	public void OnRestart()
	{
		transform.localPosition = _startPos;

		if (_spawnedPlayer.Count == 0)
		{
			AddPlayer( 1 );
		}
		else
		{
			int lastCount = _spawnedPlayer.Count;

			RemovePlayer( lastCount - 1 );

			AddPlayer( lastCount - 1 );
		}
	}


	public int HubSize => _spawnedPlayer.Count;


	public List<Player> Hub => _spawnedPlayer;
}
