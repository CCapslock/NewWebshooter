using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalZoneView : MonoBehaviour, IBoss
{
    //[SerializeField] private Animator[] _flagstocks;
    //[SerializeField] private WebConnectScript _leftSlinger;
    //[SerializeField] private WebConnectScript _rightSlinger;
    //[SerializeField] private Transform _leftFlagstockEnd;
    //[SerializeField] private Transform _rightFlagstockEnd;

    //[SerializeField] private GameObject _man;    
    [SerializeField] private Transform _leftArm;
    [SerializeField] private Transform _rightArm;
    [SerializeField] private CinemachineVirtualCamera _virtCameraGlobal;
    [SerializeField] private CinemachineVirtualCamera _virtCameraMan;

    [SerializeField] private Transform _startLine;
    [SerializeField] private Transform _endLine;
    [SerializeField] private FinalFlyingMan _man;
    [SerializeField] private Transform _manHead;

    [SerializeField] private GameObject _aimCenter;
    [SerializeField] private GameObject _aimRound;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector3 _tempScale;
    private Vector3 destination;


    private float _flagstoksBlend = 0;

    private bool _awaked = false;
    private bool _throwed = false;
    private bool _grounded = false;

    private float _throwingPower = 100;// отдаем это значение в fill ui
    [SerializeField] private float _minThrowingPower = 0;
    [SerializeField] private float _maxThrowingPower = 100;
    private bool _powerGoingUp = true;
    public static float Multiplier = 2;
    
    Gradient _gradient = new Gradient();
    GradientColorKey[] _gck = new GradientColorKey[3];
    GradientAlphaKey[] _gak = new GradientAlphaKey[3];

    private void Awake()
    {
        MainGameController.BossContainter = this;
        _aimCenter.SetActive(false);
        /*if (_flagstocks.Length < 1)
        {
            Debug.LogError("Не назначены флагштоки");
        }*/        
        _gck[0].color = Color.green;
        _gck[0].time = 1.0f;
        _gck[1].color = Color.yellow;
        _gck[1].time = 0.5f;
        _gck[2].color = Color.red;
        _gck[2].time = 0.0f;

        _gak[0].alpha = 1.0f;
        _gak[0].time = 0.0f;
        _gak[1].alpha = 1.0f;
        _gak[2].time = 0.5f;
        _gak[2].alpha = 1.0f;
        _gak[2].time = 1f;
        _gradient.SetKeys(_gck, _gak);
    }

    private void FixedUpdate()
    {
        if (!_awaked)
        {
            /*for (int i = 0; i < _flagstocks.Length; i++)
            {
                _flagstocks[i].SetFloat("Blend", _flagstoksBlend);
            }*/
        }
        else if (_awaked && !_throwed)
        {
            if (_powerGoingUp)
            {
                if (_throwingPower < _maxThrowingPower)
                {
                    _throwingPower += 2;
                }
                else
                {
                    _powerGoingUp = false;
                }
            }
            else
            {
                if (_throwingPower > _minThrowingPower)
                {
                    _throwingPower -= 2;
                }
                else
                {
                    _powerGoingUp = true;
                }
            }
            _tempScale.x = _tempScale.y = _tempScale.z = (0.9f - _throwingPower * 0.0062f);

            _aimRound.transform.localScale = _tempScale;
            _aimCenter.transform.Rotate(0, 0, 1);
            _spriteRenderer.color = _gradient.Evaluate(_throwingPower * 0.01f);
            _aimCenter.transform.position = _manHead.position + Vector3.back + Vector3.down * 0.3f;
            //изменение цвета
        }
        else if (_awaked && _throwed)
        {

        }
    }

    public void AwakeBoss()
    {
        _awaked = true;
        //Смена Приоритета камеры на чела
        _aimCenter.SetActive(true);
        _aimCenter.transform.LookAt(Camera.main.transform);
        _aimCenter.transform.position = _manHead.position + Vector3.back + Vector3.down*0.3f;
        //_leftSlinger.ConnectToHand(_leftArm);
        //_rightSlinger.ConnectToHand(_rightArm);


    }


    public void SendScale()
    {
        if (!_grounded)
        {
            _grounded = true;
            //Multiplier = Mathf.Round(14f + (_throwingPower * 0.36f))*0.1f;
            Multiplier = (float)System.Math.Round(2f + (_throwingPower * 0.035f),1);


            Debug.Log($"{Multiplier} Distance");
            Invoke("LevelVictory", 1f);
        }
    }

    private void LevelVictory()
    {
        FindObjectOfType<MainGameController>().EnemyBeenDefeated();
    }
    public void TriggerCounted()
    {

        _virtCameraGlobal.Priority = 11;
    }

    public void ThrowMan()
    {
        ParticlesController.Current.MakeStarsExplosion(_man.transform.position + Vector3.up);
        if (_awaked && !_throwed)
        {
            Multiplier = (float)System.Math.Round(1.4f + (_throwingPower * 0.035f), 1); //- (_throwingPower * 0.036f) % 1f;
            Debug.Log($"{Multiplier} Distance");
            _aimCenter.SetActive(false);
            _virtCameraMan.Priority = 12;
            _throwed = true;
            SetManRagdollState(true);

            _startLine.position = Vector3.Lerp(_startLine.position, _endLine.position, _throwingPower * 0.01f);
            
            var _AngleInRadians = 45 * Mathf.PI / 180;
            var _fromTo = _startLine.position - _man.transform.position;
            Vector3 _fromToXZ = new Vector3(_fromTo.x, 0f, _fromTo.z);

            var _xMagnitude = _fromToXZ.magnitude;
            var _y = _fromTo.y;

            var _TempVelocity = (Physics.gravity.y * _xMagnitude * _xMagnitude) / (2 * (_y - Mathf.Tan(_AngleInRadians) * _xMagnitude) * Mathf.Pow(Mathf.Cos(_AngleInRadians), 2));
            _TempVelocity = Mathf.Sqrt(Mathf.Abs(_TempVelocity));
            //bomb.GetComponent<Rigidbody>().AddForce((_fromToXZ + new Vector3(0, 1, 0)) * _TempVelocity, ForceMode.Impulse);
            //_man.ThrowableRigidbody.velocity = (_fromToXZ.normalized + Vector3.up).normalized * _TempVelocity * 20;
            _man.SetRigidBodiesVelocioty((_fromToXZ.normalized + Vector3.up).normalized * _TempVelocity);
        }
    }

    private void SetAnimatorBlendValue(float value)
    {
        /*
        for (int i = 0; i < _flagstocks.Length; i++)
        {
            _flagstocks[i].SetFloat("Blend", value);
        }
        */
    }

    private void SetManRagdollState(bool state)
    {
        _man.SetRagdollState(state);
    }

    public void ReleseSlinger()
    {

    }

    public void OnDestroy()
    {
        MainGameController.BossContainter = null;
    }
}