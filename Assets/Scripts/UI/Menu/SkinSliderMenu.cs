using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.Feedbacks;
public class SkinSliderMenu : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Slider")]
    [SerializeField] private GameObject _slider;
    [SerializeField] private Image _sliderUnfilled;
    [SerializeField] private Image _sliderFilled;
    [SerializeField] private GameObject _rotatingCircle;

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

    private int _skinShowed = 0;


    private GlovesSkinModel _currentUnlockableSkin = null;

    private void Start()
    {
        GameEvents.Current.OnLevelComplete += SliderFill;
        UIEvents.Current.OnButtonNextLevel += DeactivateMenu;
    }

    private void SliderFill()
    {
        
        _glovesManager = FindObjectOfType<GlovesSkinManager>();

        /*
        if (!IsEnoughGloves())
        {
            return;
        }   */     
        //
        _currentUnlockableSkin = IsEnoughGloves();
        if (_currentUnlockableSkin == null)
        {
            return;
        }
        else
        {
            _sliderUnfilled.sprite = _currentUnlockableSkin.LockedImage;
            _sliderFilled.sprite = _currentUnlockableSkin.UnlockedImage;            
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
                _skinShowed++;
                if (_skinShowed > _glovesManager.Skins.Length - 1)
                {
                    _skinShowed = 0;
                }
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
                _skinShowed++;
                if (_skinShowed > _glovesManager.Skins.Length - 1)
                {
                    _skinShowed = 0;
                }
                ActivatePanel();
                break;
            default:
                _sliderGoal = 0.33f;
                break;
        }

        int sliderTicks = (int)((_sliderGoal - _currentSliderCount) / _sliderDelta);

        _slider.SetActive(true);
        _sliderFilled.fillAmount = _currentSliderCount;

        for (int i = 0; i < sliderTicks; i++)
        {
            Invoke("AddSliderCount", i * (_sliderTime / sliderTicks));
        }
        StopCoroutine(RotateRotatingCircle());
        StartCoroutine(RotateRotatingCircle());
        PlayerPrefs.SetFloat(_saveKey, _sliderGoal);
    }
    

    private void AddSliderCount()
    {        
        _sliderFilled.fillAmount += _sliderDelta;
    }

    private void ActivatePanel()
    {
        _panel.SetActive(true);
        _btnNo.interactable = false;
        _btnGet.onClick.RemoveAllListeners();
        _btnNo.onClick.RemoveAllListeners();
        UIEvents.Current.OnRewardedVideoAvailabilityChanged += SetInteractable;
        if (!IronSource.Agent.isRewardedVideoAvailable())
        {
            SetInteractable(false);
        }
        

        //Выставлять закрашенный незакрашенный визуал скина в панель фила

        _btnGet.onClick.AddListener(() => UIEvents.Current.ButtonGetSkinGloves(_currentUnlockableSkin));
        _btnGet.onClick.AddListener(DeactivateMenu);
        _btnNo.onClick.AddListener(DeactivateMenu);

        

        Invoke("ActivateNoButton", _noThanksTime);
    }

    public IEnumerator RotateRotatingCircle()
    {
        _rotatingCircle.SetActive(true);
        while (_rotatingCircle.activeSelf)
        {
            _rotatingCircle.transform.Rotate(0, 0, 0.75f);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    private void SetInteractable(bool value)
    {
        _btnGet.interactable = value;
    }

    private void ActivateNoButton()
    {
        _btnNo.interactable = true;
    }

    private void DeactivateMenu()
    {
        UIEvents.Current.OnRewardedVideoAvailabilityChanged -= SetInteractable;
        _panel.SetActive(false);
        _slider.gameObject.SetActive(false);
    }

    private GlovesSkinModel IsEnoughGloves()
    {
        for (int i = 0 + _skinShowed; i < _glovesManager.Skins.Length; i++)
        {
            if (_glovesManager.Skins[_glovesManager.Skins.Length - i - 1].State == SkinState.Locked)
            {
                return _glovesManager.Skins[_glovesManager.Skins.Length - i - 1];
            }
        }
        return null;
    }
}