using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistMissileController : MonoBehaviour
{
    private GameObject Parent;
    private GameObject fxPrefab;

    private float Angle;
    private float Speed;

    public bool Twist;

    private void Awake()
    {
        fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
        Parent = GameObject.Find("EnemyList");
    }

    private void Start()
    {
        Angle = 0.0f;
        Speed = 5.0f;
    }

    private void Update()
    {
        Angle += 230.0f * Time.deltaTime;

        if (Twist)
        {
            transform.position += new Vector3(
                Mathf.Sin(Angle * Mathf.Deg2Rad),
                -1.0f,
                0.0f) * Speed * Time.deltaTime;
        }

        else
        {
            transform.position += new Vector3(
                Mathf.Sin(-Angle * Mathf.Deg2Rad),
                -1.0f,
                0.0f) * Speed * Time.deltaTime;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject Obj = Instantiate(fxPrefab);

            Obj.transform.parent = Parent.transform;
            Obj.transform.position = transform.position;
            Obj.transform.name = "Hit";

            SoundManager.Instance.soundManager("EnemyMissile");

            Destroy(gameObject, 0.016f);
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject, 0.016f);
        }
    }
}
