using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class BonusLvlController : MonoBehaviour
{
	public GameObject Coin;
	public GameObject Wall;
	public GameObject Web;
	public Vector3 StartThrownigPosition;
	public int MaxCoins;
	public float TimeBeforeThrownig = 0.4f;

	[Foldout("FallingLVL")]
	public Vector3[] BuildingsPositions;
	[Foldout("FallingLVL")]
	public Vector3 ParticlesPosition;
	[Foldout("FallingLVL")]
	public Vector3 ParticlesRotation;
	[Foldout("FallingLVL")]
	public float MovingSpeed;
	[Foldout("FallingLVL")]
	public float BotomCoordinat;
	[Foldout("FallingLVL")]
	public float RoofCoordinat;
	[Foldout("FallingLVL")]

	private Transform[] _movingHouses;
	private MainGameController _mainGameController;
	private GameObject _throwedObject;
	private ThrowingObject _throwedObjectScript;
	private List<Transform> _stickedObjects;
	private Vector2 _minScreenPosition, _maxScreenPosition;
	private Vector3 _tempVector;
	private float _timeBeforeThrowing;
	private float _widthOfScreen;
	private int _numberOfCoinsCaught = 0;
	private bool _needToMoveHouses;

	private void Awake()
	{
		_mainGameController = FindObjectOfType<MainGameController>();
		Camera.main.orthographic = true;
		_minScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		_maxScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		_widthOfScreen = _maxScreenPosition.x * 2;
		Camera.main.orthographic = false;
		_tempVector = new Vector3();
		_stickedObjects = new List<Transform>();
	}
	private void FixedUpdate()
	{
		if (_needToMoveHouses)
		{
			MoveHouses();
		}
	}
	public void CaughtCoin()
	{
		_numberOfCoinsCaught++;
	}
	public void StartSpawner()
	{
		_timeBeforeThrowing = 0f;
		for (int i = 0; i < MaxCoins; i++)
		{
			_timeBeforeThrowing += TimeBeforeThrownig;
			Invoke("SpawnCoin", _timeBeforeThrowing);
			if (i == MaxCoins - 1)
			{
				_timeBeforeThrowing += 5;
				Invoke("EndLvl", _timeBeforeThrowing);
			}
		}
	}
	public void StartMoving()
	{
		_needToMoveHouses = true;
	}
	private void SpawnCoin()
	{
		StartThrownigPosition.x = _minScreenPosition.x + _widthOfScreen * Random.Range(0f, 1f);
		_throwedObject = Instantiate(Coin, StartThrownigPosition, Quaternion.identity);
		_throwedObjectScript = _throwedObject.GetComponent<ThrowingObject>();
		_throwedObjectScript.SetForce(Random.Range(3000f, 3501f));
		_throwedObjectScript.TrowObjectUp();
		_throwedObjectScript.bonusLvlController = this;
	}
	public void StartBonusPart()
	{
		Wall.SetActive(true);
		StartSpawner();
		/*
		PrepareBuildings();
		StartMoving();
		FindObjectOfType<ParticlesController>().StartWindParticle(ParticlesPosition, ParticlesRotation);*/
	}
	private void PrepareBuildings()
	{
		_movingHouses = new Transform[BuildingsPositions.Length];
		for (int i = 0; i < BuildingsPositions.Length; i++)
		{
			_movingHouses[i] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding2"]), BuildingsPositions[i], Quaternion.Euler(new Vector3(-90f,0,90f))).transform;
			_movingHouses[i].gameObject.isStatic = false;
			//_movingHouses[i].gameObject.GetComponent<Wall>().IsBonusLvlWall = true;
			_movingHouses[i].gameObject.tag = TagManager.GetTag(TagType.Wall);
			Wall WallObject = _movingHouses[i].gameObject.AddComponent<Wall>();
			WallObject.Web = Web;
			WallObject.IsBonusLvlWall = true;
		}
	}
	private void EndLvl()
	{
		_mainGameController.EndBonusLvl(_numberOfCoinsCaught);
	}
	public void StickObject(Transform transform)
	{
		_stickedObjects.Add(transform);
	}
	private void MoveHouses()
	{
		for (int i = 0; i < _movingHouses.Length; i++)
		{
			_tempVector = _movingHouses[i].position;
			_tempVector.y += MovingSpeed;
			_movingHouses[i].position = _tempVector;
			if (_movingHouses[i].position.y >= RoofCoordinat)
			{
				_tempVector = _movingHouses[i].position;
				_tempVector.y = BotomCoordinat;
				_movingHouses[i].position = _tempVector;
			}
		}
		try
		{
			foreach (Transform stickedObject in _stickedObjects)
			{
				_tempVector = stickedObject.position;
				_tempVector.y += MovingSpeed;
				stickedObject.position = _tempVector;
				if (stickedObject.position.y >= RoofCoordinat)
				{
					_stickedObjects.Remove(stickedObject);
					Destroy(stickedObject.gameObject);
				}
			}
		}
		catch
		{
		}
	}


}
