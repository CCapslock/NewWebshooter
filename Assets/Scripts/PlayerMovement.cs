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
    private void Start()
    {
        _speed = MaxSpeed;
        _mainGameController = FindObjectOfType<MainGameController>();
        _rotationSpeed = MaxRotationSpeed;
        GameEvents.Current.OnCreateNewWebTrigger += CreateFlyingWebTrigger;
       
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnCreateNewWebTrigger -= CreateFlyingWebTrigger;
    }

    private void FixedUpdate()
    {
        if (_needToMove && _currentPointNum < _movementPoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, _movementPoints[_currentPointNum].transform.position, _speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        AsyncTriggerChecker(other);
    }

    private void CreateFlyingWebTrigger()
    {
        Debug.Log("SpawnWebTrigger");
        if (FlyingWebTrigger != null)
        {
            CurrentFlyingTrigger = Instantiate(FlyingWebTrigger, transform.position + Vector3.forward * 10f + Vector3.up * 4.5f + Vector3.right * UnityEngine.Random.Range(-2f, 2f)*0.5f, Quaternion.identity);
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
        GameEvents.Current.OnGetClickFromWebTrigger += (GameObject obj) => { task.SetResult(obj); };
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
            yield return new WaitForEndOfFrame();
        }
        yield break;        
    }

}

