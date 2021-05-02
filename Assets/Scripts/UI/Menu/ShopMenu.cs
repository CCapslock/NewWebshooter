using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Buttons")]
    [SerializeField] private Button _btnExit;

    [Header("Gloves")]
    [SerializeField] private GameObject _panelGloves;
    [SerializeField] private ShopItemGloves[] _glovesItems;
    [SerializeField] private Button _btnGloves;

    [Header("Nets")]
    [SerializeField] private GameObject _panelNets;
    [SerializeField] private ShopItemNet[] _netItems;
    [SerializeField] private Button _btnNets;

    [Header("Colors")]
    [SerializeField] private Color _colorBack;

    private GlovesSkinManager _glovesManager;


    private void Awake()
    {
        _btnExit.onClick.AddListener(UIEvents.Current.ButtonMainMenu);

        _btnGloves.onClick.AddListener(() => OpenGlovesPanel());
        _btnNets.onClick.AddListener(() => OpenNetsPanel());

        UIEvents.Current.OnButtonShop += SetItems;
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

    private void SetItems()
    {
        _glovesManager = FindObjectOfType<GlovesSkinManager>();
        
        for (int i = 0; i < _glovesItems.Length; i++)
        {
            _glovesItems[i].SetItem(_glovesManager.Skins[i]);
        }
    }

    private void OpenGlovesPanel()
    {
        _panelGloves.SetActive(true);
        _panelNets.SetActive(false);
        _btnGloves.image.color = _panelGloves.GetComponent<Image>().color;
        _btnNets.image.color = _colorBack;
    }
    private void OpenNetsPanel()
    {
        _panelGloves.SetActive(false);
        _panelNets.SetActive(true);
        _btnGloves.image.color = _colorBack;
        _btnNets.image.color = _panelNets.GetComponent<Image>().color;
    }
}