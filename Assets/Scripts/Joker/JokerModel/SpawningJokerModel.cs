using System.Collections;
using UnityEngine;


public class SpawningJokerModel : BaseJokerModel
{
    private float _spawnColdown;
    private int _spawnAmount;
    private float _lastSpawnTime = 0;

    public SpawningJokerModel(float cooldown, int amount)
    {
        _spawnColdown = cooldown;
        _spawnAmount = amount;
    }

    public override void Execute(JokerView view)
    {
        base.Execute(view);
        if (_spawnAmount > 0)
        {
            if ((Time.time - _lastSpawnTime) > _spawnColdown)
            {
                _lastSpawnTime = Time.time;
                view.Animator.SetTrigger("Attack");
                _spawnAmount--;
            }            
        }
    }
}