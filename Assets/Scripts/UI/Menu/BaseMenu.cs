using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    protected bool IsShow { get; set; }

    public abstract void Hide();
    public abstract void Show();
}