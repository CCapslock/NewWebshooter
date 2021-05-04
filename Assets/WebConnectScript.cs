using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebConnectScript : MonoBehaviour
{
    [SerializeField] private Transform ThrowingJoint;
    [SerializeField] private Transform SavedJoint;
    [SerializeField] private SkinnedMeshRenderer _render;
    private Transform _connectedHand;
    private Vector3 _myStaticPosition;
    private bool _connected = false;

    
    private void Awake()
    {        
        _myStaticPosition = SavedJoint.position += Vector3.up*6+Vector3.right*2+Vector3.forward*5;
        SavedJoint.position = _myStaticPosition;
        ThrowingJoint.rotation = Quaternion.LookRotation(ThrowingJoint.position - SavedJoint.position);
        SavedJoint.rotation = ThrowingJoint.rotation;
        _render.enabled = true;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        
        ThrowingJoint.rotation = Quaternion.LookRotation(ThrowingJoint.position - SavedJoint.position);
        SavedJoint.rotation = ThrowingJoint.rotation;
        SavedJoint.position = _myStaticPosition;       
        //ThrowingJoint.LookAt(SavedJoint.position);
        //SavedJoint.LookAt(ThrowingJoint.position);
        
    }

    public void ThrowJoint()
    {
        
    }

    public void ConnectToHand(Transform hand)
    {        
        _connectedHand = hand;
        if (_connectedHand != null)
            _connected = true;        
    }

    public void DisconnectFromHand()
    {        
        _connectedHand = null;
        _connected = false;
        Destroy(this.gameObject);
    }
}
