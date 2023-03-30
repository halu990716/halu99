using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject Target;

    private Animator Ani;

    private Vector3 Movement;

    private float Speed;
    private int HP;


    private void Awake()
    {
        Target = GameObject.Find("Player");

        Ani = GetComponent<Animator>();

        Speed = 5.0f;

        Movement = new Vector3(0.0f, Speed, 0.0f);
    }

    private void Start()
    {
        HP = ControllerManager.GetInstance().BossHp;
    }

    private void Update()
    {
        Move();
    }

    private void Move() 
    {
        if (transform.position.y >   14.0f)
            transform.position -= Movement * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            Ani.SetTrigger("Hit");

            HP = HP - ControllerManager.GetInstance().MissileDamage;
            ControllerManager.GetInstance().BossHp = HP;

            if (HP <= 0)
            {
                Destroy(gameObject, 0.016f);
            }
        }
    }
}
