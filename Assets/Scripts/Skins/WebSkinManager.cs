using UnityEngine;

public class WebSkinManager : MonoBehaviour
{
    private WebSkinModel[] _skins;
    public WebSkinModel[] Skins => _skins;


    private void Awake()
    {
        
        _skins = FindObjectOfType<WebShooter>().StuckedWeb.GetComponentsInChildren<WebSkinModel>(true);
    }

    private void Start()
    {
        LoadSkins();
        GameEvents.Current.OnUnlockWeb += UnlockSkin;
        GameEvents.Current.OnSelectWeb += SelectSkin;
    }

    private void LoadSkins()
    {
        bool isSelected = false;
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].LoadState();
            _skins[i].gameObject.SetActive(false);
            if (_skins[i].State == SkinState.Selected)
            {
                WebShooter.Current.SetMaterial(_skins[i].WebMaterial);
                if (isSelected == false)
                {
                    _skins[i].gameObject.SetActive(true);
                    isSelected = true;
                }
                else
                {
                    _skins[i].ChangeState(SkinState.Unlocked);
                    _skins[i].SaveState();
                }
            }
        }

        if (_skins[0].State == SkinState.Locked)
        {
            SelectSkin(_skins[0]);
        }
    }

    public void UnlockSkin(WebSkinModel skin)
    {
        skin.ChangeState(SkinState.Unlocked);
        skin.SaveState();
        LoadSkins();
        UIEvents.Current.UpdateShop();
        GameEvents.Current.SelectWeb(skin);
    }

    public void SelectSkin(WebSkinModel skin)
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].gameObject.SetActive(false);
            if (_skins[i].State == SkinState.Selected)
            {
                _skins[i].ChangeState(SkinState.Unlocked);
                _skins[i].SaveState();
            }
        }
        skin.gameObject.SetActive(true);
        
        skin.ChangeState(SkinState.Selected);
        skin.SaveState();

        
        LoadSkins();
        UIEvents.Current.UpdateShop();
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnUnlockWeb -= UnlockSkin;
        GameEvents.Current.OnSelectWeb -= SelectSkin;
    }
}