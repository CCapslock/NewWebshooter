using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueInputController : MonoBehaviour
{
    private TrueInputEvents _inputEventsHandler = new TrueInputEvents();

    // Start is called before the first frame update
    private void Awake()
    {

    }

    private Touch _currentTouch;
    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _currentTouch = Input.GetTouch(0);
            switch (_currentTouch.phase)
            {
                case TouchPhase.Began:
                    {
                        TrueInputEvents.Current.TouchBegan(_currentTouch.position);
                        break;
                    }
                case TouchPhase.Canceled:
                    {
                        TrueInputEvents.Current.TouchCancelled();
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        TrueInputEvents.Current.TouchEnded(_currentTouch.position);
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        TrueInputEvents.Current.TouchMoved(_currentTouch.position);
                        break;
                    }
                case TouchPhase.Stationary:
                    {
                        TrueInputEvents.Current.TouchStationary(_currentTouch.position);
                        break;
                    }
            }
        }
    }
}

public class TrueInputEvents
{
    public static TrueInputEvents Current;
    public TrueInputEvents()
    {
        if (Current == null)
        {
            Current = this;
        }
    }

    public event Action<Vector2> OnTouchBegan;
    public void TouchBegan(Vector2 position)
    {
        OnTouchBegan?.Invoke(position);
    }

    public event Action OnTouchCancelled;
    public void TouchCancelled()
    {
        OnTouchCancelled?.Invoke();
    }
    public event Action<Vector2> OnTouchEnded;
    public void TouchEnded(Vector2 position)
    {
        OnTouchEnded?.Invoke(position);
    }
    public event Action<Vector2> OnTouchMoved;
    public void TouchMoved(Vector2 position)
    {
        OnTouchMoved?.Invoke(position);
    }
    public event Action<Vector2> OnTouchStationary;
    public void TouchStationary(Vector2 position)
    {
        OnTouchStationary?.Invoke(position);
    }




}
