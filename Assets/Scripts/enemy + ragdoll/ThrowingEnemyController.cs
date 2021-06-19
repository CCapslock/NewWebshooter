using NaughtyAttributes;
using UnityEngine;

public class ThrowingEnemyController : MonoBehaviour
{
    [Foldout("Enemy Settings")]
    public Rigidbody HipsRigidBody;
    [Foldout("Enemy Settings")]
    public Rigidbody HeadRigidBody;
    [Foldout("Enemy Settings")]
    public Rigidbody SpineRigidBody;
    [Foldout("Enemy Settings")]
    public GameObject SpiderWeb;
    [Foldout("Enemy Settings")]
    public float ThrowingAnimationLenght;
    [Foldout("Enemy Settings")]
    public float MaxDistanceToPlayer;
    [Foldout("Enemy Settings")]
    public float EnemySpeed;

    [Foldout("Bomb Settings")]
    public GameObject Bomb;
    [Foldout("Bomb Settings")]
    public AnimationCurve MovementCurve;
    [Foldout("Bomb Settings")]
    public float BombSpeed;
    [Foldout("Bomb Settings")]
    public float BombMaxHeight;

    private MainGameController _mainGameController;
    private Transform _playerTransform;
    private Animator _animator;
    private Rigidbody[] _ragdollRigidBodyes;
    private Rigidbody _capsuleRigidBody;
    private Collider[] _ragdollColliders;
    private CapsuleCollider _capsuleCollider;
    private Collider _bombCollider;
    private Rigidbody _bombRigidBody;
    private GameObject Web;
    private Vector3 CustomWebPosition;
    private Vector3 _throwingVector;
    private float _magicNumber = 0.02f;
    private bool _isStucked;
    private bool _needToWalk = false;
    private bool _isEnemyWebbed = false;
    private bool _bombThrown = false;
    private bool _bombDetonated = false;
    [HideInInspector] public bool IsEnemyActive = false;
    [HideInInspector] public bool IsEnemyAttacking = false;
    private void Start()
    {
        _isStucked = false;
        CustomWebPosition = new Vector3(0, 0, -0.3f); // ������������ � ���������� �������� � � ���� ���������� ��������� �������
        _playerTransform = GameObject.FindGameObjectWithTag(TagManager.GetTag(TagType.Player)).transform;
        _animator = GetComponent<Animator>();
        _capsuleRigidBody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _bombCollider = Bomb.GetComponent<Collider>();
        _bombCollider.isTrigger = true;
        _bombRigidBody = Bomb.GetComponent<Rigidbody>();

        int num = 0;
        Rigidbody[] ragdollRigidBodyes = GetComponentsInChildren<Rigidbody>();
        _ragdollRigidBodyes = new Rigidbody[ragdollRigidBodyes.Length - 1];
        for (int i = 0; i < ragdollRigidBodyes.Length; i++)
        {
            if (ragdollRigidBodyes[i] != _bombRigidBody)
            {
                _ragdollRigidBodyes[num] = ragdollRigidBodyes[i];
                num++;
            }
        }

        num = 0;
        Collider[] ragdollColliders = GetComponentsInChildren<Collider>();
        _ragdollColliders = new Collider[ragdollColliders.Length - 1];
        for (int i = 0; i < ragdollColliders.Length; i++)
        {
            if (ragdollColliders[i] != _bombCollider)
            {
                _ragdollColliders[num] = ragdollColliders[i];
                num++;
            }
        }
        _capsuleRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        _mainGameController = FindObjectOfType<MainGameController>();
        HipsRigidBody.gameObject.AddComponent<RagdollCollisionChecker>().SetParametrs(this);
        HeadRigidBody.gameObject.AddComponent<RagdollCollisionChecker>().SetParametrs(this);
        SpineRigidBody.gameObject.AddComponent<RagdollCollisionChecker>().SetParametrs(this);
        TurnOffRagdoll();
    }
    private void FixedUpdate()
    {
        if (IsEnemyActive)
        {
            if (_needToWalk)
            {
                if (!IsNearPlayer())
                {
                    MoveEnemy();
                }
                else if (!IsNearPlayer() && IsEnemyAttacking)
                {
                    ActivateEnemy();
                }
                else if (IsNearPlayer() && !IsEnemyAttacking)
                {
                    EnemyAttack();
                }
            }
            transform.LookAt(_playerTransform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            if (IsEnemyActive)
            {
                ClearBomb();

                IsEnemyActive = false; _isEnemyWebbed = true;
                _mainGameController.EnemyBeenDefeated();
                SphereCollider collider = collision.gameObject.GetComponent<SphereCollider>();
                collider.isTrigger = true;
                _throwingVector = transform.position; if ((transform.position.z - collision.transform.position.z) * 10000f > 5500f)
                {
                    _throwingVector.z = (transform.position.z - collision.transform.position.z) * 10000;
                }
                else
                {
                    _throwingVector.z = 5500f;
                }
                _throwingVector.x = (transform.position.x - HipsRigidBody.transform.position.x) * 4000;
                _throwingVector.y = (transform.position.y - HipsRigidBody.transform.position.y) * 1f + 1000f;
                TurnOnRagdoll();
                for (int i = 0; i < _ragdollRigidBodyes.Length; i++)
                {
                    _ragdollRigidBodyes[i].AddForce(_throwingVector * 2f);
                }
                HipsRigidBody.AddForce(_throwingVector * 4f);
                //����� ����� ��� ��� �������� �������� ���������� ��������

            }
            else
            {
                ParticlesController.Current.MakeGlow(collision.transform.position);
            }
        }
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Bottom)) && IsEnemyActive)
        {
            ClearBomb();
            IsEnemyActive = false;
            _mainGameController.EnemyBeenDefeated();
        }
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.EnemyPart)) && IsEnemyActive)
        {
            ClearBomb();
            _mainGameController.EnemyBeenDefeated();
            TurnOnRagdoll();
            IsEnemyActive = false;
        }
        if (collision.gameObject.CompareTag("Enemy") && IsEnemyActive)
        {
            ClearBomb();
            IsEnemyActive = false;
            _mainGameController.EnemyBeenDefeated();
            TurnOnRagdoll();
        }
        if (collision.gameObject.CompareTag("ThrowingEnemy") && IsEnemyActive)
        {
            ClearBomb();
            IsEnemyActive = false;
            _mainGameController.EnemyBeenDefeated();
            TurnOnRagdoll();
        }
        if (collision.gameObject.CompareTag("DodgeEnemy") && IsEnemyActive)
        {
            ClearBomb();
            IsEnemyActive = false;
            _mainGameController.EnemyBeenDefeated();
            TurnOnRagdoll();
        }
    }

    private void ClearBomb()
    {
        if (!_bombThrown)
        {
            _bombDetonated = true;
            Bomb.GetComponent<Bomb>().DetonateBomb();
        }
    }

    #region Enemy Methods
    private void PrepareForThrowingBomb()
    {
        _animator.SetTrigger("ThrowBomb");
        Invoke("ThrowBomb", ThrowingAnimationLenght);
        Invoke("StartWalking", ThrowingAnimationLenght + 2f);
    }
    private void ThrowBomb()
    {
        if (!_bombDetonated)
        {
            _bombThrown = true;
            if (_bombCollider != null)
            {
                _bombCollider.isTrigger = false;
                if (IsEnemyActive)
                {
                    Bomb.GetComponent<Bomb>().ThrowBomb(MovementCurve, _playerTransform, BombSpeed, BombMaxHeight);
                }
            }
        }
    }
    private void StartWalking()
    {
        if (IsEnemyActive)
        {
            _needToWalk = true;
            _animator.SetTrigger("StartMoving");
        }
    }
    public void ThrowEnemy(Vector3 impulsePosition)
    {
        if (!_bombThrown && !_bombDetonated)
        {
            _bombDetonated = true;
            Bomb.GetComponent<Bomb>().DetonateBomb();
        }
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.GetTag(TagType.Player)))
        {
            ActivateEnemy();
        }
    }
    private bool IsNearPlayer()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) > MaxDistanceToPlayer)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void EnemyAttack()
    {
        IsEnemyAttacking = true;
        _animator.SetTrigger("StartAttacking");
    }
    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, EnemySpeed);
    }
    public void ActivateEnemy()
    {
        _capsuleRigidBody.constraints = RigidbodyConstraints.None;
        _capsuleRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        if (!_isStucked)
        {
            PrepareForThrowingBomb();
            IsEnemyActive = true;
        }
        else
        {
            _mainGameController.EnemyBeenDefeated();
        }
    }
    public void GetStickmanStucked(Collision collision, Vector3 positionOfBone, string name)
    {
        if (!_isStucked && _isEnemyWebbed)
        {
            _isStucked = true;
            TurnRagdollStucked();
            _capsuleCollider.enabled = false;
            _capsuleRigidBody.isKinematic = true;
            Web = Instantiate(SpiderWeb, positionOfBone + CustomWebPosition, Quaternion.identity);
            Web.transform.Rotate(new Vector3(0, 180f, Random.Range(0, 360f)));
            if (collision.GetContact(0).point.z - positionOfBone.z <= _magicNumber)
            {
                if (collision.GetContact(0).point.x > positionOfBone.x)
                {
                    Web.transform.rotation = Quaternion.Euler(Web.transform.rotation.eulerAngles + new Vector3(0, 90f, 0));
                }
                else if (collision.GetContact(0).point.x < positionOfBone.x)
                {
                    Web.transform.rotation = Quaternion.Euler(Web.transform.rotation.eulerAngles + new Vector3(0, -90f, 0));
                }
            }
            //Destroy(this);//����� � ��� �����, �� ����� ������� 
        }
    }
    #endregion

    #region Ragdoll Methods
    private void TurnOffRagdoll()
    {
        _capsuleCollider.isTrigger = false;
        for (int i = 0; i < _ragdollRigidBodyes.Length; i++)
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