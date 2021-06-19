using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFlyingMan : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;
    [SerializeField] private Collider FinalTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] public Collider StarterCollider;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
        SetRagdollState(false);

    }    

    public void SetRagdollState(bool state)
    {
        StarterCollider.enabled = !state;
        _animator.enabled = !state;
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = !state;
            _rigidbodies[i].useGravity = state;
        }
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = state;
        }
        FinalTrigger.enabled = !state;
    }

    public void SetRigidBodiesVelocioty(Vector3 velocity)
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].velocity = velocity;
        }
    }
}
