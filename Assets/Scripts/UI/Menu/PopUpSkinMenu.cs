using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSkinMenu : BaseMenu
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _particles;

    [Header("Shop Item")]
    [SerializeField] private ShopItem _shopItem;


    public override void Hide()
    {
        _panel.SetActive(false);
        _particles.Play();
    }

    public override void Show()
    {
        _panel.SetActive(true);
        _particles.Stop();
    }
}