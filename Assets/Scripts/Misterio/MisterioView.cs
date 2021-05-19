using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisterioView : MonoBehaviour, IBoss
{

	[SerializeField] private bool isReal;
	[SerializeField] private MisterioState _state;
	private MisterioState _nextState;
	[SerializeField] private int _currentPhase = 0;
	[SerializeField] private int _maxPhase = 1;
	[SerializeField] private GameObject _misterioClone;
	[SerializeField] private Transform _circleCenter;

	private int _interval;
	private Vector3 _baseCirclePosition;

	public float CircleRadiusPosition;
	public PlayerMovement Player;


	public MisterioState State => _state;
	public MisterioState NextState => _nextState;
	public bool IsReal => isReal;
	public Transform Transform => transform;
	public Transform CircleCenter => _circleCenter;
	public Vector3 BaseCirclePosition => _baseCirclePosition;

	public void Awake()
	{
		Player = FindObjectOfType<PlayerMovement>();
		if (MainGameController.BossContainter == null)
			MainGameController.BossContainter = this;
		if (MisterioController.Current == null)
		{
			MisterioController.Current = new MisterioController();
		}
		SetState(MisterioState.Awaiting);
	}

	public void Start()
	{
		MisterioController.Current.AddMisterioToList(this);
	}

	public void Update()
	{
		if (isReal)
		{
			if (MisterioController.Current != null)
			{
				MisterioController.Current.Execute();
			}
		}
	}

	public void SetState(MisterioState state)
	{
		_state = state;
	}

	public void SetNextState(MisterioState state)
	{
		_nextState = state;
	}
	public void CheckState()
	{
		if (_nextState != _state)
		{
			_state = _nextState;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
		{
			if (State == MisterioState.Idle)
			{
				if (IsReal)
				{
					ChangePhase();
				}
				else
				{
					DestroyMisterio();
				}
			}
			else
			{
				ParticlesController.Current.MakeGlow(transform.position);
			}
		}
	}

	public void AwakeMisterio()
	{
		MisterioController.Current.ChangeNextState(this, MisterioState.Awake);
	}

	public void ChangePhase()
	{
		_currentPhase++;
		if (_currentPhase > _maxPhase)
		{
			LevelVictory();
			return;
		}
		else
		{
			MisterioController.Current.RemoveAllFakeMisterio();
			MisterioController.Current.ChangeNextState(this, MisterioState.Puffed);
		}
	}

	public void LevelVictory()
	{
		GetComponent<BossRagdollController>().ThrowEnemy();
		MisterioController.Current.RemoveAllFakeMisterio();
		MisterioController.Current.RemoveAllMisterio();
		MisterioController.Current = null;
		MainGameController.BossContainter = null;
		//Ενδγειμ
		FindObjectOfType<MainGameController>().EnemyBeenDefeated();
	}

	public void SpawnClones()
	{
		ParticlesController.Current.MakeMagicExplosion(transform.position);
		SetCircleRadiusPosition(Random.Range(0, 360));
		_interval = 360 / (_currentPhase + 2);
		GameObject obj;
		MisterioView view;
		for (int i = 0; i < _currentPhase + 1; i++)
		{
			obj = Instantiate(_misterioClone, Transform.position, Transform.rotation);
			view = obj.GetComponent<MisterioView>();
			view.SetState(MisterioState.Transporting);
			view.SetNextState(MisterioState.Transporting);
			view.SetCircleRadiusPosition(CircleRadiusPosition + (_interval * (i + 1)));
			if (view.isReal)
			{
				SetReal(false);
			}
		}
	}

	public void DestroyMisterio()
	{
		ParticlesController.Current.MakeMagicExplosion(transform.position);
		Destroy(this.gameObject);
		MisterioController.Current.RemoveMisterioFromList(this);
	}

	public void SetCircleRadiusPosition(float position)
	{
		CircleRadiusPosition = position;
		if (CircleRadiusPosition > 360)
		{
			CircleRadiusPosition -= 360;
		}
		SetBaseCirclePosition(new Vector3(
			Mathf.Cos(CircleRadiusPosition * Mathf.PI / 180) * 2f,
			Mathf.Sin(CircleRadiusPosition * Mathf.PI / 180) * 2f,
			0) + CircleCenter.position);
	}

	public void SetBaseCirclePosition(Vector3 position)
	{
		_baseCirclePosition = position;
	}

	public void OnDestroy()
	{
		if (MisterioController.Current != null)
		{
			MisterioController.Current.RemoveMisterioFromList(this);
		}
	}

	public void AwakeBoss()
	{
		MisterioController.Current.ChangeNextState(this, MisterioState.Awake);
	}

	public void SetReal(bool value)
	{
		isReal = value;
	}
}