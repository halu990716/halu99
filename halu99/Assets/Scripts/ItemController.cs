using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    const int Pickup_1_A = 1;
    const int Pickup_1_B = 2;
    const int Pickup_1_C = 3;
    const int Pickup_1_D = 4;
    const int Pickup_1_Hiden = 5;


    private int rand;

    private float Speed;
    private bool xMove;

    private Vector3 Movement;
    private Vector3 MovementX;
    private Vector3 OldPosition;

    private void Awake()
    {
        rand = ControllerManager.GetInstance().Rand;
    }
    void Start()
    {
        Speed = 1.0f;
        xMove = true;
        OldPosition = transform.position;
        Movement = new Vector3(1.0f, Speed, 0.0f);
        MovementX = new Vector3(-1.0f, Speed, 0.0f);

        StartCoroutine(ItemX());
    }

    void Update()
    {
        if(xMove)
            transform.position -= Movement * Time.deltaTime;
        else
            transform.position -= MovementX * Time.deltaTime;
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
                    if(ControllerManager.GetInstance().MissileHp < 3)
                        ControllerManager.GetInstance().MissileHp++;
                    break;

                case Pickup_1_C:
                    if (ControllerManager.GetInstance().MissileDamage < ControllerManager.GetInstance().MaxMissileDamage)
                    {
                        if (!ControllerManager.GetInstance().Ship_C)
                        {
                            ControllerManager.GetInstance().MissileDamage += 10;
                        }
                        else
                            ControllerManager.GetInstance().MissileDamage += 20;
                    }
                    break;

                case Pickup_1_D:
                    if (ControllerManager.GetInstance().AttackCount < 5)
                        ControllerManager.GetInstance().AttackCount++;
                    break;

                case Pickup_1_Hiden:
                    ControllerManager.GetInstance().MissileCount++;
                    break;
            }

            Destroy(this.gameObject, 0.016f);
        }
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);
    }

    IEnumerator ItemX()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            xMove = !xMove;
        }
    }
}
