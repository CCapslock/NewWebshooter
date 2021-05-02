using System.Collections;
using UnityEngine;


public class AwakeGoblinModel : BaseGoblinModel
{
    private float _distanceToFinalPoint;
    public override void Execute(GoblinView view)
    {
        base.Execute(view);
        view.transform.LookAt(view.FinalAwakTransform);
        _distanceToFinalPoint = (view.transform.position - view.FinalAwakTransform.position).magnitude;
        
        if (_distanceToFinalPoint < view.SmothAwakChangeDistance)
        {
            view.ChangeState(GoblinState.Idle);
        }

        view.transform.position = Vector3.Lerp(view.transform.position, view.FinalAwakTransform.position, 0.2f);
    }
}