using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class WebChainingSphereView : MonoBehaviour
{
    [SerializeField] private float _chainingSphereRadius = 2f;
    private List<Rigidbody> enemiesRigidbodies = new List<Rigidbody>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EnemyPart"))
        {
            ReleaseChains(collision.GetContact(0).point);
        }
        else if (collision.collider.CompareTag("ShieldEnemy"))
        {
            ReleaseChains(collision.GetContact(0).point);
        }
        else if (collision.collider.CompareTag("SimpleEnemy"))
        {
            ReleaseChains(collision.GetContact(0).point);
        }
        else if (collision.collider.CompareTag("DodgeEnemy"))
        {
            ReleaseChains(collision.GetContact(0).point);
        }
        else if (collision.collider.CompareTag("ThrowingEnemy"))
        {
            ReleaseChains(collision.GetContact(0).point);
        }
        Debug.Log($"Trigger {collision.transform.tag}");
    }

    private void ReleaseChains(Vector3 point)
    {
        enemiesRigidbodies.Clear();
        var hits = FindChainableEnemies(point);
        Debug.Log($"Hits: {hits.Length}");
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag("ShieldEnemy"))
            {
                var baseController = hits[i].transform.GetComponent<EnemyController>();
                if (baseController.IsEnemyActive)
                {
                    baseController.KillEnemy();
                    if (!enemiesRigidbodies.Contains(baseController.HipsRigidBody))
                    {
                        enemiesRigidbodies.Add(baseController.HipsRigidBody);
                    }
                }
            }
            else if (hits[i].transform.CompareTag("DodgeEnemy"))
            {
                var baseController = hits[i].transform.GetComponent<EnemyController>();
                if (baseController.IsEnemyActive)
                {
                    baseController.KillEnemy();
                    if (!enemiesRigidbodies.Contains(baseController.HipsRigidBody))
                    {
                        enemiesRigidbodies.Add(baseController.HipsRigidBody);
                    }
                }
            }
            else if (hits[i].transform.CompareTag("SimpleEnemy"))
            {
                var baseController = hits[i].transform.GetComponent<EnemyController>();
                if (baseController.IsEnemyActive)
                {
                    baseController.KillEnemy();
                    if (!enemiesRigidbodies.Contains(baseController.HipsRigidBody))
                    {
                        enemiesRigidbodies.Add(baseController.HipsRigidBody);
                    }
                }
            }
            else if (hits[i].transform.CompareTag("ThrowingEnemy"))
            {
                var baseController = hits[i].transform.GetComponent<ThrowingEnemyController>();
                if (baseController.IsEnemyActive)
                {
                    baseController.KillEnemy();
                    if (!enemiesRigidbodies.Contains(baseController.HipsRigidBody))
                    {
                        enemiesRigidbodies.Add(baseController.HipsRigidBody);
                    }
                }
            }
        }
        Debug.Log($"Transforms: {enemiesRigidbodies.Count}");
        if (enemiesRigidbodies.Count > 0)
        {
            ParticlesController.Current.MakeMagicExplosion(point);
            StartCoroutine(DoBlackHole(point, enemiesRigidbodies));
            ///
            /*
            SpringJoint _jo;
            for (int i = 0; i < enemiesRigidbodies.Count; i++)
            {
                _jo = enemiesRigidbodies[i].gameObject.AddComponent<SpringJoint>();
                _jo.connectedBody = enemiesRigidbodies[(i + 1)< enemiesRigidbodies.Count ? i+1:0];
                _jo.spring = 100f;
                _jo.damper = 0;
                //_jo.connectedBody = enemiesRigidbodies[i];

            }
            ///*/
        }
    }

    private IEnumerator DoBlackHole(Vector3 point, List<Rigidbody> list)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j] != null)
                {
                    list[j].AddForce((point - list[j].transform.position) * 100f, ForceMode.Impulse);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i]?.GetComponent<EnemyController>()?.TurnRagdollStucked();
            list[i]?.GetComponent<ThrowingEnemyController>()?.TurnRagdollStucked();
        }
        yield break;
    }

    private RaycastHit[] FindChainableEnemies(Vector3 point)
    {
        var hits = Physics.SphereCastAll(point, _chainingSphereRadius, Vector3.one);
        return hits;
    }
}