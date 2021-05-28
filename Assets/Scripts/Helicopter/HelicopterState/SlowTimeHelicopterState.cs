using System.Collections;
using UnityEngine;


public class SlowTimeHelicopterState : BaseHelicopterState
{

    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.AskingPlayerLookAtMe();
    }

}