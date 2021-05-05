using UnityEngine;

public class GlovesSkinManager : MonoBehaviour
{
    [SerializeField] private GlovesSkinModel[] _skins;

    public GlovesSkinModel[] Skins => _skins;


    private void Awake()
    {
        LoadSkins();
    }

    private void Start()
    {
        GameEvents.Current.OnUnlockGloves += UnlockSkin;
        GameEvents.Current.OnSelectGloves += SelectSkin;
    }

    public void SelectSkin(GlovesSkinModel skin)
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].Hide();
            if (_skins[i].State == SkinState.Selected)
            {
                _skins[i].ChangeState(SkinState.Unlocked);
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
                }
            }
        }

        if (_skins[0].State == SkinState.Locked)
        {
            SelectSkin(_skins[0]);
        }
    }
}