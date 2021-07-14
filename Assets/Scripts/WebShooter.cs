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
    private Vector3 _rightHandPosition;
    private Vector3 _leftHandPosition;
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
        _rightHandPosition = RightHandTransform.position;
        _leftHandPosition = LeftHandTransform.position;
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

    private IChainable chainableObj;
    public void ShootStreamWeb(Vector3 mousePosition, float power)
    {        
        if (_isActivated)
        {
            if (CheckTheStreamGoal(mousePosition, out chainableObj))
            {
                chainableObj.ChangeChainPower(power);
            }
        }
    }

    private bool CheckTheStreamGoal(Vector3 mousePosition, out IChainable obj)
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

    public void ShootWeb(Vector3 mousePosition)
    {
        if (_isActivated)
        {
            if (!CheckTheGoal(mousePosition))
            {
                return;
            }
            else
            {
                _goalPosition.z += 0.1f;
                if (mousePosition.x > _halfOfScreenWidth)
                {
                    _rightHandPosition = RightHandTransform.position;
                    InstantiateWeb(RightHandTransform.position, _goalPosition);
                    /*_webObject = Instantiate(ShootingWeb, new Vector3(_rightHandPosition.x, _rightHandPosition.y, _rightHandPosition.z), Quaternion.identity);
                    _webObjects.Add(new WebObject()
                    {
                        WebGameObject = _webObject,
                        GoalPosition = _goalPosition
                    });*/
                    RightHandAnimator.SetTrigger("Shoot");
                }
                else
                {
                    _leftHandPosition = LeftHandTransform.position;
                    InstantiateWeb(LeftHandTransform.position, _goalPosition);
                    /*_webObject = Instantiate(ShootingWeb, new Vector3(_leftHandPosition.x, _leftHandPosition.y, _leftHandPosition.z), Quaternion.identity);
                    _webObjects.Add(new WebObject()
                    {
                        WebGameObject = _webObject,
                        GoalPosition = _goalPosition
                    });*/
                    LeftHandAnimator.SetTrigger("Shoot");
                }
            };
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
            GoalPosition = _position
        });
    }

    private bool CheckTheGoal(Vector3 mousePosition)
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
                    GameEvents.Current.GetClickFromWebTrigger(_objectHit.transform.gameObject);                    
                    return false;
                }
                _goalPosition = _objectHit.point;
                return true;
            }
        }
        _goalPosition = Camera.main.transform.position + new Vector3(0, 0, 100f);
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
                    i.WebGameObject.transform.position = Vector3.MoveTowards(i.WebGameObject.transform.position, i.GoalPosition, WebSpeed);
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
}
