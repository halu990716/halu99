using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileController : MonoBehaviour
{
    private int Damage;
    private int MissileHp;

    private float Speed;

    private float Angle;

    public GameObject Target = null;
    public GameObject fxPrefab;
    private GameObject Parent;

    private Rigidbody2D rb;

    public Vector3 Direction { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Speed = 20.0f;

        Damage = ControllerManager.GetInstance().MissileDamage;
        MissileHp = ControllerManager.GetInstance().MissileHp;

        Parent = GameObject.Find("EnemyList");

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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            // 충돌횟수 차감.
            --MissileHp;

            // 이펙트효과 복제.
            GameObject Obj = Instantiate(fxPrefab);

            // 이펙트 효과의 위치를 지정
            Obj.transform.position = transform.position;
            Obj.transform.parent = Parent.transform;

            Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
            DamageTextManager.Instance.CreateDamageText(pos, Damage);

            //audioSource.PlayOneShot(MissileAudio);
            SoundManager.Instance.soundManager("MissileAudio");

            // ** 진동효과를 생성할 관리자 생성.
            GameObject camera = new GameObject("CameraController");

            // ** 진동 효과 컨트롤러 생성.
            camera.AddComponent<CameraController>();
        }

        // ** 총알의 충돌 횟수가 0이 되면 총알 삭제.
        if (MissileHp == 0)
        {
            Destroy(this.gameObject, 0.016f);
        }
    }
}
