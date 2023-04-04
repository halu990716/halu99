using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBoardController : MonoBehaviour
{
    private Animator Ani;

    private void Awake()
    {
        Ani = GetComponent<Animator>();

    }

    void Start()
    {
        
    }

    void Update()
    {
        if(ControllerManager.GetInstance().Player_Die)
            Ani.SetBool("Move", true);

    }
}
