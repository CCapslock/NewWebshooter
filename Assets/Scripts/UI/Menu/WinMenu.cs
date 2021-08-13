using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Buttons")]
    [SerializeField] private Button _btnNextLevel;
    [SerializeField] private Button _btnGetMoreCoins;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private TextMeshProUGUI _textScale;

    [Header("Multilier")]
    [SerializeField] private GameObject _multiplier;
    [SerializeField] private GameObject _multiplierArrow;

    private UIController _controller;

    private TextMeshProUGUI _buttonMultilyText;
    private float _timeOfAddingCoins = 0.5f;
    private int _addingCoinsAmount;

    private bool _isMultiply = false;

    public GameObject Arrow => _multiplierArrow;
    public bool IsMultiply => _isMultiply;

    private void Awake()
    {
        _controller = transform.parent.GetComponent<UIController>();
        _buttonMultilyText = _btnGetMoreCoins.GetComponentInChildren<TextMeshProUGUI>();
    }


    public override void Hide()
    {
        if (!IsShow) return;
        _panel.gameObject.SetActive(false);
        UIEvents.Current.OnRewardedVideoAvailabilityChanged -= SetInteractable;
        IsShow = false;
        _isMultiply = false;
    }

    public override void Show()
    {
        if (IsShow) return;

        _panel.gameObject.SetActive(true);
        _btnNextLevel.gameObject.SetActive(true);
        UIEvents.Current.OnRewardedVideoAvailabilityChanged += SetInteractable;

        IsShow = true;
        if (MainGameController.BossContainter != null)
        {
            if (MainGameController.BossContainter is FinalZoneView)
            {              
                _textScale.text = $"BONUS  X{FinalZoneView.Multiplier}";                
            }
        }
        else
        {
            _textScale.text = "Great";
        }
        _btnNextLevel.onClick.RemoveListener(GameEvents.Current.InterstitialAsked);
        _btnNextLevel.onClick.AddListener(UIEvents.Current.ButtonNextLevel);
    }



    public void ActivatePanel(int coins, bool isMultiplier)
    {
        if (isMultiplier == false)
        {
            _isMultiply = true;
            _multiplier.SetActive(true);

            _btnGetMoreCoins.gameObject.SetActive(true);
            /*if (!IronSource.Agent.isRewardedVideoAvailable())
            {
                SetInteractable(false);
            }*/
            SetInteractable(IronSource.Agent.isRewardedVideoAvailable());
            _btnGetMoreCoins.onClick.RemoveAllListeners();
            _btnGetMoreCoins.onClick.AddListener(() => UIEvents.Current.ButtonGetMoreCoins(coins));

            UpdateButtonMultiplierText(coins, _controller.CoinsMultiplier);
            _btnNextLevel.onClick.AddListener(GameEvents.Current.InterstitialAsked);
            _textCoins.text = "+0";
            AddMoreCoinsInUI(coins);
        }
        else
        {
            _isMultiply = false;
            _multiplier.SetActive(false);

            _btnGetMoreCoins.onClick.RemoveAllListeners();
            _btnGetMoreCoins.gameObject.SetActive(false);
            _btnNextLevel.onClick.RemoveAllListeners();
            _btnNextLevel.onClick.AddListener(UIEvents.Current.ButtonNextLevel);
            _textCoins.text = "+0";
            AddMoreCoinsInUI(coins);
        }

    }

    private void SetInteractable(bool value)
    {
        _btnGetMoreCoins.interactable = value;
    }

    private void AddMoreCoinsInUI(int amount)
    {
        _addingCoinsAmount = 0;
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

    public void UpdateButtonMultiplierText(int coins, int multiplier)
    {
        _buttonMultilyText.text = $"GET {coins * multiplier}";
    }
}