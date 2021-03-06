using System.Collections.Generic;
using UnityEngine;


public class MisterioController 
{
    public static MisterioController Current;
    public List<MisterioView> _misterios = new List<MisterioView>();
    public Dictionary<MisterioState, BaseMisterioModel> _model = new Dictionary<MisterioState, BaseMisterioModel>();
    public MisterioView _realMisterio = null;
    public int MisteriosCount => _misterios.Count;
    public MisterioController()
    {
        _model.Add(MisterioState.Awaiting, new AwaitingMisterioStateModel());
        _model.Add(MisterioState.Awake, new AwakeMisterioStateModel());
        _model.Add(MisterioState.Defeated, new DefeatedMisterioStateModel());
        _model.Add(MisterioState.Idle, new IdleMisterioStateModel());
        _model.Add(MisterioState.Puffed, new PuffedMisterioStateModel());
        _model.Add(MisterioState.Transporting, new TransportingMisterioStateModel());
    }


    public void Execute()
    {
        foreach (MisterioView misterio in _misterios)
        {
            misterio.CheckState();
            _model[misterio.State].Execute(misterio);
        }
    }

    public void ChangeNextState(MisterioView View, MisterioState State)
    {
        View.SetNextState(State);
        switch (View.NextState)
        {            
            case MisterioState.Transporting:
                {
                    if (View.IsReal)
                    {
                        View.SpawnClones();
                    }
                }
                break;
            default: break;            
        }
    }

    public void AddMisterioToList(MisterioView misterio)
    {
        if (!_misterios.Contains(misterio))
        {
            _misterios.Add(misterio);
        }
        if (misterio.IsReal)
        {
            _realMisterio = misterio;
        }
    }

    public void RemoveMisterioFromList(MisterioView misterio)
    {
        if (_misterios.Contains(misterio))
        {
            _misterios.Remove(misterio);
        }

    }

    public void RemoveAllFakeMisterio()
    {/*
        foreach (MisterioView misterio in _misterios)
        {
            if (!misterio.IsReal)
            {
                misterio.DestroyMisterio();
            }
        }*/

        for (int i = 0; i < _misterios.Count; i++)
        {
            if (!_misterios[i].IsReal)
            {
                _misterios[i].DestroyMisterio();
                i = 0;
            }
        }
    }

    public void RemoveAllMisterio()
    {
        _misterios.Clear();
    }
}
