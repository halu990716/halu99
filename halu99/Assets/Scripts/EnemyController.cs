using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float Speed;
    private int HP;
    private Animator Ani;
    private Vector3 Movement;
    public GameObject Player;

    private bool Run;

    private void Awake()
    {
        Ani = GetComponent<Animator>();
    }

    private void Start()
    {

        Player = GameObject.Find("Player").gameObject;

        Speed = 0.3f;
        Movement = new Vector3(0.0f, Speed, 0.0f);
        HP = ControllerManager.GetInstance().EnemyHp;

        Run = true;
    }

    private void Update()
    {
        if (Run)
        {
            transform.position -= Movement * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            HP = HP - ControllerManager.GetInstance().MissileDamage;

            Ani.SetTrigger("HIT");
        }
        if (HP <= 0 )
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, 0.016f);
        }
    }
}
