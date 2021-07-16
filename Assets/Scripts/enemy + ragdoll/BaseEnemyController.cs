using System.Collections;
using UnityEngine;

public abstract class BaseEnemyController : MonoBehaviour
{
    public virtual void TurnOnRagdoll() { } 
    public virtual void TurnOffRagdoll() { }
    public virtual void TurnRagdollStucked() { } 
    public virtual void KillEnemy() { }
}