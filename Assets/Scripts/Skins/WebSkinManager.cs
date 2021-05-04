using UnityEngine;

public class WebSkinManager : MonoBehaviour
{
    private WebSkinModel[] _skins;

    public WebSkinModel[] Skins => _skins;


    private void Awake()
    {
        _skins = FindObjectOfType<WebShooter>().StuckedWeb.GetComponentsInChildren<WebSkinModel>(true);
        LoadSkins();
    }

    private void LoadSkins()
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].LoadState();
        }
    }

    public void UnlockSkin(WebSkinModel skin)
    {
        skin.ChangeState(SkinState.Unlocked);
        skin.SaveState();
    }

    public void SelectSkin(WebSkinModel skin)
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].gameObject.SetActive(false);
        }
        skin.gameObject.SetActive(true);
        skin.ChangeState(SkinState.Selected);
        skin.SaveState();
    }
}