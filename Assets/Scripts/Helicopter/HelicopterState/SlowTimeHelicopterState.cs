using System.Collections;
using UnityEngine;


public class SlowTimeHelicopterState : BaseHelicopterState
{

    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.AskingPlayerLookAtMe();
        view.transform.Translate(0, -view.SlowTime, 0);
        
    }

}