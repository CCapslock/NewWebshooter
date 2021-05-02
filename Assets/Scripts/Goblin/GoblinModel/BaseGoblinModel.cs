using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGoblinModel
{
    protected GoblinView _view;

    public virtual void Execute(GoblinView view)
    {
        _view = view;
    }

    
}
