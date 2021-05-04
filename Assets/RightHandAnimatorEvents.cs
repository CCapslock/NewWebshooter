using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAnimatorEvents : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private GameObject _web;
    private GameObject _tempWeb;
    

    public void ThrowMovementWeb()
    {
        _tempWeb = Instantiate(_web, _hand.position, Quaternion.identity, _hand);
        _tempWeb.GetComponent<WebConnectScript>().ConnectToHand(_hand);
    }

    public void DisconnectMovementWeb()
    {
        _tempWeb.GetComponent<WebConnectScript>().DisconnectFromHand();
    }
}
