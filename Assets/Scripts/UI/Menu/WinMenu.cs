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

    private UIController _controller;

    private float _timeOfAddingCoins = 0.5f;
    private int _addingCoinsAmount;


    private void Awake()
    {
        _controller = transform.parent.GetComponent<UIController>();
        
        
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _panel.gameObject.SetActive(false);
        UIEvents.Current.OnRewardedVideoAvailabilityChanged -= SetInteractable;
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        
        _panel.gameObject.SetActive(true);        
        _btnNextLevel.gameObject.SetActive(true);
        UIEvents.Current.OnRewardedVideoAvailabilityChanged += SetInteractable;

        IsShow = true;

        var value = FinalZoneView.Multiplier;
        if (value > 2)
        {
            _textScale.text = $"BONUS  X{FinalZoneView.Multiplier}";
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
            _btnGetMoreCoins.gameObject.SetActive(true);
            /*if (!IronSource.Agent.isRewardedVideoAvailable())
            {
                SetInteractable(false);
            }*/
            SetInteractable(IronSource.Agent.isRewardedVideoAvailable());
            _btnGetMoreCoins.onClick.RemoveAllListeners();
            _btnGetMoreCoins.onClick.AddListener(() => UIEvents.Current.ButtonGetMoreCoins(coins));
            
            _btnGetMoreCoins.GetComponentInChildren<TextMeshProUGUI>().text = $"GET {coins * _controller.CoinsMultiplier}";
            _btnNextLevel.onClick.AddListener(GameEvents.Current.InterstitialAsked);
            _textCoins.text = "+0";
            AddMoreCoinsInUI(coins);
        }
        else
        {
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
}