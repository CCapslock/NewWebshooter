using UnityEngine;
using UnityEngine.UI;

public class PopUpSkinMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _particles;

    [Header("Shop Item")]
    [SerializeField] private ShopItem _shopItem;

    [Header("Button hide")]
    [SerializeField] private Button _btnHide;
    

    private void Start()
    {
        GameEvents.Current.OnUnlockGloves += ShowPopUp;
        GameEvents.Current.OnUnlockWeb += ShowPopUp;

        _btnHide.onClick.AddListener(() => Hide());
    }

    public override void Hide()
    {
        _panel.SetActive(false);
        _particles.Stop();
    }

    public override void Show()
    {
        _panel.SetActive(true);
        _particles.Play();
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