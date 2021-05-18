using System.Collections;
using UnityEngine;

public class IdleMisterioStateModel : BaseMisterioModel
{
    //Летают и пугают добрых молодцев
    private float radian;
    private Vector3 temporalVector;
    public override void Execute(MisterioView view)
    {
        
        base.Execute(view);
        view.CircleRadiusPosition += 0.5f;
        radian = view.CircleRadiusPosition * Mathf.PI / 180;
        temporalVector.x = Mathf.Cos(radian) * 2f + view.CircleCenter.position.x;
        temporalVector.y = Mathf.Sin(radian) * 2f + view.CircleCenter.position.y;
        temporalVector.z = view.transform.position.z;
        view.transform.position = temporalVector;
        
    }
}
