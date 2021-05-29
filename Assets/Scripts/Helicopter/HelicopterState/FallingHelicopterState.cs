using System.Collections;
using UnityEngine;


public class FallingHelicopterState : BaseHelicopterState
{
    private float _amplitude = 0;
    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.AskingPlayerLookAtMe();
        view.Lopasti.transform.Rotate(0, 0.5f, 0);
        view.BackLopasti.transform.Rotate(0.3f, 0, 0);
        view.transform.Rotate(0, 1f, 0);
        view.transform.Translate(0, -(_amplitude * 0.01f), 0);
        _amplitude++;
        if (_amplitude > 120)
        {
            view.ChangeState(HelicopterStates.SlowTime);
        }
    }
}
