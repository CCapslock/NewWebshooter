using UnityEngine;
using UnityEngine.UI;

public class ShopItemGloves : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frameSelected;
    [SerializeField] private Button _btnSelect;


    public void SetItem(GlovesSkinModel skin)
    {
        switch (skin.State)
        {
            case SkinState.Locked:
                _image.sprite = skin.LockedImage;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = false;
                break;
            case SkinState.Unlocked:
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
}