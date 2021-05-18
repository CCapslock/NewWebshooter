using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMisterioModel
{
    
    public virtual void Execute(MisterioView view)
    {
        view.Transform.LookAt(view.Player.transform);
    }
}