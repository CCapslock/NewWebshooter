using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSkinMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;



    public override void Hide()
    {
        _panel.SetActive(false);
    }

    public override void Show()
    {
        _panel.SetActive(true);
    }
}