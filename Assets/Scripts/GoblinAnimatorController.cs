using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimatorController : MonoBehaviour
{
    [SerializeField] private GoblinView _view;
    private GameObject _tempBombObject;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _bombParentTransform;
    

    private void CreateBomb()
    {
        Debug.LogWarning("CreatingBomb");
        _tempBombObject = Instantiate(_bombPrefab, _bombParentTransform.position, Quaternion.identity, _bombParentTransform);
        //GameEvents.Current.CreatingBomb();
    }

    private void ThrowBomb()
    {
        Debug.LogWarning("Bomb");
    }
}
