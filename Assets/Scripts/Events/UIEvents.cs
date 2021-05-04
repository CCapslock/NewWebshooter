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
    public Action<GlovesSkinModel> OnButtonSelectSkinGloves;
    public void ButtonSelectSkinGloves(GlovesSkinModel skin)
    {
        OnButtonSelectSkinGloves?.Invoke(skin);
    }

    public Action<GlovesSkinModel> OnButtonBuySkinGloves;
    public void ButtonBuySkinGloves(GlovesSkinModel skin)
    {
        OnButtonBuySkinGloves?.Invoke(skin);
    }

    public Action<WebSkinModel> OnButtonSelectSkinNet;
    public void ButtonSelectSkinNet(WebSkinModel skin)
    {
        OnButtonSelectSkinNet?.Invoke(skin);
    }

    public Action<WebSkinModel> OnButtonBuySkinNet;
    public void ButtonBuySkinNet(WebSkinModel skin)
    {
        OnButtonBuySkinNet?.Invoke(skin);
    }
    #endregion

    #region Advertise events
    public Action<GlovesSkinModel> OnButtonGetSkinGloves;
    public void ButtonGetSkinGloves(GlovesSkinModel skin)
    {
        OnButtonGetSkinGloves?.Invoke(skin);
    }

    public Action<WebSkinModel> OnButtonGetSkinNet;
    public void ButtonGetSkinNet(WebSkinModel skin)
    {
        OnButtonGetSkinNet?.Invoke(skin);
    }

    public Action OnButtonGetMoreCoins;
    public void ButtonGetMoreCoins()
    {
        OnButtonGetMoreCoins?.Invoke();
    }
    #endregion
}