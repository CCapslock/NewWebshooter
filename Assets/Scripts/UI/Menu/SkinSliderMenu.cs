using System.Collections;
using System.Collections.Generic;
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

    private string _saveKey = "Slider";
    private float _currentSliderCount;
    private bool _isEnoughGloves;


    private void Awake()
    {
        _glovesManager = FindObjectOfType<GlovesSkinManager>();
    }

    private void Start()
    {
        for (int i = 0; i < _glovesManager.Skins.Length; i++)
        {
            if (_glovesManager.Skins[i].State == SkinState.Locked)
            {
                _isEnoughGloves = true;
            }
        }

        //GameEvents.Current.OnLevelComplete +=
    }


}