using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimatorController : MonoBehaviour
{
    [SerializeField] private GoblinView _view;
    private GameObject _tempBombObject;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _bombParentTransform;
    

    public void CreateBomb()
    {
        Debug.LogWarning("CreatingBomb");
        _tempBombObject = Instantiate(_bombPrefab, _bombParentTransform.position, Quaternion.identity,_bombParentTransform);
        _tempBombObject.GetComponent<Rigidbody>().isKinematic = true;
        //GameEvents.Current.CreatingBomb();
    }

    public void ThrowBomb()
    {
        Debug.LogWarning("Bomb");
        _tempBombObject.transform.parent = null;
        _tempBombObject.GetComponent<Rigidbody>().isKinematic = false;
        GameEvents.Current.ThrowingBomb(_tempBombObject);
    }
}
