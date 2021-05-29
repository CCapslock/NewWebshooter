using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterView : MonoBehaviour, IBoss
{
    private HelicopterStates _state = HelicopterStates.Await;
    private PlayerMovement _player;
    private Dictionary<HelicopterStates, BaseHelicopterState> _model = new Dictionary<HelicopterStates, BaseHelicopterState>();
    [SerializeField] private Transform _centralFlyingPoint;
    [SerializeField] private Transform _goblin;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _lopasti;
    [SerializeField] private GameObject _backLopasti;
    [SerializeField] private Transform _playerMovementPoint;

    #region Goblin
    [SerializeField] private Transform _goblinStartPosition;
    [SerializeField] private Transform _goblinEndPosition;
    #endregion
    #region SlowTime
    [SerializeField]private float _slowTimeFallingSpeed = 0.05f;
    [SerializeField]private float _slowValuePerWeb = 0.002f;
    [SerializeField]private GameObject _web;
    private int _websCount = 0;//???
    #endregion

    public Transform GoblinStart => _goblinStartPosition;
    public Transform GoblinEnd => _goblinEndPosition;
    public float SlowTime => _slowTimeFallingSpeed;
    public Transform CentralFlyingPoint => _centralFlyingPoint;
    public GameObject Lopasti => _lopasti;
    public GameObject BackLopasti => _backLopasti;
    public PlayerMovement Player => _player;
    public Transform PlayerMovementPoint => _playerMovementPoint;
    public void Awake()
    {
        MainGameController.BossContainter = this;
        _websCount = 0;
        _player = FindObjectOfType<PlayerMovement>();
        _model.Add(HelicopterStates.Await, new AwaitHelicopterState());
        _model.Add(HelicopterStates.Falling, new FallingHelicopterState());
        _model.Add(HelicopterStates.SlowTime, new SlowTimeHelicopterState());
        _model.Add(HelicopterStates.Attacked, new AttackedHelicopterState(_goblin, GoblinStart, _bomb));
    }

    public void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            collision.gameObject.GetComponent<Bomb>().DetonateBomb();
            if (_state != HelicopterStates.Falling)
            {
                ChangeState(HelicopterStates.Falling);
            }

        }
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            if (_state == HelicopterStates.SlowTime)
            {
                ReleaseNewWeb();
                _slowTimeFallingSpeed -= _slowValuePerWeb;
                _websCount++;
                if (_slowTimeFallingSpeed < 0)
                {
                    _slowTimeFallingSpeed = 0;
                    PlayerVictory();
                }
            }
        }
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Bottom)))
        {
            PlayerLose();
        }
    }

    

    public void ReleaseNewWeb()
    {
        //бросить рейкаст в стороны, в точке соприкосновнеи€ с wall tag 
        //заспавнить центр паутины, скейлом присобачить его в вертолету
        RaycastHit hit;
        GameObject obj;
        HelicopterWebView _webView;
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, ~(1 << 8)))
        {
            if (hit.collider.CompareTag(TagManager.GetTag(TagType.Wall)))
            {
                obj = Instantiate(_web, hit.point, Quaternion.identity);
                _webView = obj.GetComponent<HelicopterWebView>();
                _webView.SetHelicopter(this, hit.point);
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 3f);
            //Debug.Log("Did Hit");            
        }

        if (Physics.Raycast(transform.position, direction * -1f, out hit, Mathf.Infinity, ~(1<<8)))
        {
            if (hit.collider.CompareTag(TagManager.GetTag(TagType.Wall)))
            {
                obj = Instantiate(_web, hit.point, Quaternion.identity);
                _webView = obj.GetComponent<HelicopterWebView>();
                _webView.SetHelicopter(this, hit.point);
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow,3f);
            //Debug.Log("Did Hit");
        }
    }

    public void PlayerVictory()
    {
        MainGameController.BossContainter = null;
        FindObjectOfType<MainGameController>().EnemyBeenDefeated();
        //≈ндгейм
    }

    public void PlayerLose()
    {
        FindObjectOfType<MainGameController>().PlayerLose();
    }

    public void FixedUpdate()
    {
        _model[_state].Execute(this);
        if (transform.localPosition.y < -35)
        {
            Invoke("PlayerLose", 0.4f);
            ParticlesController.Current.MakeSmallExplosion(transform.position);
            ParticlesController.Current.MakeSmallExplosion(transform.position + Vector3.forward + Vector3.down);
            ParticlesController.Current.MakeSmallExplosion(transform.position + Vector3.forward*-1 + Vector3.down);

        }
    }

    public void ChangeState(HelicopterStates state)
    {
        _state = state;
        switch (_state)
        {
            case HelicopterStates.Falling:
                {
                    _goblin.LookAt(_player.transform);
                    ParticlesController.Current.MakeEvilLaughParticles(_goblin.position);
                    Destroy(_goblin.gameObject, 0.5f);
                }
                break;
            default: 
                break;
        }
    }

    public void AskingPlayerLookAtMe()
    {
        _player.transform.LookAt(this.transform);
    }

    public void AwakeBoss()
    {
        ChangeState(HelicopterStates.Attacked);
    }

    private void OnDestroy()
    {
        
    }
}
