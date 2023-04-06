using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileController : MonoBehaviour
{
    private int Damage;

    private float Speed;

    private float Angle;

    public GameObject Target = null;
    public GameObject fxPrefab;

    private Rigidbody2D rb;

    public Vector3 Direction { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Speed = 20.0f;

    }

    void Update()
    {
        if (!Target)
            Target = GameObject.Find("Enemy");

        else if (!Target)
            Target = GameObject.Find("Boss");

        if (Target)
        { 

            Vector2 direction = (Vector2)Target.transform.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * 200.0f;

            rb.velocity = transform.up * Speed;
        }

        else
        {
            transform.position += new Vector3(0.0f, Speed, 0.0f) * Time.deltaTime;
        }
    }

    public float getAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.down, to - from).eulerAngles.z;
    }
}
