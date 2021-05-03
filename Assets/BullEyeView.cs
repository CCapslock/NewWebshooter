using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullEyeView : MonoBehaviour
{
    private GoblinView _goblin;
    public void SetGoblinView(GoblinView goblin)
    {
        _goblin = goblin;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
        {
            _goblin.TakeDamage();
            this.gameObject.SetActive(false);
        }
    }
}
