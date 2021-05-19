using UnityEngine;
using UnityEngine.UI;

public class SkinSliderMenu : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Slider")]
    [SerializeField] private Slider _slider;

    [Header("Buttons")]
    [SerializeField] private Button _btnGet;
    [SerializeField] private Button _btnNo;

    private GlovesSkinManager _glovesManager;

    private float _currentSliderCount;
    private float _sliderGoal = 0;

    private string _saveKey = "Slider";

    private float _sliderDelta = 0.01f;
    private float _sliderTime = 0.5f;
    private float _noThanksTime = 2f;


    private void Start()
    {
        GameEvents.Current.OnLevelComplete += SliderFill;
        UIEvents.Current.OnButtonNextLevel += DeactivateMenu;
    }

    private void SliderFill()
    {
        _glovesManager = FindObjectOfType<GlovesSkinManager>();

        if (!IsEnoughGloves())
        {
            return;
        }

        _currentSliderCount = PlayerPrefs.GetFloat(_saveKey);

        switch (_currentSliderCount)
        {
            case 0f:
                _sliderGoal = 0.33f;
                break;
            case 0.33f:
                _sliderGoal = 0.66f;
                break;
            case 0.66f:
                _sliderGoal = 1f;
                ActivatePanel();
                break;
            case 1f:
                _currentSliderCount = 0f;
                _sliderGoal = 0.25f;
                break;
            case 0.25f:
                _sliderGoal = 0.5f;
                break;
            case 0.5f:
                _sliderGoal = 0.75f;
                break;
            case 0.75f:
                _sliderGoal = 1f;
                ActivatePanel();
                break;
            default:
                _sliderGoal = 0.33f;
                break;
        }

        int sliderTicks = (int)((_sliderGoal - _currentSliderCount) / _sliderDelta);

        _slider.gameObject.SetActive(true);
        _slider.value = _currentSliderCount;

        for (int i = 0; i < sliderTicks; i++)
        {
            Invoke("AddSliderCount", i * (_sliderTime / sliderTicks));
        }

        PlayerPrefs.SetFloat(_saveKey, _sliderGoal);
    }

    private void AddSliderCount()
    {
        _slider.value += _sliderDelta;
    }

    private void ActivatePanel()
    {
        _panel.SetActive(true);
        _btnNo.interactable = false;
        _btnGet.onClick.RemoveAllListeners();
        _btnNo.onClick.RemoveAllListeners();

        GlovesSkinModel skin = null;

        for (int i = 0; i < _glovesManager.Skins.Length; i++)
        {
            if (_glovesManager.Skins[i].State == SkinState.Locked)
            {
                skin = _glovesManager.Skins[i];
            }
        }

        _btnGet.onClick.AddListener(() => UIEvents.Current.ButtonGetSkinGloves(skin));
        _btnNo.onClick.AddListener(DeactivateMenu);

        Invoke("ActivateNoButton", _noThanksTime);
    }

    private void ActivateNoButton()
    {
        _btnNo.interactable = true;
    }

    private void DeactivateMenu()
    {
        _panel.SetActive(false);
        _slider.gameObject.SetActive(false);
    }

    private bool IsEnoughGloves()
    {
        for (int i = 0; i < _glovesManager.Skins.Length; i++)
        {
            if (_glovesManager.Skins[i].State == SkinState.Locked)
            {
                return true;
            }
        }

        return false;
    }
}