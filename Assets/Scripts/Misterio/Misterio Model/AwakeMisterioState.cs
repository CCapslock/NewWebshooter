using System.Collections;
using UnityEngine;

public class AwakeMisterioStateModel : BaseMisterioModel
{
    //крадется на стартовую точку
    public override void Execute(MisterioView view)
    {
        base.Execute(view);
        view.transform.position = Vector3.MoveTowards
            (view.transform.position,
            view.CircleCenter.position,
            2f * Time.deltaTime);
        if ((view.transform.position - view.CircleCenter.position).magnitude < 0.1f)
        {
            MisterioController.Current.ChangeNextState(view, MisterioState.Transporting);
        }
    }
}