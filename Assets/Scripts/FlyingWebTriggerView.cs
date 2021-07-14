using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingWebTriggerView : MonoBehaviour
{
    [SerializeField] GameObject _aimCenter;
    [SerializeField] GameObject _aimRound;
    [SerializeField] SpriteRenderer _spriteRenderer;

    float _scalePower = 0;
    Gradient _gradient = new Gradient();
    GradientColorKey[] _gck = new GradientColorKey[3];
    GradientAlphaKey[] _gak = new GradientAlphaKey[3];
    private bool _powerGoingUp = true;
    private float _maxScalePower = 100f;
    private float _minScalePower = 0f;
    private Vector3 _tempScale = Vector3.zero;
    private Transform player = null;
    private void Awake()
    {
        _gck[0].color = Color.red;
        _gck[0].time = 1.0f;
        _gck[1].color = Color.red;
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
        //this.gameObject.tag = "";
        if (player == null)
        { 
            player = FindObjectOfType<PlayerMovement>().transform;
        }
        transform.LookAt(player);
    }

    private void FixedUpdate()
    {
        if (_powerGoingUp)
        {
            if (_scalePower < _maxScalePower)
            {
                _scalePower += 2;
            }
            else
            {
                _powerGoingUp = false;
            }
        }
        else
        {
            if (_scalePower > _minScalePower)
            {
                _scalePower -= 2;
            }
            else
            {
                _powerGoingUp = true;
            }
        }
        _tempScale.x = _tempScale.y = _tempScale.z = (0.9f - _scalePower * 0.0062f);
        _aimRound.transform.localScale = _tempScale;
        _aimCenter.transform.Rotate(0, 0, 1);
        _spriteRenderer.color = _gradient.Evaluate(_scalePower * 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            ParticlesController.Current.MakeStarsExplosion(this.transform.position);
            GameEvents.Current.GetClickFromWebTrigger(this.gameObject);
            //Destroy(this);
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagManager.GetTag(TagType.Wall)))
        {
            transform.position += Vector3.back*0.33f;
            transform.LookAt(player);
        }
    }

    private void OnDestroy()
    {

    }
}
