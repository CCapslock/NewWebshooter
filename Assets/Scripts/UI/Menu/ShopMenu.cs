using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Buttons")]
    [SerializeField] private Button _btnExit;

    private UIController _controller;


    private void Awake()
    {
        _controller = transform.parent.GetComponent<UIController>();

        _btnExit.onClick.AddListener(UIEvents.Current.ButtonMainMenu);
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