using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frameSelected;
    [SerializeField] private Button _btnSelect;


    public void SetItem(GlovesSkinModel skin)
    {
        _btnSelect.onClick.RemoveAllListeners();
        switch (skin.State)
        {
            case SkinState.Locked:
                _image.sprite = skin.LockedImage;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = false;
                break;
            case SkinState.Unlocked:
                _btnSelect.onClick.AddListener(() => UIEvents.Current.ButtonSelectSkinGloves(skin));
                _image.sprite = skin.UnlockedImage; ;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = true;
                break;
            case SkinState.Selected:
                _image.sprite = skin.UnlockedImage;
                _frameSelected.gameObject.SetActive(true);
                _btnSelect.interactable = false;
                break;
        }
    }
    public void SetItem(WebSkinModel skin)
    {
        _btnSelect.onClick.RemoveAllListeners();
        switch (skin.State)
        {
            case SkinState.Locked:
                _image.sprite = skin.LockedImage;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = false;
                break;
            case SkinState.Unlocked:
                _btnSelect.onClick.AddListener(() => UIEvents.Current.ButtonSelectSkinWeb(skin));
                _image.sprite = skin.UnlockedImage; ;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = true;
                break;
            case SkinState.Selected:
                _image.sprite = skin.UnlockedImage;
                _frameSelected.gameObject.SetActive(true);
                _btnSelect.interactable = false;
                break;
        }
    }

    public void SetPopUp(GlovesSkinModel skin)
    {
        _btnSelect.gameObject.SetActive(false);
        _frameSelected.gameObject.SetActive(false);

        _image.sprite = skin.UnlockedImage;
    }
    public void SetPopUp(WebSkinModel skin)
    {
        _btnSelect.gameObject.SetActive(false);
        _frameSelected.gameObject.SetActive(false);

        _image.sprite = skin.UnlockedImage;
    }
}