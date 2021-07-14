using System.Collections.Generic;
using UnityEngine;

public class JokerView : MonoBehaviour, IBoss
{
    private JokerStates _state = JokerStates.Awaiting;
    private Dictionary<JokerStates, BaseJokerModel> _models = new Dictionary<JokerStates, BaseJokerModel>();
    [SerializeField] private float _spawnCooldown = 1.3f;
    [SerializeField] private int _spawnAmount = 2;
    [SerializeField] private int _spawnPerCast = 2;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _jokerMinion;
    private int _hp;
    private PlayerMovement _player;

    public JokerStates State => _state;
    public Animator Animator => _animator;

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        _hp = _spawnAmount * _spawnPerCast;
        _player = FindObjectOfType<PlayerMovement>();
        MainGameController.BossContainter = this;
        _models.Add(JokerStates.Awaiting, new AwaitingJokerModel());
        _models.Add(JokerStates.Spawning, new SpawningJokerModel(_spawnCooldown, _spawnAmount));
        _models.Add(JokerStates.Stucked, new StuckedJokerModel());
        _models.Add(JokerStates.Waiting, new WaitingJokerModel());
    }

    private void FixedUpdate()
    {
        _models[_state].Execute(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            switch (_state)
            {
                case JokerStates.Waiting:
                    {
                        if (_hp <= 0)
                        {
                            ChangeState(JokerStates.Stucked);
                        }
                        else
                        {
                            ParticlesController.Current.MakeGlow(transform.position);
                        }
                    }
                    break;
                default:
                    {
                        ParticlesController.Current.MakeGlow(transform.position);
                    }
                    break;
            }
        }
    }

    public void GetDamage()
    {
        _hp -= 1;
        if (_hp <= 0)
        {
            ChangeState(JokerStates.Waiting);
        }
    }


    public void AwakeBoss()
    {
        ChangeState(JokerStates.Spawning);
    }

    public void ChangeState(JokerStates state)
    {
        _state = state;
        switch (_state)
        {
            case JokerStates.Waiting:
                {
                    ParticlesController.Current.MakeEvilLaughParticles(transform.position + Vector3.up * 4f);
                    _animator.SetTrigger("Laugh");
                }
                break;
            case JokerStates.Stucked:
                {

                    LevelVictory();
                }
                break;
            default: break;
        }
    }

    public void LevelVictory()
    {
        GetComponent<BossRagdollController>().ThrowEnemy();
        FindObjectOfType<MainGameController>().EnemyBeenDefeated();
    }

    public void SpawnEnemy()
    {
        GameObject obj;
        //Debug.Log($"О сработало (SpawnEnemy from {this.gameObject.name})");
        for (int i = 0; i < _spawnPerCast; i++)
        {
            obj = Instantiate(_jokerMinion, _rightHand.transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.FromToRotation(transform.position, _player.transform.position);
            JokerMinionView Minion = obj.GetComponent<JokerMinionView>();
            Minion.SetPlayer(_player);
            Minion.SetJoker(this);
            Minion.ThrowToPosition();
        }
    }


    public void OnDestroy()
    {
        MainGameController.BossContainter = null;
    }
}
