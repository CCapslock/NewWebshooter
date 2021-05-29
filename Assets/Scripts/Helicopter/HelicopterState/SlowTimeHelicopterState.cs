using System.Collections;
using UnityEngine;


public class SlowTimeHelicopterState : BaseHelicopterState
{
    private float coef = 100;
    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        if (coef > 0)
            coef--;
        view.AskingPlayerLookAtMe();
        view.transform.Translate(0, -view.SlowTime, 0);
        view.Lopasti.transform.Rotate(0, 0.02f * coef, 0);
        view.BackLopasti.transform.Rotate(0.01f * coef, 0, 0);
        view.transform.Rotate(0, 0.01f*coef, 0);
    }

}