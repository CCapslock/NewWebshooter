using System.Collections;
using UnityEngine;


public class FallingHelicopterState : BaseHelicopterState
{
    float _amplitude = 0;
    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.AskingPlayerLookAtMe();
        view.transform.Rotate(0, 1f, 0);
        view.transform.Translate(0, _amplitude * 0.1f, 0);
        _amplitude++;
    }
}
