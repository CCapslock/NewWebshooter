using UnityEngine;
using UnityEngine.UI;

public class PopUpSkinMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Rolling image")]
    [SerializeField] private Image _image;

    [Header("Shop Item")]
    [SerializeField] private ShopItem _shopItem;

    [Header("Button hide")]
    [SerializeField] private Button _btnHide;

    private float _speed;
    private bool _isActive;
    

    private void Start()
    {
        GameEvents.Current.OnUnlockGloves += ShowPopUp;
        GameEvents.Current.OnUnlockWeb += ShowPopUp;

        _btnHide.onClick.AddListener(() => Hide());
    }

    public override void Hide()
    {
        _panel.SetActive(false);
        _isActive = false;
    }

    public override void Show()
    {
        _panel.SetActive(true);
        _isActive = true;
    }

    private void ShowPopUp(GlovesSkinModel skin)
    {
        _shopItem.SetPopUp(skin);
        Show();
    }
    private void ShowPopUp(WebSkinModel skin)
    {
        _shopItem.SetPopUp(skin);
        Show();
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnUnlockGloves -= ShowPopUp;
        GameEvents.Current.OnUnlockWeb -= ShowPopUp;
    }
}