using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour
{
    private GameObject Target;
    private GameObject Parent;
    public GameObject prefab;

    private Vector3 Movement;
    private Vector3 EndPoint;

    private int Hp;

    private float Speed;
    private float Distance;

    private void Awake()
    {
        Target = GameObject.Find("Player");
        Parent = GameObject.Find("EnemyList");
        //prefab = Resources.Load("Prefabs/FXHit") as GameObject;

        // ** 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착지점으로 셋팅
        EndPoint = Target.transform.position;

        Distance = Vector3.Distance(EndPoint, transform.position);

        Hp = 1;
        Speed = 17.0f;

    }

    private void Start()
    {
        Vector3 Direction = (EndPoint - transform.position).normalized;

        Movement = new Vector3(
                Speed * Direction.x,
                Speed * Direction.y,
                0.0f);
    }

    private void Update()
    {
        transform.position += Movement * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hp--;
        if (collision.transform.tag == "Player")
            SoundManager.Instance.soundManager("EnemyMissile");

        if (Hp <= 0)
        {
            Destroy(gameObject, 0.016f);
        }

        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);

        GameObject Obj = Instantiate(prefab);

        Obj.transform.parent = Parent.transform;
        Obj.transform.position = transform.position;
        Obj.transform.name = "Hit";

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Hp--;

    //    if (Hp <= 0)
    //    {
    //        Destroy(gameObject, 0.016f);
    //    }
    //}

}
