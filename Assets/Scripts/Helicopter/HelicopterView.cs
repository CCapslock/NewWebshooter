using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterView : MonoBehaviour, IBoss
{
    private HelicopterStates _state;
    private PlayerMovement _player;
    private Dictionary<HelicopterStates, BaseHelicopterState> _model = new Dictionary<HelicopterStates, BaseHelicopterState>();
    [SerializeField] private Transform _centralFlyingPoint;
    [SerializeField] private Transform _goblin;
    [SerializeField] private GameObject _bomb;
    
    #region Goblin
    [SerializeField] private Transform _goblinStartPosition;
    [SerializeField] private Transform _goblinEndPosition;
    #endregion

    public Transform GoblinStart => _goblinStartPosition;
    public Transform GoblinEnd => _goblinEndPosition;

    public Transform CentralFlyingPoint => _centralFlyingPoint;

    public void Awake()
    {        
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
        if (collision.gameObject.CompareTag("HelicopterBomb"))
        {
            collision.gameObject.GetComponent<Bomb>().DetonateBomb();
            if (_state != HelicopterStates.Falling)
            {
                ChangeState(HelicopterStates.Falling);
            }

        }
        else if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            if ((_state == HelicopterStates.SlowTime))
            {
                ReleaseNewWeb();
            }
        }
    }

    public void ReleaseNewWeb()
    { 
    //бросить рейкаст в стороны, в точке соприкосновнеия с wall tag заспавнить центр паутины, скейлом присобачить его в вертолету
    }

    public void FixedUpdate()
    {
        _model[_state].Execute(this);
    }

    public void ChangeState(HelicopterStates state)
    {
        _state = state;
        switch (_state)
        {
            case HelicopterStates.Falling:
                { 

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
}
