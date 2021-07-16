using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class WebShooter : MonoBehaviour
{
    public static WebShooter Current;
    public float WebSpeed;
    [Foldout("Settings")]
    public GameObject StuckedWeb;
    [Foldout("Settings")]
    public GameObject ShootingWeb;

    [SerializeField] [Foldout("Settings")] public GameObject ShootingChainingWeb;
    [Foldout("Settings")]
    public Animator RightHandAnimator;
    [Foldout("Settings")]
    public Animator LeftHandAnimator;
    [Foldout("Settings")]
    public Transform RightHandTransform;
    [Foldout("Settings")]
    public Transform LeftHandTransform;

    private Material _webMaterial;
    private GameObject _webObject;
    private List<WebObject> _webObjects;
    private RaycastHit _objectHit;
    private Ray _ray;
    //private Vector3 _rightHandPosition;
    //private Vector3 _leftHandPosition;
    private Vector3 _goalPosition;
    private float _halfOfScreenWidth;
    private int webIterator = 0;
    [SerializeField] private bool _isActivated;

    private void Awake()
    {
        if (Current == null)
        {
            Current = this;
        }
    }

    private void Start()
    {
        //_rightHandPosition = RightHandTransform.position;
        //_leftHandPosition = LeftHandTransform.position;
        _halfOfScreenWidth = Screen.width / 2f;
        _webObjects = new List<WebObject>();
    }
    private void FixedUpdate()
    {
        MoveWeb();
    }
    public void ActivateWebShooter()
    {
        _isActivated = true;
    }
    public void DisactivateWebShooter()
    {
        _isActivated = false;
    }

    public void SetMaterial(Material material)
    {
        _webMaterial = material;
    }

    private IChainable chainableObj;//легаси
    public void ShootStreamWeb(Vector3 mousePosition, float power)///легаси
    {
        if (_isActivated)
        {
            if (CheckTheStreamGoal(mousePosition, out chainableObj))
            {
                chainableObj.ChangeChainPower(power);
            }
        }
    }

    private bool CheckTheStreamGoal(Vector3 mousePosition, out IChainable obj)///легаси
    {
        _ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(_ray, out _objectHit))
        {
            if (_objectHit.collider.TryGetComponent<IChainable>(out obj))
            {
                return true;
            }
        }
        obj = null;
        return false;
    }


    private string _shootingTag = "";
    public void ShootWeb(Vector3 mousePosition)
    {
        if (_isActivated)
        {
            if (CheckTheGoal(mousePosition, out _shootingTag))
            {
                Debug.Log($"Tag >>{_shootingTag}<<");
                _goalPosition.z += 0.1f;

                switch (_shootingTag)
                {
                    case "SimpleEnemy":
                        if (Random.Range(0, 5) > 3)
                        {
                            ShootLeftChainingWeb(_goalPosition);
                        }
                        else
                        {
                            ReleaseShootingWeb(mousePosition, _goalPosition);
                        }
                        break;
                    case "ShieldEnemy":
                        if (Random.Range(0, 5) > 3)
                        {
                            ShootLeftChainingWeb(_goalPosition);
                        }
                        else
                        {
                            ReleaseShootingWeb(mousePosition, _goalPosition);
                        }
                        break;
                    case "ThrowingEnemy":
                        if (Random.Range(0, 5) > 3)
                        {
                            ShootLeftChainingWeb(_goalPosition);
                        }
                        else
                        {
                            ReleaseShootingWeb(mousePosition, _goalPosition);
                        }
                        break;
                    case "DodgeEnemy":
                        if (Random.Range(0, 5) > 3)
                        {
                            ShootLeftChainingWeb(_goalPosition);
                        }
                        else
                        {
                            ReleaseShootingWeb(mousePosition, _goalPosition);
                        }
                        break;
                    case "EnemyPart":
                        if (Random.Range(0, 5) > 3)
                        {
                            ShootLeftChainingWeb(_goalPosition);
                        }
                        else
                        {
                            ReleaseShootingWeb(mousePosition, _goalPosition);
                        }
                        break;
                    default:
                        {
                            ReleaseShootingWeb(mousePosition, _goalPosition);
                            break;
                        }
                }

            }
        }
    }
    private void ReleaseShootingWeb(Vector3 mousePosition, Vector3 pos)
    {
        if (mousePosition.x > _halfOfScreenWidth)
        {
            ShootRightWeb(pos);
        }
        else
        {
            ShootLeftWeb(pos);
        }
    }
    private void ShootLeftChainingWeb(Vector3 pos)
    {
        InstantiateChainingWeb(LeftHandTransform.position, pos);
        LeftHandAnimator.SetTrigger("Shoot");
    }

    private void ShootRightWeb(Vector3 pos)
    {
        //_rightHandPosition = RightHandTransform.position;
        InstantiateWeb(RightHandTransform.position, pos);
        RightHandAnimator.SetTrigger("Shoot");
    }
    private void ShootLeftWeb(Vector3 pos)
    {
        //_leftHandPosition = LeftHandTransform.position;
        InstantiateWeb(LeftHandTransform.position, pos);
        LeftHandAnimator.SetTrigger("Shoot");
    }


    private void InstantiateChainingWeb(Vector3 Pos, Vector3 _position)
    {
        if (ShootingChainingWeb != null)
        {
            _webObject = Instantiate(ShootingChainingWeb, Pos, Quaternion.identity);
            /*if (_webMaterial != null)
            {
                
                _webObject.GetComponentInChildren<TrailRenderer>().material = _webMaterial;
                _webObject.GetComponentInChildren<MeshRenderer>().material = _webMaterial;

            }*/
            _webObjects.Add(new WebObject()
            {
                WebGameObject = _webObject,
                GoalPosition = _position,
                slowed = true
            });
        }
        else
        {
            Debug.LogWarning($" ShootingChainingWeb is not set");
        }
    }

    private void InstantiateWeb(Vector3 Pos, Vector3 _position)
    {
        _webObject = Instantiate(ShootingWeb, Pos, Quaternion.identity);
        if (_webMaterial != null)
        {
            _webObject.GetComponentInChildren<TrailRenderer>().material = _webMaterial;
            _webObject.GetComponentInChildren<MeshRenderer>().material = _webMaterial;

        }
        _webObjects.Add(new WebObject()
        {
            WebGameObject = _webObject,
            GoalPosition = _position,
            slowed = false
        });
    }

    private bool CheckTheGoal(Vector3 mousePosition, out string tag)
    {
        _ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(_ray, out _objectHit))
        {
            if (_objectHit.rigidbody != null)
            {
                if (_objectHit.transform.CompareTag("FinalLevelTrigger"))
                {
                    if (MainGameController.BossContainter is FinalZoneView)
                    {
                        (MainGameController.BossContainter as FinalZoneView).TriggerCounted();
                    }
                }
                else if (_objectHit.transform.CompareTag("FlyingWebTrigger"))
                {
                    tag = "FlyingWebTrigger";
                    GameEvents.Current.GetClickFromWebTrigger(_objectHit.transform.gameObject);
                    return false;
                }
                _goalPosition = _objectHit.point;
                tag = _objectHit.collider.tag;
                return true;
            }
        }
        _goalPosition = Camera.main.transform.position + new Vector3(0, 0, 100f);
        tag = "";
        return true;
    }
    private void MoveWeb()
    {
        if (_webObjects.Count > 0)
        {
            webIterator = 0;
            foreach (WebObject i in _webObjects)
            {
                if (i.WebGameObject != null)
                {
                    if (i.slowed)
                    {
                        i.WebGameObject.transform.position = Vector3.MoveTowards(i.WebGameObject.transform.position, i.GoalPosition, WebSpeed * 0.15f);
                    }
                    else
                    {
                        i.WebGameObject.transform.position = Vector3.MoveTowards(i.WebGameObject.transform.position, i.GoalPosition, WebSpeed);
                    }
                }
                else
                {
                    webIterator++;
                }
            }
            /*
            if (webIterator > 5)
            {
                if (webIterator == _webObjects.Count)
                {
                    _webObjects = new List<WebObject>();
                }
            }*/
        }
    }
}

public class WebObject
{
    public GameObject WebGameObject;
    public Vector3 GoalPosition;
    public bool slowed;
}
