using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Buttons")]
    [SerializeField] private Button _btnExit;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _coinsText;

    [Header("Gloves")]
    [SerializeField] private GameObject _panelGloves;
    [SerializeField] private ShopItem[] _glovesItems;
    [SerializeField] private Button _btnGloves;
    [SerializeField] private Button _btnBuyGloves;
    [SerializeField] private Button _btnGetGloves;

    [Header("Nets")]
    [SerializeField] private GameObject _panelNets;
    [SerializeField] private ShopItem[] _netItems;
    [SerializeField] private Button _btnNets;
    [SerializeField] private Button _btnBuyNet;
    [SerializeField] private Button _btnGetNet;

    [Header("Colors")]
    [SerializeField] private Color _colorBack;

    private UIController _uiController;
    private GlovesSkinManager _glovesManager;
    private WebSkinManager _webManager;
    private int _currentCoins;
    private int _skinPrice = 500;

    public int SkinPrice => _skinPrice;


    private void Awake()
    {
        _uiController = transform.parent.GetComponent<UIController>();
    }

    private void Start()
    {
        _btnExit.onClick.AddListener(UIEvents.Current.ButtonMainMenu);

        _btnGloves.onClick.AddListener(() => OpenGlovesPanel());
        _btnNets.onClick.AddListener(() => OpenNetsPanel());

        _btnBuyGloves.onClick.AddListener(() => UIEvents.Current.ButtonBuySkinGloves(GetRandomGloves()));
        _btnGetGloves.onClick.AddListener(() => UIEvents.Current.ButtonGetSkinGloves(GetFreeRandomGloves()));
        _btnBuyNet.onClick.AddListener(() => UIEvents.Current.ButtonBuySkinNet(GetRandomWeb()));
        _btnGetNet.onClick.AddListener(() => UIEvents.Current.ButtonGetSkinNet(GetRandomWeb()));

        UIEvents.Current.OnButtonShop += SetShop;
        UIEvents.Current.OnUpdateShop += SetShop;
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
        UIEvents.Current.OnRewardedVideoAvailabilityChanged += SetInteractable;
        IsShow = true;
    }

    public void SetInteractable(bool value)
    {       
        SetShop();
    }
    private void SetShop()
    {
        bool isLockedGloves = false;
        bool isLockedWebs = false;
        bool isEnoughMoney = false;

        _btnBuyGloves.interactable = false;
        _btnGetGloves.interactable = false;
        _btnBuyNet.interactable = false;
        _btnGetNet.interactable = false;

        _glovesManager = FindObjectOfType<GlovesSkinManager>();
        _webManager = FindObjectOfType<WebSkinManager>();
        
        for (int i = 0; i < _glovesItems.Length; i++)
        {
            _glovesItems[i].SetItem(_glovesManager.Skins[i]);

            if (_glovesManager.Skins[i].State == SkinState.Locked)
            {
                isLockedGloves = true;
            }
        }

        for (int i = 0; i < _netItems.Length; i++)
        {
            _netItems[i].SetItem(_webManager.Skins[i]);

            if (_webManager.Skins[i].State == SkinState.Locked)
            {
                isLockedWebs = true;
            }
        }

        _currentCoins = _uiController.GetCurrentCoins();
        _coinsText.text = $"{_currentCoins}";

        if (_currentCoins >= _skinPrice)
        {
            isEnoughMoney = true;
        }

        if (isLockedGloves)
        {
            _btnGetGloves.interactable = true;

            if (isEnoughMoney)
            {
                _btnBuyGloves.interactable = true;
            }
            else
            {
                _btnBuyGloves.interactable = false;
            }
        }
        else
        {
            _btnGetGloves.interactable = false;
        }

        if (isLockedWebs)
        {
            _btnGetNet.interactable = true;

            if (isEnoughMoney)
            {
                _btnBuyNet.interactable = true;
            }
            else
            {
                _btnBuyNet.interactable = false;
            }
        }
        else
        {
            _btnGetNet.interactable = false;
        }

        if (!IronSource.Agent.isRewardedVideoAvailable())
        {
            _btnGetGloves.interactable = false;
            _btnGetNet.interactable = false;
        }
    }

    private void OpenGlovesPanel()
    {
        _panelGloves.SetActive(true);
        _panelNets.SetActive(false);
        SetShop();
        _btnGloves.image.color = _panelGloves.GetComponent<Image>().color;
        _btnNets.image.color = _colorBack;
    }
    private void OpenNetsPanel()
    {
        _panelGloves.SetActive(false);
        _panelNets.SetActive(true);
        SetShop();
        _btnGloves.image.color = _colorBack;
        _btnNets.image.color = _panelNets.GetComponent<Image>().color;
    }

    private GlovesSkinModel GetRandomGloves()
    {
        int count = 0;
        List<GlovesSkinModel> lockedGloves = new List<GlovesSkinModel>();

        for (int i = 0; i < _glovesManager.Skins.Length; i++)
        {
            if (_glovesManager.Skins[i].State == SkinState.Locked)
            {
                lockedGloves.Add(_glovesManager.Skins[i]);
                count++;
            }
        }

        if (count == 1)
        {
            return lockedGloves[0];
        }

        int rnd = Random.Range(0, lockedGloves.Count);

        return lockedGloves[rnd];
    }

    private GlovesSkinModel GetFreeRandomGloves()
    {
        int count = 0;
        List<GlovesSkinModel> lockedGloves = new List<GlovesSkinModel>();

        for (int i = 0; i < _glovesManager.Skins.Length; i++)
        {
            if (_glovesManager.Skins[i].State == SkinState.Locked)
            {
                lockedGloves.Add(_glovesManager.Skins[i]);
                count++;
            }
        }

        if (count == 1)
        {
            return lockedGloves[0];
        }

        int rnd = Random.Range(0, lockedGloves.Count);

        return lockedGloves[rnd];
    }

    private WebSkinModel GetRandomWeb()
    {
        int count = 0;
        List<WebSkinModel> lockedWebs = new List<WebSkinModel>();

        for (int i = 0; i < _webManager.Skins.Length; i++)
        {
            if (_webManager.Skins[i].State == SkinState.Locked)
            {
                lockedWebs.Add(_webManager.Skins[i]);
                count++;
            }
        }

        if (count == 1)
        {
            return lockedWebs[0];
        }

        int rnd = Random.Range(0, lockedWebs.Count);

        return lockedWebs[rnd];
    }
}