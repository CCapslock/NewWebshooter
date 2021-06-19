using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScaleView : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinalFallingMan"))
        {
            if (MainGameController.BossContainter is FinalZoneView)
                (MainGameController.BossContainter as FinalZoneView).SendScale();
        }
    }
}
