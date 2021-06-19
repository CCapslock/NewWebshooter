using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            if (MainGameController.BossContainter is FinalZoneView)
            {
                (MainGameController.BossContainter as FinalZoneView).ThrowMan();
            }
        }
    }
}
