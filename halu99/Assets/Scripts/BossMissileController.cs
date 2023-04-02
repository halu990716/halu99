using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileController : MonoBehaviour
{
    public float Speed;

    public GameObject Target = null;

    private int Hp;

    public bool Option;

    // �Ѿ��� �浹�� Ƚ��
    //private int hp;

    // ����Ʈȿ�� ����
    private GameObject fxPrefab;

    private GameObject Parent;

    //private SpriteRenderer spriteRenderer;

    // �Ѿ��� ���ư����� ����
    public Vector3 Direction { get; set; }


    private void Start()
    {
        // �ӵ� �ʱⰪ
        Speed = Option ? 0.5f : 1.75f;
        Hp = 1;
        // �浹 Ƚ���� 3���� �����Ѵ�
        //hp = 3;

        fxPrefab = Resources.Load("Prefabs/FX/Boom") as GameObject;
        Parent = GameObject.Find("EnemyList");

        //// ** ������ ����ȭ
        //if (Option)
        //    Direction = (Target.transform.position - transform.position).normalized;
        Direction.Normalize();

        float fAngle = getAngle(Vector3.down, Direction);


        transform.eulerAngles = new Vector3(
            0.0f, 0.0f, fAngle);
    }

    void Update()
    {
        // ** �ǽð����� Ÿ���� ��ġ�� Ȯ���ϰ� ������ �����Ѵ�.
        if (Option && Target)
        {
            Direction = (Target.transform.position - transform.position).normalized;

            float fAngle = getAngle(Vector3.down, Target.transform.position);

            transform.eulerAngles = new Vector3(
            fAngle, fAngle, fAngle);
        }

        // �������� �ӵ���ŭ ��ġ�� ����
        transform.position -= Direction * Speed * Time.deltaTime;

    }

    // �浿ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹Ƚ�� ����.
        --Hp;

        // ����Ʈȿ�� ����.
        GameObject Obj = Instantiate(fxPrefab);

        // ����Ʈ ȿ���� ��ġ�� ����
        Obj.transform.position = transform.position;

        Obj.transform.parent = Parent.transform;

        // ** collision = �浹�� ���.
        // ** �浹�� ����� �����Ѵ�.
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);

        // ** �Ѿ��� �浹 Ƚ���� 0�� �Ǹ� �Ѿ� ����.
        if (Hp == 0)
        Destroy(this.gameObject, 0.016f);
    }


    public float getAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.down, to - from).eulerAngles.z;
    }
}

