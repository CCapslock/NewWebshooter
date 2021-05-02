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
    private Vector3 fromTo;
    private Vector3 fromToXZ;
    private float xMagnitude; //fromToXZ magnitude
    private float y; //fromto.y
    private float AngleInRadians = 45 * Mathf.PI / 180;
    private float TempVelocity;
    private float _g = Physics.gravity.y;
    #endregion

    public GoblinState State => _currentState;
    public Transform StartAwakTransform => _startAwakeningTransform;
    public Transform FinalAwakTransform => _finalAwakeningTransform;
    public float SmothAwakChangeDistance => _smothAwakChangeDistance;
    public Transform[] StrifePoints => _strifePoints;
    public float AttackCooldown => _attackCooldown;
    public Animator MainAnimator => _animator;
    private void Awake()
    {
        _newState = GoblinState.Awakening;
        transform.position = _startAwakeningTransform.position;



        _models = new Dictionary<GoblinState, BaseGoblinModel>();
        _models.Add(GoblinState.Awakening, new AwakeGoblinModel());
        _models.Add(GoblinState.Idle, new IdleGoblinModel());
        _models.Add(GoblinState.Dead, new DeadGoblinModel());


        _player = FindObjectOfType<PlayerMovement>();
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
        
        //;
        fromTo = _player.transform.position - bomb.transform.position;
        fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        xMagnitude = fromToXZ.magnitude;
        y = fromTo.y;

        TempVelocity = (_g * xMagnitude * xMagnitude) / (2 * (y - Mathf.Tan(AngleInRadians) * xMagnitude) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        TempVelocity = Mathf.Sqrt(Mathf.Abs(TempVelocity));
        bomb.transform.parent = null;
        bomb.GetComponent<Rigidbody>().AddForce((fromToXZ + new Vector3(0, 1, 0)) * TempVelocity, ForceMode.Acceleration);

    }
}
