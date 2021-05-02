using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Buttons")]
    [SerializeField] private Button _btnStartGame;
    [SerializeField] private Button _btnShop;

    private UIController _controller;


    private void Awake()
    {
        _controller = transform.parent.GetComponent<UIController>();

        _btnStartGame.onClick.AddListener(UIEvents.Current.ButtonLevelStart);
        _btnShop.onClick.AddListener(UIEvents.Current.ButtonShop);
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _panel.gameObject.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _panel.gameObject.SetActive(true);
        IsShow = true;
    }
}