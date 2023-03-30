using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    const int Pickup_1_A = 1;
    const int Pickup_1_B = 2;
    const int Pickup_1_C = 3;
    const int Pickup_1_D = 4;


    private int rand;

    private float Speed;

    private Vector3 Movement;

    private void Awake()
    {
        rand = ControllerManager.GetInstance().Rand;
    }
    void Start()
    {
        Speed = 1.0f;
        Movement = new Vector3(0.0f, Speed, 0.0f);
    }

    void Update()
    {
        transform.position -= Movement * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            switch (rand)
            {
                case Pickup_1_A:
                    if (ControllerManager.GetInstance().Player_HP < ControllerManager.GetInstance().Player_MaxHp)
                        ControllerManager.GetInstance().Player_HP++;
                    break;

                case Pickup_1_B:
                    ControllerManager.GetInstance().MissileHp++;
                    break;

                case Pickup_1_C:
                    ControllerManager.GetInstance().MissileDamage++;
                    break;

                case Pickup_1_D:
                    ControllerManager.GetInstance().AttackSpeed *= 0.9f;
                    break;
            }

            Destroy(this.gameObject, 0.016f);
        }
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);
    }
}
