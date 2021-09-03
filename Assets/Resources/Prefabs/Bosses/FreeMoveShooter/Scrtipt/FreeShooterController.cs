using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{ 
    Round,
    Quad
}

public class FreeShooterController : MonoBehaviour, IBoss
{
    [SerializeField] public PlayerMovement Player;

    [SerializeField] private GameObject SquareGround;
    [SerializeField] private GameObject CircleGround;
    public void AwakeBoss()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>();
        if (MainGameController.BossContainter == null)
        {
            MainGameController.BossContainter = this;
        }


        SetTrueInputController();
            
    }

    private void SetTrueInputController()
    {
        var obj = FindObjectOfType<InputController>().gameObject;
        Destroy(obj.GetComponent<InputController>());
        obj.AddComponent<TrueInputController>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
