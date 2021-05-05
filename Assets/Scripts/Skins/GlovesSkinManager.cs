using UnityEngine;

public class GlovesSkinManager : MonoBehaviour
{
    private GlovesSkinModel[] _skins;

    public GlovesSkinModel[] Skins => _skins;


    private void Awake()
    {
        _skins = GetComponentsInChildren<GlovesSkinModel>();
        LoadSkins();
    }

    private void Start()
    {
        GameEvents.Current.OnUnlockGloves += UnlockSkin;
        GameEvents.Current.OnSelectGloves += SelectSkin;
    }

    public void SelectSkin(GlovesSkinModel skin)
    {
        Debug.LogWarning($"Selecting skin {skin.gameObject.name}");
        Debug.LogWarning($"Skin lenght = {_skins.Length}");

        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].Hide();
            if (_skins[i].State == SkinState.Selected)
            {
                _skins[i].ChangeState(SkinState.Unlocked);
                _skins[i].SaveState();
            }
        }

        skin.Show();
        skin.ChangeState(SkinState.Selected);
        skin.SaveState();
        UIEvents.Current.UpdateShop();
    }

    public void UnlockSkin(GlovesSkinModel skin)
    {
        skin.ChangeState(SkinState.Unlocked);
        skin.SaveState();
        UIEvents.Current.UpdateShop();
    }

    public void LoadSkins()
    {
        bool isSelected = false;
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].LoadState();
            _skins[i].Hide();
            if (_skins[i].State == SkinState.Selected)
            {
                if (isSelected == false)
                {
                    _skins[i].Show();
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

    private void OnDestroy()
    {
        GameEvents.Current.OnUnlockGloves -= UnlockSkin;
        GameEvents.Current.OnSelectGloves -= SelectSkin;
    }
}