using System.Collections;
using UnityEngine;


public class AwaitHelicopterState : BaseHelicopterState
{
    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.Lopasti.transform.Rotate(0, 6, 0);
        view.BackLopasti.transform.Rotate( 6, 0, 0);
    }
}