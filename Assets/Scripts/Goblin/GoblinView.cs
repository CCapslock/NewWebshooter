using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GoblinView : MonoBehaviour
{    
    private BaseGoblinModel _currentModel;
    private Dictionary<GoblinState,BaseGoblinModel> _models;
    private GoblinState _newState;
    private GoblinState _currentState;
    private PlayerMovement _player;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _finalAwakeningTransform;
    [SerializeField] private Transform _startAwakeningTransform;
    [SerializeField] private float _smothAwakChangeDistance;

    [SerializeField] private Transform[] _strifePoints;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private Animator _animator;


    #region ParaThrowBomb
    private Vector3 _fromTo;
    private Vector3 _fromToXZ;
    private float _xMagnitude; //fromToXZ magnitude
    private float _y; //fromto.y
    private float _AngleInRadians;
    private float _TempVelocity;
    #endregion

    public GoblinState State => _currentState;
    public Transform StartAwakTransform => _startAwakeningTransform;
    public Transform FinalAwakTransform => _finalAwakeningTransform;
    public float SmothAwakChangeDistance => _smothAwakChangeDistance;
    public Transform[] StrifePoints => _strifePoints;
    public float AttackCooldown => _attackCooldown;
    public Animator MainAnimator => _animator;
    public PlayerMovement Player => _player;
    private void Awake()
    {
        _newState = GoblinState.Awakening;
        transform.position = _startAwakeningTransform.position;
        


       _models = new Dictionary<GoblinState, BaseGoblinModel>();
        _models.Add(GoblinState.Awakening, new AwakeGoblinModel());
        _models.Add(GoblinState.Idle, new IdleGoblinModel());
        _models.Add(GoblinState.Dead, new DeadGoblinModel());
        _currentModel = _models[GoblinState.Awakening];

        _player = FindObjectOfType<PlayerMovement>();
        ChangeState(_newState);
    }

    private void Start()
    {
        GameEvents.Current.OnThrowingBomb += ThrowBomb;
    }
    private void FixedUpdate()
    {
        if (_newState != _currentState)
        {
            _currentState = _newState;
            _currentModel = _models[_currentState];
        }        
        _currentModel.Execute(this);
    }

    public void ChangeState(GoblinState state)
    {
        _newState = state;
       
    }

    public void ThrowBomb(GameObject bomb)
    {
        _AngleInRadians = 30 * Mathf.PI / 180;
        _fromTo = _player.transform.position - bomb.transform.position;
        _fromToXZ = new Vector3(_fromTo.x, 0f, _fromTo.z);

        _xMagnitude = _fromToXZ.magnitude;
        _y = _fromTo.y;

        _TempVelocity = (Physics.gravity.y * _xMagnitude * _xMagnitude) / (2 * (_y - Mathf.Tan(_AngleInRadians) * _xMagnitude) * Mathf.Pow(Mathf.Cos(_AngleInRadians), 2));
        _TempVelocity = Mathf.Sqrt(Mathf.Abs(_TempVelocity));
        //bomb.GetComponent<Rigidbody>().AddForce((_fromToXZ + new Vector3(0, 1, 0)) * _TempVelocity, ForceMode.Impulse);
        bomb.GetComponent<Rigidbody>().velocity = (_fromToXZ.normalized+Vector3.up).normalized *_TempVelocity;




    }
}
