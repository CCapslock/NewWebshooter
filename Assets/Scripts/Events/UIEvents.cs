using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;

    public void Awake()
    {
        if (Current != null)
        {
            Destroy(this.gameObject);
            return;
        }
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

    public Action<WebSkinModel> OnButtonSelectSkinWeb;
    public void ButtonSelectSkinWeb(WebSkinModel skin)
    {
        OnButtonSelectSkinWeb?.Invoke(skin);
    }

    public Action<WebSkinModel> OnButtonBuySkinNet;
    public void ButtonBuySkinNet(WebSkinModel skin)
    {
        OnButtonBuySkinNet?.Invoke(skin);
    }

    public Action OnUpdateShop;
    public void UpdateShop()
    {
        OnUpdateShop?.Invoke();
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

    public Action<int> OnButtonGetMoreCoins;
    public void ButtonGetMoreCoins(int coins)
    {
        OnButtonGetMoreCoins?.Invoke(coins);
    }
    #endregion
}