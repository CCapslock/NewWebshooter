using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSkinModel : MonoBehaviour
{
    [SerializeField] private Sprite _lockedImage;
    [SerializeField] private Sprite _unlockedImage;

    [SerializeField] private SkinState _state;

    public Sprite LockedImage => _lockedImage;
    public Sprite UnlockedImage => _unlockedImage;
    public SkinState State => _state;


    public void ChangeState(SkinState state)
    {
        _state = state;
    }

    public void SaveState()
    {
        switch (_state)
        {
            case SkinState.Locked:
                PlayerPrefs.SetInt(gameObject.name, 0);
                PlayerPrefs.Save();
                break;
            case SkinState.Unlocked:
                PlayerPrefs.SetInt(gameObject.name, 1);
                PlayerPrefs.Save();
                break;
            case SkinState.Selected:
                PlayerPrefs.SetInt(gameObject.name, 2);
                PlayerPrefs.Save();
                break;
        }
    }

    public void LoadState()
    {
        int i = PlayerPrefs.GetInt(gameObject.name);
        switch (i)
        {
            case 0:
                _state = SkinState.Locked;
                break;
            case 1:
                _state = SkinState.Unlocked;
                break;
            case 2:
                _state = SkinState.Selected;
                break;
            default:
                _state = SkinState.Locked;
                break;
        }
    }
}