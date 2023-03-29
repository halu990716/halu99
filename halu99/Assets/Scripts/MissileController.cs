using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    // �̻����� ���󰡴� �ӵ�
    private float Speed;

    // �̻����� ������
    private int Damage;

    // �̻����� �浹�� Ƚ��
    private int MissileHp;

    // ����Ʈȿ�� ����
    public GameObject fxPrefab;

    void Start()
    {
        // �ӵ��� �ʱⰪ
        Speed = 15.0f;

        // �������� �ʱⰪ
        Damage = ControllerManager.GetInstance().MissileDamage;

        // �̻��� ü���� �ʱⰪ
        MissileHp = ControllerManager.GetInstance().MissileHp;
    }

    void Update()
    {
        transform.position += new Vector3(0.0f, Speed, 0.0f) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("123");

        // �浹Ƚ�� ����.
        --MissileHp;

        // ** collision = �浹�� ���.
        // ** �浹�� ����� �����Ѵ�.
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);

        // ** �Ѿ��� �浹 Ƚ���� 0�� �Ǹ� �Ѿ� ����.
        if (MissileHp == 0)
        {
            // ����Ʈȿ�� ����.
            GameObject Obj = Instantiate(fxPrefab);

            // ����Ʈ ȿ���� ��ġ�� ����
            Obj.transform.position = transform.position;

            Destroy(this.gameObject, 0.016f);
        }
    }

}