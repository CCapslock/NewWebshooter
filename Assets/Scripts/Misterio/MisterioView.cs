using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisterioView : MonoBehaviour
{
    [SerializeField] private bool isReal;
    [SerializeField] private MisterioState _state;
    [SerializeField] private int _currentPhase;
    [SerializeField] private int _maxPhase;
    [SerializeField] private GameObject _misterioClone;
    public MisterioState State => _state;


    public bool IsReal => isReal;


    public void Awake()
    {
        if (MisterioController.Current == null)
        {
            MisterioController.Current = new MisterioController();
        }
        MisterioController.Current.AddMisterioToList(this);
        SetState(MisterioState.Awaiting);
    }

    public void Update()
    {
        if (isReal)
        {
            MisterioController.Current.Execute();
        }
    }

    public void SetState(MisterioState state)
    {
        _state = state;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        { 
            
        }
    }

    public void OnDestroy()
    {
        MisterioController.Current.RemoveMisterioFromList(this);
    }
}
