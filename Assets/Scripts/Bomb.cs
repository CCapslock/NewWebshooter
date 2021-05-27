using UnityEngine;

public class Bomb : MonoBehaviour
{
	public LayerMask _enemyLayer;

	private Rigidbody _rigidbody;
	private Vector3 RotationVector;
	private bool _needToMoveBomb = false;

	//movingBomb
	private AnimationCurve _curve;
	private Transform _destinationTransform;
	private Vector3 _destinationVector;
	private Vector3 _movingVector;
	private Vector3 _customDestinationVector;
	private float _bombMoveSpeed;
	private float _maxDistance;
	private float _maxHeight;
	private float _startingHeight;
	private ParticlesController _particlesController;

	private bool _isDetonated = false;
	public bool NeedToRotate = true;
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_particlesController = FindObjectOfType<ParticlesController>();
		int num1 = Random.Range(0, 3);
		int num2 = Random.Range(0, 3 - num1);
		int num3 = Random.Range(0, 3 - num1 - num2);
		_customDestinationVector = new Vector3(0, 0, 0.5f);
		RotationVector = new Vector3(num1, num2, num3);
	}
	private void FixedUpdate()
	{
		/*
		if (NeedToRotate)
			RotateObject();
		if (_needToMoveBomb)
			MoveBomb();
		 */		
	}
	private void RotateObject()
	{
		_rigidbody.transform.Rotate(RotationVector);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
		{
			Destroy(collision.gameObject);
			DetonateBomb();
		}
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Player)))
		{
			DetonateBomb();
			HealthController.Current.GetHitFromBomb();
		}
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Wall)))
		{
			DetonateBomb();
		}
	}
	public void DetonateBomb()
	{
		if (!_isDetonated)
		{
			_particlesController.MakeSmallExplosion(transform.position);
			_isDetonated = true;
			Collider[] sds = Physics.OverlapSphere(transform.position, 8f, _enemyLayer);
			for (int i = 0; i < sds.Length; i++)
			{
				try
				{
					sds[i].GetComponent<ThrowingEnemyController>().ThrowEnemy(transform.position);
				}
				catch { }
				try
				{
					sds[i].GetComponent<EnemyController>().ThrowEnemy(transform.position);
				}
				catch { }
			}
			Destroy(this.gameObject);
		}
	}
	public void ThrowBomb(AnimationCurve curve, Transform destinationTransform, float speed, float maxHeight)
	{
		transform.parent = null;
		_rigidbody.useGravity = true;
		/*
		_curve = curve;
		_destinationTransform = destinationTransform;
		_destinationVector = _destinationTransform.position;
		_bombMoveSpeed = speed;
		_maxDistance = Vector3.Distance(transform.position, destinationTransform.position);
		_startingHeight = transform.position.y;
		_maxHeight = maxHeight;
		_needToMoveBomb = true;
		NeedToRotate = true;
		 */


		float _AngleInRadians = 45 * Mathf.PI / 180;
		Vector3 _fromTo = destinationTransform.position - transform.position;
		Vector3 _fromToXZ = new Vector3(_fromTo.x, 0f, _fromTo.z);

		float _xMagnitude = _fromToXZ.magnitude;
		float _y = _fromTo.y;

		float _TempVelocity = (Physics.gravity.y * _xMagnitude * _xMagnitude) / (2 * (_y - Mathf.Tan(_AngleInRadians) * _xMagnitude) * Mathf.Pow(Mathf.Cos(_AngleInRadians), 2));
		_TempVelocity = Mathf.Sqrt(Mathf.Abs(_TempVelocity));
		//bomb.GetComponent<Rigidbody>().AddForce((_fromToXZ + new Vector3(0, 1, 0)) * _TempVelocity, ForceMode.Impulse);
		_rigidbody.velocity = (_fromToXZ.normalized + Vector3.up).normalized * _TempVelocity;

	}
	public void MoveBomb()
	{
		_movingVector = Vector3.MoveTowards(transform.position, _destinationVector + _customDestinationVector, _bombMoveSpeed);
		if ((Vector3.Distance(transform.position, _destinationTransform.position + _customDestinationVector) / _maxDistance) > 0.3f)
		{
			_movingVector.y = _startingHeight + _curve.Evaluate((Vector3.Distance(transform.position, _destinationTransform.position + _customDestinationVector) / _maxDistance)) * _maxHeight;
		}
		if (Vector3.Distance(transform.position, _destinationTransform.position + _customDestinationVector) <= 1f)
		{
			_particlesController.MakeSmallExplosion(transform.position);
			Invoke("killPlayer", 0.1f);
		}
		transform.position = _movingVector;
	}
	private void killPlayer()
	{
		FindObjectOfType<UIController>().SetGradientsAlpha(1, 0);
		FindObjectOfType<MainGameController>().PlayerLose();
		Destroy(this.gameObject);
	}
}
