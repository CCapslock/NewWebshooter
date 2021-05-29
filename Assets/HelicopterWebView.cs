using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterWebView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _webStart;
    [SerializeField] private GameObject _webEnd;
    private HelicopterView _helicopter;
    private Vector3 _hitPoint;
    private bool _helicopterSetted = false;
    private bool _webFixed = false;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_helicopterSetted)
        {
            if (!_webFixed)
            {
                _webEnd.transform.position = Vector3.MoveTowards(_webEnd.transform.position, _helicopter.transform.position, 3f);
                if ((_webEnd.transform.position - _helicopter.transform.position).magnitude < 2f)
                {
                    _webFixed = true;
                }
            }
            else
            {
                _webEnd.transform.position = _helicopter.transform.position;
            }
        }

    }

    public void SetHelicopter(HelicopterView view, Vector3 hitpoint)
    {
        _helicopter = view;
        _hitPoint = hitpoint;
        _helicopterSetted = true;
        _webStart.transform.LookAt(_helicopter.transform.position);
    }
}
