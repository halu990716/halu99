using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    const int Pickup_1_A = 1;
    const int Pickup_1_B = 2;
    const int Pickup_1_C = 3;
    const int Pickup_1_D = 4;


    private float Speed;
    private int HP;
    private Animator Ani;
    private Vector3 Movement;

    public GameObject prefab;

    private int rand;

    private bool Run;

    private void Awake()
    {
        Ani = GetComponent<Animator>();

        rand = 1;
    }

    private void Start()
    {
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
            rand = Random.Range(Pickup_1_A, Pickup_1_D + 5);

            ControllerManager.GetInstance().Rand = rand;

            switch (rand)
            {
                case Pickup_1_A:
                    prefab = Resources.Load("Prefabs/Player/Item/Pickup_1_A") as GameObject;

                    break;

                case Pickup_1_B:
                    prefab = Resources.Load("Prefabs/Player/Item/Pickup_1_B") as GameObject;

                    break;

                case Pickup_1_C:
                    prefab = Resources.Load("Prefabs/Player/Item/Pickup_1_C") as GameObject;

                    break;

                case Pickup_1_D:
                    prefab = Resources.Load("Prefabs/Player/Item/Pickup_1_D") as GameObject;

                    break;
            }

            if (rand <= Pickup_1_D)
            {
                // ** Enemy 원형객체를 복제한다.
                GameObject Obj = Instantiate(prefab);

                Obj.transform.position = transform.position;
                Obj.transform.name = "Item";
            }
            GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, 0.016f);
        }
    }
}