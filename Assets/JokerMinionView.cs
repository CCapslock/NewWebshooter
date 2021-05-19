using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerMinionView : MonoBehaviour
{
    private Vector3 _temporalVector3;

    private PlayerMovement _player;
    private JokerView _joker;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private bool _runToPlayer = false;
    

    public Animator Animator => _animator;
    public Rigidbody Rigidbody => _rigidbody;

    public void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        /*collision.gameObject.CompareTag(TagManager.GetTag(TagType.Buildings1)) ||
        collision.gameObject.CompareTag(TagManager.GetTag(TagType.Buildings2)) ||*/
        if (!_runToPlayer)
        {
            if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Wall)))
            {
                _runToPlayer = true;
                ParticlesController.Current.MakeMagicExplosion(transform.position);
                _animator.SetTrigger("Land");
            }
        }
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            //Заменить на рэгдолл при желании
            Detonate();
        }
    }

    private void Detonate()
    {
        ParticlesController.Current.MakeMagicExplosion(transform.position);
        _joker.GetDamage();
        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        _temporalVector3.x = _player.transform.position.x;// - transform.position.x;
        _temporalVector3.y = transform.position.y;
        _temporalVector3.z = _player.transform.position.z; //- transform.position.z;
        transform.LookAt(_temporalVector3);
        if (_runToPlayer)
        {
            if ((transform.position - _player.transform.position).magnitude > 1.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _movementSpeed * Time.deltaTime);
            }
            else
            {
                FindObjectOfType<HealthController>().GetHitFromBomb();
                Detonate();
                               
            }
        }
    }

    public void SetPlayer(PlayerMovement player)
    {
        _player = player;
    }

    public void SetJoker(JokerView joker)
    {
        _joker = joker;
    }

    public void ThrowToPosition()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;

        _temporalVector3.x = _joker.transform.position.x + Random.Range(-1.5f, 1.5f);
        _temporalVector3.y = _joker.transform.position.y;
        _temporalVector3.z = _joker.transform.position.z - 3f;

        var _AngleInRadians = 45 * Mathf.PI / 180;
        var _fromTo = _temporalVector3 - transform.position;
        Vector3 _fromToXZ = new Vector3(_fromTo.x, 0f, _fromTo.z);

        var _xMagnitude = _fromToXZ.magnitude;
        var _y = _fromTo.y;

        var _TempVelocity = (Physics.gravity.y * _xMagnitude * _xMagnitude) / (2 * (_y - Mathf.Tan(_AngleInRadians) * _xMagnitude) * Mathf.Pow(Mathf.Cos(_AngleInRadians), 2));
        _TempVelocity = Mathf.Sqrt(Mathf.Abs(_TempVelocity));
        //bomb.GetComponent<Rigidbody>().AddForce((_fromToXZ + new Vector3(0, 1, 0)) * _TempVelocity, ForceMode.Impulse);
        Rigidbody.velocity = (_fromToXZ.normalized + Vector3.up).normalized * _TempVelocity;
    }
}
