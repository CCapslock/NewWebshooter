using System.Collections;
using UnityEngine;

public class PuffedMisterioStateModel : BaseMisterioModel
{
    //получил паутину в лицо
    public override void Execute(MisterioView view)
    {
        base.Execute(view);
        view.Transform.position = Vector3.MoveTowards(
            view.Transform.position, 
            view.CircleCenter.position, 
            2f * Time.deltaTime);
        if ((view.Transform.position - view.CircleCenter.position).magnitude < 0.5f)
        {
            MisterioController.Current.ChangeNextState(view,MisterioState.Transporting);
        }
    }

}
