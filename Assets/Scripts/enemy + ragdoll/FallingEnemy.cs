using NaughtyAttributes;
using UnityEngine;

public class FallingEnemy : MonoBehaviour
{
	[Foldout("Settings")]
	public Rigidbody HipsRigidBody;
	[Foldout("Settings")]
	public Rigidbody HeadRigidBody;
	[Foldout("Settings")]
	public Rigidbody SpineRigidBody;
	[Foldout("Settings")]
	public GameObject SpiderWeb;
	[Foldout("Settings")]
	public float EnemySpeed;
	[Foldout("Settings")]
	public float MaxDistanceToPlayer;
	[Foldout("Settings")]

	private MainGameController _mainGameController;
	private BonusLvlController _bonusLvlController;
	private Transform _playerTransform;
	private Animator _animator;
	private Rigidbody _capsuleRigidBody;
	private Rigidbody _capsuleRigidbody;
	private Rigidbody[] _ragdollRigidBodyes;
	private CapsuleCollider _capsuleCollider;
	private Collider[] _ragdollColliders;
	private GameObject _web;
	private Vector3 _customWebPosition;
	private Vector3 _idlePosition;
	private Vector3 _throwingVector;
	private float _magicNumber = 0.05f;
	private float _movingToIdleSpeed = 0.05f;
	private float _movingUpAndDownSpeed = 0.05f;
	private bool _isHitByEnemy = false;
	private bool _isEnemyWebbed = false;
	private bool _needToMoveUpAndDown;
	private bool _needToMove;
	[HideInInspector] public bool IsStucked;
	[HideInInspector] public bool IsEnemyActive = false;
	[HideInInspector] public bool IsEnemyAttacking = false;
	private void Start()
	{
		IsStucked = false;
		_customWebPosition = new Vector3(0, 0, -0.3f); // прибавляется к кординатам предмета и в этих кординатах спавнится паутина
		_playerTransform = GameObject.FindGameObjectWithTag(TagManager.GetTag(TagType.Player)).transform;
		_animator = GetComponent<Animator>();
		_capsuleRigidBody = GetComponent<Rigidbody>();
		_capsuleCollider = GetComponent<CapsuleCollider>();
		_capsuleRigidbody = GetComponent<Rigidbody>();
		_ragdollRigidBodyes = GetComponentsInChildren<Rigidbody>();
		_mainGameController = FindObjectOfType<MainGameController>();
		_ragdollColliders = GetComponentsInChildren<Collider>();
		HipsRigidBody.gameObject.AddComponent<RagdollCollisionChecker>().SetParametrs(this);
		HeadRigidBody.gameObject.AddComponent<RagdollCollisionChecker>().SetParametrs(this);
		SpineRigidBody.gameObject.AddComponent<RagdollCollisionChecker>().SetParametrs(this);
		_capsuleRigidbody.constraints = RigidbodyConstraints.FreezeAll;

		TurnOffRagdoll();

		int num = Random.Range(0, 3);
		switch (num)
		{
			case 0:
				_animator.SetTrigger("Fall1");
				break;
			case 1:
				_animator.SetTrigger("Fall2");
				break;
			case 2:
				_animator.SetTrigger("Fall3");
				break;
		}

		int num1 = Random.Range(0, 360);
		int num2 = Random.Range(0, 360 - num1);
		int num3 = Random.Range(0, 360 - num1 - num2);

		transform.Rotate(new Vector3(num1, num2, num3));
	}
	public void SendEnemy(float yPosition, float movingSpeed, BonusLvlController bonusLvlController)
	{
		_bonusLvlController = bonusLvlController;
		_idlePosition = transform.position;
		_idlePosition.y = yPosition;
		_needToMove = true;
		_movingToIdleSpeed = movingSpeed;
	}
	private void FixedUpdate()
	{
		if (_needToMoveUpAndDown)
		{

		}
		if (_needToMove)
		{
			MoveToIdlePosition();
		}
	}
	private void MoveToIdlePosition()
	{
		if (Vector3.Distance(transform.position, _idlePosition) > 10f)
		{
			transform.position = Vector3.MoveTowards(transform.position, _idlePosition, _movingToIdleSpeed);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, _idlePosition, _movingToIdleSpeed * (Vector3.Distance(transform.position, _idlePosition) / 10f));
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)) && !_isEnemyWebbed)
		{
			_needToMove = false;
			IsEnemyActive = false;
			_isEnemyWebbed = true;
			SphereCollider collider = collision.gameObject.GetComponent<SphereCollider>();
			collider.isTrigger = true;
			_bonusLvlController.DefeatEnemy();
			_throwingVector = transform.position;
			_throwingVector.z = 3000f;
			_throwingVector.x = (transform.position.x - HipsRigidBody.transform.position.x) * 1000;
			_throwingVector.y = (transform.position.y - HipsRigidBody.transform.position.y) * 1f + 1000f;
			TurnOnRagdoll();
			for (int i = 0; i < _ragdollRigidBodyes.Length; i++)
			{
				_ragdollRigidBodyes[i].AddForce(_throwingVector * 1f);
			}
			HipsRigidBody.AddForce(_throwingVector * 4f);
			//много чисел так как подгонял наиболее подходящие значения
		}
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Bottom)) && IsEnemyActive)
		{
			IsEnemyActive = false;
			_mainGameController.EnemyBeenDefeated();
		}
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.EnemyPart)) && IsEnemyActive)
		{
			IsEnemyActive = false;
			_isHitByEnemy = true;
			_mainGameController.EnemyBeenDefeated();
			TurnOnRagdoll();
		}
	}
	#region Enemy Methods
	private void MoveUpAndDown()
	{

	}
	public void ThrowEnemy(Vector3 impulsePosition)
	{
		if (IsEnemyActive)
		{
			IsEnemyActive = false;
			_mainGameController.EnemyBeenDefeated();
			_throwingVector = transform.position;
			_throwingVector.z = (transform.position.z - impulsePosition.z) * 1000f;
			_throwingVector.x = (transform.position.x - impulsePosition.x) * 1000f;
			_throwingVector.y = 0f;
			TurnOnRagdoll();
			for (int i = 0; i < _ragdollRigidBodyes.Length; i++)
			{
				_ragdollRigidBodyes[i].AddForce(_throwingVector * 1.5f);
			}
			HipsRigidBody.AddForce(_throwingVector * 4f);
		}
	}
	public void GetStickmanStucked(Collision collision, Vector3 positionOfBone, string name)
	{
		if (!IsStucked && _isEnemyWebbed)
		{
			IsStucked = true;
			TurnRagdollStucked();
			_capsuleCollider.enabled = false;
			_capsuleRigidBody.isKinematic = true;
			for (int i = 0; i < _ragdollRigidBodyes.Length; i++)
			{
				_ragdollRigidBodyes[i].velocity = Vector3.zero;
			}
			if (!_isHitByEnemy)
			{
				_web = Instantiate(SpiderWeb, positionOfBone + _customWebPosition, Quaternion.identity);
				_web.transform.Rotate(new Vector3(0, 180f, Random.Range(0, 360f)));
				if (collision.GetContact(0).point.z - transform.position.z <= _magicNumber)
				{
					if (collision.GetContact(0).point.x > transform.position.x)
					{
						_web.transform.rotation = Quaternion.Euler(_web.transform.rotation.eulerAngles + new Vector3(0, 90f, 0));
					}
					else if (collision.GetContact(0).point.x < positionOfBone.x)
					{
						_web.transform.rotation = Quaternion.Euler(_web.transform.rotation.eulerAngles + new Vector3(0, -90f, 0));
					}
				}
				_bonusLvlController.StickObject(_web.transform);
				_bonusLvlController.StickObject(transform);
			}
			//Destroy(this);//можно и без этого, хз зачем добавил 
		}
	}
	#endregion

	#region Ragdoll Methods
	private void TurnOffRagdoll()
	{
		_capsuleCollider.isTrigger = false;
		for (int i = 0; i < _ragdollColliders.Length; i++)
		{
			if (_ragdollColliders[i] != _capsuleCollider)
			{
				_ragdollColliders[i].isTrigger = true;
			}
			if (_ragdollRigidBodyes[i] != _capsuleRigidBody)
			{
				_ragdollRigidBodyes[i].isKinematic = true;
			}
		}
		_capsuleRigidBody.isKinematic = false;
	}
	private void TurnOnRagdoll()
	{
		_animator.enabled = false;
		for (int i = 0; i < _ragdollColliders.Length; i++)
		{
			if (_ragdollColliders[i] != _capsuleCollider)
			{
				_ragdollColliders[i].isTrigger = false;
			}
			if (_ragdollRigidBodyes[i] != _capsuleRigidBody)
			{
				_ragdollRigidBodyes[i].isKinematic = false;
			}
		}
	}
	private void TurnRagdollStucked()
	{
		SpineRigidBody.isKinematic = true;
		HipsRigidBody.isKinematic = true;
		SpineRigidBody.GetComponent<Collider>().isTrigger = true;
		HipsRigidBody.GetComponent<Collider>().isTrigger = true;
	}
	#endregion
}
