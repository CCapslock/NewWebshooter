using System.Collections;
using UnityEngine;

public class TransportingMisterioStateModel : BaseMisterioModel
{
    ///Мистерики заспавнились и полетели по своим местам
    public override void Execute(MisterioView view)
    {
        base.Execute(view);
        view.transform.position = Vector3.MoveTowards(
            view.transform.position, 
            view.BaseCirclePosition, 
            2f * Time.deltaTime);
        if ((view.transform.position - view.BaseCirclePosition).magnitude < 0.1f)
        {
            MisterioController.Current.ChangeNextState(view, MisterioState.Idle);
        }

    }
}
