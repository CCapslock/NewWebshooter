using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Buttons")]
    [SerializeField] private Button _btnNextLevel;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _textCoins;

    private UIController _controller;

    private float _timeOfAddingCoins = 0.5f;
    private int _addingCoinsAmount;


    private void Awake()
    {
        _controller = transform.parent.GetComponent<UIController>();

        _btnNextLevel.onClick.AddListener(UIEvents.Current.ButtonNextLevel);
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

    public void ActivatePanel(int coins)
    {
        _textCoins.text = "+0";
        AddMoreCoinsInUI(coins);
    }
    private void AddMoreCoinsInUI(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Invoke("AddSingleCoin", 0.5f + _timeOfAddingCoins / amount * i);
        }
    }
    private void AddSingleCoin()
    {
        _addingCoinsAmount++;
        _textCoins.text = "+" + _addingCoinsAmount;
    }
}