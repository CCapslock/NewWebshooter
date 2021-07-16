using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    public static GameObject CurrentFlyingTrigger = null;
    public float MaxSpeed;
    public float MaxRotationSpeed;
    [SerializeField] private Animator LeftHandAnimator;
    [SerializeField] private Animator RightHandAnimator;
    [SerializeField] private GameObject FlyingWebTrigger;
    private MovementPoints[] _movementPoints;
    private MainGameController _mainGameController;
    private float RotationSideX;
    private float RotationSideY;
    private float RotationSideZ;
    private float _speed;
    private float _rotationSpeed;
    private int _currentPointNum;
    private int TimesOfRotateX;
    private int TimesOfRotateY;
    private int TimesOfRotateZ;
    private bool _needToMove = false;
    private GameObject resultOfClick;
    private Vector3 _currentStartPosition;
    private void Start()
    {
        _speed = MaxSpeed;
        _mainGameController = FindObjectOfType<MainGameController>();
        _rotationSpeed = MaxRotationSpeed;
        GameEvents.Current.OnCreateNewWebTrigger += CreateFlyingWebTrigger;
        //_currentStartPosition = transform.position;
        
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnCreateNewWebTrigger -= CreateFlyingWebTrigger;
    }

    private float t = 0;
    private bool _tGoingUp;
    private float _flyingAmplitudeDelta = 0.01f;
    private float _goingAmplitudeDelta = 0.06f;
    private float _minAmplitude = 0f;
    private float _minFlyAmplitude = 0f;
    private float _maxAmplitude = 0.5f;
    private float _maxFlyAmplitude = 1.5f;
    private float _currentMinAmplitude;
    private float _currentMaxAmplitude;
    private bool _flyMovement = false;
    private bool _grounded = false;
    private RaycastHit _hit;

    private void SetGrounded(bool value)
    {
        if (_grounded != value)
        {
            _grounded = value;
            //Debug.Log($"Grounded {value}");
        }
    }
    private void FixedUpdate()
    {
        if (_needToMove && _currentPointNum < _movementPoints.Length)
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red, 1.2f);
            if (Physics.Raycast(transform.position, Vector3.down, out _hit, 1.2f))
            {
                if (_hit.collider.CompareTag(TagManager.GetTag(TagType.Wall)))
                {
                    SetGrounded(true);                    
                }
                else
                {
                    SetGrounded(false);                    
                }                
            }
            if (_movementPoints[Mathf.Clamp(_currentPointNum - 1, 0, _movementPoints.Length - 1)].NeedToFly)
            {
                _flyMovement = true;
                _currentMinAmplitude = _minFlyAmplitude;
                _currentMaxAmplitude = _maxFlyAmplitude;
            }
            else
            {
                _flyMovement = false;
                _currentMinAmplitude = _minAmplitude;
                _currentMaxAmplitude = _maxAmplitude;
            }
            if (_tGoingUp)
            {
                if (t < _currentMaxAmplitude)
                {
                    if (_flyMovement)
                    {
                        t += _flyingAmplitudeDelta;
                    }
                    else
                    {
                        t += _goingAmplitudeDelta;
                    }
                }
                else
                {
                    t = _currentMaxAmplitude;
                    _tGoingUp = false;
                }

            }
            else
            {
                if (t > _currentMinAmplitude)
                {
                    if (_flyMovement)
                    {
                        t -= _flyingAmplitudeDelta;
                    }
                    else
                    {
                        t -= _goingAmplitudeDelta;
                    }
                }
                else
                {
                    t = _currentMinAmplitude;
                    _tGoingUp = true;
                }
            }
            if (!_grounded && !_flyMovement)
            {

            }
            else
            { 
                transform.position += Vector3.up *0.1f * t;
            }
            transform.position = Vector3.MoveTowards(transform.position, _movementPoints[_currentPointNum].transform.position, _speed);
            /*t += _speed * Time.deltaTime;
            if (t > 1)
            { 
                t = 1;
            }*/
            //transform.position = Vector3.Lerp(_currentStartPosition, _movementPoints[_currentPointNum].transform.position, t) + Vector3.up * ((-(t * t)) + t) * 10f;

        }
        else
        {
            //_currentStartPosition = transform.position;
            //t = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        AsyncTriggerChecker(other);
    }

    private void CreateFlyingWebTrigger()
    {
        //Debug.Log("SpawnWebTrigger");
        if (FlyingWebTrigger != null)
        {
            CurrentFlyingTrigger = Instantiate(FlyingWebTrigger, transform.position + Vector3.forward * 10f + Vector3.up * 4.5f + Vector3.right * UnityEngine.Random.Range(-2f, 2f) * 0.5f, Quaternion.identity);
        }
    }

    private async void AsyncTriggerChecker(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.GetTag(TagType.MovingPoint)))
        {
            if (_movementPoints[_currentPointNum].NeedToRotate)
            {
                RotatePlayer(_movementPoints[_currentPointNum].RotationVector);
            }
            if (_movementPoints[_currentPointNum].NeedToChangeSpeed)
            {
                _speed = _movementPoints[_currentPointNum].NewSpeed;
            }
            else
            {
                _speed = MaxSpeed;
            }
            if (_movementPoints[_currentPointNum].NeedToFly)
            {
                resultOfClick = await this.GetClickOnWebTrigger();
                //запуск паутины к полученному resultofclick
                RightHandAnimator.SetTrigger("Flying");
                LeftHandAnimator.SetTrigger("FlyingOffhand");
            }
            if (_currentPointNum < _movementPoints.Length && (_movementPoints[_currentPointNum].IsFinalPoint) && _movementPoints[_currentPointNum].Enemyes.Length == 0)
            {
                if (_movementPoints[_currentPointNum].IsBossBattle)
                {
                    _mainGameController.LevelIsEnded(true, true);
                }
                else
                {
                    _mainGameController.LevelIsEnded(true, false);
                }
            }
            if (_movementPoints[_currentPointNum].Enemyes.Length == 0)
            {
                ChangePoint();
            }
            else
            {
                _mainGameController.ActivateEnemyes(_movementPoints[_currentPointNum].Enemyes, _movementPoints[_currentPointNum].NeedToCountEnemy);
                if (_movementPoints[_currentPointNum].NeedToCountEnemy)
                {
                    _needToMove = false;
                }
                else
                {
                    ChangePoint();
                }
                if ((_movementPoints[_currentPointNum].IsFinalPoint))
                {
                    _mainGameController.LevelIsEnded();
                }
            }


            Destroy(other.gameObject);
        }
    }

    private GameObject CallbackForFlyingAnimator() { return resultOfClick; }


    public Task<GameObject> GetClickOnWebTrigger()
    {
        GameEvents.Current.CreateNewWebTrigger();
        var task = new TaskCompletionSource<GameObject>();
        GameEvents.Current.OnGetClickFromWebTrigger += (GameObject obj) =>
        {
            task.SetResult(obj);
        };
        //вызывать по попаданию в вебтриггер        
        return task.Task;
    }

    public void SetMovementPoints(MovementPoints[] movementPoints)
    {
        _movementPoints = movementPoints;
    }
    public void StartMoving()
    {
        _needToMove = true;
    }
    public void ContinueMoving()
    {
        ChangePoint();
        _needToMove = true;
    }
    public void ChangePoint()
    {
        _currentPointNum++;
    }

    #region Rotation
    public void RotatePlayer(Vector3 RotatingVector)
    {
        if (RotatingVector.x != 0)
        {
            if (RotatingVector.x > 0)
                RotationSideX = 1;
            else
                RotationSideX = -1;
            TimesOfRotateX = (int)(ConvertToPositive(RotatingVector.x) / _rotationSpeed);
        }
        else
        {
            RotationSideX = 0;
        }
        if (RotatingVector.y != 0)
        {
            if (RotatingVector.y > 0)
                RotationSideY = 1;
            else
                RotationSideY = -1;
            TimesOfRotateY = (int)(ConvertToPositive(RotatingVector.y) / _rotationSpeed);
        }
        else
        {
            RotationSideY = 0;
        }
        if (RotatingVector.z != 0)
        {
            if (RotatingVector.z > 0)
                RotationSideZ = 1;
            else
                RotationSideZ = -1;
            TimesOfRotateZ = (int)(ConvertToPositive(RotatingVector.z) / _rotationSpeed);
        }
        else
        {
            RotationSideZ = 0;
        }


        int maxnum = 0;
        if (TimesOfRotateX > TimesOfRotateY && TimesOfRotateX > TimesOfRotateY)
        {
            maxnum = TimesOfRotateX;
        }
        if (TimesOfRotateY > TimesOfRotateX && TimesOfRotateY > TimesOfRotateZ)
        {
            maxnum = TimesOfRotateY;
        }
        if (TimesOfRotateZ > TimesOfRotateY && TimesOfRotateZ > TimesOfRotateX)
        {
            maxnum = TimesOfRotateZ;
        }

        StopAllCoroutines();
        StartCoroutine(RotatePlayer(new Vector3(_rotationSpeed * RotationSideX, 0, 0), TimesOfRotateX));
        StartCoroutine(RotatePlayer(new Vector3(0, _rotationSpeed * RotationSideY, 0), TimesOfRotateY));
        StartCoroutine(RotatePlayer(new Vector3(0, 0, _rotationSpeed * RotationSideZ), TimesOfRotateZ));

        /*
        for (int i = 0; i < maxnum; i++)
        {
            if (i <= TimesOfRotateX)
            {
                Invoke("RotateALittleX", 0.015f * i);
            }
            if (i <= TimesOfRotateY)
            {
                Invoke("RotateALittleY", 0.015f * i);
            }
            if (i <= TimesOfRotateZ)
            {
                Invoke("RotateALittleZ", 0.015f * i);
            }
        }*/
    }
    private void RotateALittleX()
    {
        transform.Rotate(_rotationSpeed * RotationSideX, 0, 0);
    }
    private void RotateALittleY()
    {
        transform.Rotate(0, _rotationSpeed * RotationSideY, 0);
    }
    private void RotateALittleZ()
    {
        transform.Rotate(0, 0, _rotationSpeed * RotationSideZ);
    }
    private float ConvertToPositive(float num)
    {
        if (num > 0)
            return num;
        if (num < 0)
            return -num;
        return 0;
    }
    #endregion

    private IEnumerator RotatePlayer(Vector3 rotationVector, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            transform.Rotate(rotationVector);            
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

}

