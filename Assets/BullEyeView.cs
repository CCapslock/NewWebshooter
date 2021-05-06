using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullEyeView : MonoBehaviour
{
    private GoblinView _goblin;
    private Vector3 _basePosition;
    private Vector3 _spawnPosition;
    private bool _notInPosition = true;
    private float _smooth = 0f;
    private float _maxSmooth = 1f;
    
    public void SetGoblinView(GoblinView goblin)
    {
        _goblin = goblin;
        
    }

    public void Awake()
    {
        _basePosition = transform.position;
        _spawnPosition = _basePosition - Vector3.down * 10;
        transform.position = _spawnPosition;
    }

    public void FixedUpdate()
    {
        if (_notInPosition)
        {            
            _smooth += 0.33f;
            if (_smooth > 1)
            {
                _smooth = 1;
                _notInPosition = false;
                return;
            }                
            transform.position = Vector3.Lerp(_spawnPosition, _basePosition, _smooth);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            _goblin.TakeDamage();
            ParticlesController.Current.MakeStarsExplosionExplosion(transform.position);
            this.gameObject.SetActive(false);
       
        }
    }
}
