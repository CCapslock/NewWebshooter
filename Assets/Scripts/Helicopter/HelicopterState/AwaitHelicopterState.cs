using System.Collections;
using UnityEngine;


public class AwaitHelicopterState : BaseHelicopterState
{
    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.Lopasti.transform.Rotate(0, 1, 0);
        view.BackLopasti.transform.Rotate( 1, 0, 0);
    }
}