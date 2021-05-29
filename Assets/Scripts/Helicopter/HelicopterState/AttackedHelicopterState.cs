using System.Collections;
using UnityEngine;

public class AttackedHelicopterState : BaseHelicopterState
{
    private Transform _goblin;
    private GameObject _bomb;
    private bool _goblinAttackHelicopter = true;
    public AttackedHelicopterState(Transform goblin, Transform GoblinStart, GameObject bomb)
    {
        _goblin = goblin;
        _goblin.position = GoblinStart.position;
        _bomb = bomb;
    }

    public override void Execute(HelicopterView view)
    {
        base.Execute(view);
        view.Lopasti.transform.Rotate(0, 6, 0);
        view.BackLopasti.transform.Rotate(6, 0, 0);
        view.AskingPlayerLookAtMe();
        _goblin.position = Vector3.MoveTowards(_goblin.position, view.GoblinEnd.position, 0.6f);
        _goblin.LookAt(view.GoblinEnd.position);
        if ((_goblin.position - view.transform.position).magnitude < 12f)
        {
            if (_goblinAttackHelicopter)
            {
                _goblinAttackHelicopter = false;
                for (int i = 0; i < 3; i++)
                {

                    GameObject tempBomb = GameObject.Instantiate(_bomb, _goblin.position+Vector3.up * 3f + Vector3.right*(1-i), Quaternion.identity);
                    /*Rigidbody rig = _bomb.GetComponent<Rigidbody>();
                    rig.isKinematic = false;
                    rig.useGravity = true;
                    float _AngleInRadians = 30 * Mathf.PI / 180;
                    Vector3 _fromTo = view.transform.position - tempBomb.transform.position;
                    Vector3 _fromToXZ = new Vector3(_fromTo.x, 0f, _fromTo.z);

                    float _xMagnitude = _fromToXZ.magnitude;
                    float _y = _fromTo.y;

                    float _TempVelocity = (Physics.gravity.y * _xMagnitude * _xMagnitude) / (2 * (_y - Mathf.Tan(_AngleInRadians) * _xMagnitude) * Mathf.Pow(Mathf.Cos(_AngleInRadians), 2));
                    _TempVelocity = Mathf.Sqrt(Mathf.Abs(_TempVelocity));
                    rig.velocity = (_fromToXZ.normalized + Vector3.up).normalized * _TempVelocity;*/
                    tempBomb.GetComponent<Bomb>().ThrowBomb(null, view.transform, 0, 0);
                }
            }
        }

    }
}