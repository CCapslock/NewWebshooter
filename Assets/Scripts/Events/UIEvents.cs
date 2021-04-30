using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Current = this;
    }

    #region General events
    public Action OnButtonLevelStart;
    public void ButtonLevelStart()
    {
        OnButtonLevelStart?.Invoke();
    }

    public Action OnButtonShop;
    public void ButtonShop()
    {
        OnButtonShop?.Invoke();
    }

    public Action OnButtonMainMenu;
    public void ButtonMainMenu()
    {
        OnButtonMainMenu?.Invoke();
    }

    public Action OnButtonPause;
    public void ButtonPause()
    {
        OnButtonPause?.Invoke();
    }

    public Action OnButtonResume;
    public void ButtonResume()
    {
        OnButtonResume?.Invoke();
    }

    public Action OnButtonRestart;
    public void ButtonRestart()
    {
        OnButtonRestart?.Invoke();
    }

    public Action OnButtonNextLevel;
    public void ButtonNextLevel()
    {
        OnButtonNextLevel?.Invoke();
    }
    #endregion

    #region Shop events
    
    #endregion

    #region Advertise events

    #endregion
}