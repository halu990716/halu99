using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour
{
    const int Pickup_1_A = 1;
    const int Pickup_1_B = 2;
    const int Pickup_1_C = 3;
    const int Pickup_1_D = 4;
    const int Hiden = 5;


    private float Speed;
    private int HP;
    private Animator Ani;
    private Vector3 Movement;

    private GameObject Parent;
    private GameObject Missile;
    private GameObject prefab;
    private GameObject Coin;

    private int rand;

    private bool Run;
    private bool Attack;
    private bool HidenB;

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        Missile = Resources.Load("Prefabs/Enemy/Missile/Missile") as GameObject;
        Coin = Resources.Load("Prefabs/UI/Coin") as GameObject;

        rand = 1;
        Attack = false;
        HidenB = false;

        Parent = GameObject.Find("EnemyList");
    }

    private void Start()
    {
        Speed = 17.0f;
        Movement = new Vector3(0.0f, Speed, 0.0f);
        HP = ControllerManager.GetInstance().EnemyHp;

        Run = true;

        StartCoroutine(runstop());
    }

    private void Update()
    {
        if (Run)
        {
            transform.position -= Movement * Time.deltaTime;
        }

        else
        {
            if (!Attack)
            {
                Attack = true;
                StartCoroutine(EnemyAttack());
            }
        }

        Die();

        if (ControllerManager.GetInstance().Player_Die || ControllerManager.GetInstance().BossDie)
            Destroy(gameObject, 0.016f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            HP = HP - ControllerManager.GetInstance().MissileDamage;

            Ani.SetTrigger("HIT");
        }

        if (collision.tag == "Wall")
            Destroy(gameObject, 0.016f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Skill1")
        {
            HP = HP - 10;

            Ani.SetTrigger("HIT");
        }
    }

    IEnumerator EnemyAttack()
    {
        while (true)
        {
            OnEnemyAttack();

            yield return new WaitForSeconds(Random.Range(1.0f, 1.7f));

            rand = Random.Range(0, 2);

            if (rand == 1)
                Movement = new Vector3(Speed, 0.0f, 0.0f);

            else
                Movement = new Vector3(-Speed, 0.0f, 0.0f);

            Run = true;
        }
    }

    IEnumerator runstop()
    {
        yield return new WaitForSeconds(Random.Range(0.7f, 1.0f));

        Run = false;
    }

    private void Die()
    {
        if (HP <= 0)
        {
            rand = Random.Range(Pickup_1_A, Hiden + 4);

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

                case Hiden:
                    rand = Random.Range(Pickup_1_A, Hiden + 1);
                    ControllerManager.GetInstance().Rand = rand;

                    HidenB = true;
                    switch (rand)
                    {
                        case Hiden:
                            prefab = Resources.Load("Prefabs/Player/Item/Pickup_1_Hiden") as GameObject;
                            HidenB = false;

                            break;
                    }
                    break;
            }
            if (rand <= Hiden && !HidenB)
            {
                // ** Enemy 원형객체를 복제한다.
                GameObject Obj = Instantiate(prefab);

                Obj.transform.position = transform.position;
                Obj.transform.parent = Parent.transform;
                Obj.transform.name = "Item";
            }

            GameObject CoinObj = Instantiate(Coin);

            CoinObj.transform.position = transform.position;
            CoinObj.transform.parent = Parent.transform;
            CoinObj.transform.name = "Coin";

            ControllerManager.GetInstance().Coin += 1;

            GetComponent<CapsuleCollider2D>().enabled = false;

            Destroy(gameObject);
        }
    }
    private void OnEnemyAttack()
    {
        GameObject Object = Instantiate(Missile);

        Object.transform.position = transform.position;
        Object.transform.parent = Parent.transform;
        Object.transform.name = "EnemyMissile";
    }
}