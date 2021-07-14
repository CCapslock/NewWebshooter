using UnityEngine;

public class InputController : MonoBehaviour
{
    //следит за пальцем на экране
    private WebShooter _webShooter;
    [HideInInspector] public Vector3 TouchPosition;
    [HideInInspector] public bool DragingStarted = false;
    public bool UseMouse = false;
    private void Start()
    {
        _webShooter = FindObjectOfType<WebShooter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //GameEvents.Current.GetClickFromWebTrigger(new GameObject());
            DragingStarted = true;
            TouchPosition = Input.mousePosition;
            _webShooter.ShootWeb(TouchPosition);
        }
        else
        {
            DragingStarted = false;
        }
    }


    /*
    private float _iterator = 0;
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _iterator++;
            if (_iterator > 15)
            {
                _iterator = 15;
                _webShooter?.ShootStreamWeb(Input.mousePosition, _iterator - 15);
            }
        }
        else
        {
            _iterator = 0;
        }
    }*/
}
