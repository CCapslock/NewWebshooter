using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _locker;
    [SerializeField] private Image _frameSelected;
    [SerializeField] private Button _btnSelect;
    [SerializeField] private MMFeedback _frameSelectedFeedback;

    public void SetItem(GlovesSkinModel skin)
    {
        _btnSelect.onClick.RemoveAllListeners();
        _frameSelectedFeedback.StopAllCoroutines();
        switch (skin.State)
        {
            case SkinState.Locked:
                _image.sprite = skin.LockedImage;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = false;
                _locker.gameObject.SetActive(true);

                _frameSelectedFeedback.StopAllCoroutines();
                _frameSelectedFeedback.Active = false;
                _frameSelectedFeedback.enabled = false;

                break;
            case SkinState.Unlocked:
                _btnSelect.onClick.AddListener(() => UIEvents.Current.ButtonSelectSkinGloves(skin));
                _image.sprite = skin.UnlockedImage;
                _btnSelect.interactable = true;
                _locker.gameObject.SetActive(false);


                _frameSelectedFeedback.StopAllCoroutines();
                _frameSelectedFeedback.enabled = false;
                _frameSelectedFeedback.Active = false;
                _frameSelected.gameObject.SetActive(false);

                break;
            case SkinState.Selected:
                _image.sprite = skin.UnlockedImage;
                _frameSelected.gameObject.SetActive(true);
                _btnSelect.interactable = false;
                _locker.gameObject.SetActive(false);

                _frameSelectedFeedback.enabled = true;
                _frameSelectedFeedback.Initialization(_frameSelectedFeedback.gameObject);
                _frameSelectedFeedback.Active = true;
                if (this.isActiveAndEnabled)
                {
                    _frameSelectedFeedback.Play(_frameSelectedFeedback.transform.position, default);
                }
                Invoke("InvokedPlayAnimation", 0f);
                
                break;
            default: break;
        }
    }

    private void InvokedPlayAnimation()
    {
        if (this.isActiveAndEnabled)
        { 
            _frameSelectedFeedback.Play(_frameSelectedFeedback.transform.position, default);
        }
    }

    public void SetItem(WebSkinModel skin)
    {
        _btnSelect.onClick.RemoveAllListeners();
        _frameSelectedFeedback.StopAllCoroutines();
        switch (skin.State)
        {
            case SkinState.Locked:
                _image.sprite = skin.LockedImage;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = false;
                _locker.gameObject.SetActive(true);

                _frameSelectedFeedback.StopAllCoroutines();
                _frameSelectedFeedback.Active = false;
                _frameSelectedFeedback.enabled = false;
                break;
            case SkinState.Unlocked:
                _btnSelect.onClick.AddListener(() => UIEvents.Current.ButtonSelectSkinWeb(skin));
                _image.sprite = skin.UnlockedImage; ;
                _frameSelected.gameObject.SetActive(false);
                _btnSelect.interactable = true;
                _locker.gameObject.SetActive(false);

                _frameSelectedFeedback.StopAllCoroutines();
                _frameSelectedFeedback.enabled = false;
                _frameSelectedFeedback.Active = false;
                break;
            case SkinState.Selected:
                _image.sprite = skin.UnlockedImage;
                _frameSelected.gameObject.SetActive(true);
                _btnSelect.interactable = false;
                _locker.gameObject.SetActive(false);

                _frameSelectedFeedback.enabled = true;
                _frameSelectedFeedback.Initialization(_frameSelectedFeedback.gameObject);
                _frameSelectedFeedback.Active = true;
                if (this.isActiveAndEnabled)
                {
                    _frameSelectedFeedback.Play(_frameSelectedFeedback.transform.position, default);
                }
                Invoke("InvokedPlayAnimation", 0f);
                break;
            default: break;
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