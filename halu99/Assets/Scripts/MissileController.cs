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
        // �浹Ƚ�� ����.
        --MissileHp;

        // ����Ʈȿ�� ����.
        GameObject Obj = Instantiate(fxPrefab);

        // ����Ʈ ȿ���� ��ġ�� ����
        Obj.transform.position = transform.position;

        // ** collision = �浹�� ���.
        // ** �浹�� ����� �����Ѵ�.
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);
        else
        {
            // ** ����ȿ���� ������ ������ ����.
            GameObject camera = new GameObject("CameraController");

            // ** ���� ȿ�� ��Ʈ�ѷ� ����.
            camera.AddComponent<CameraController>();
        }

        // ** �Ѿ��� �浹 Ƚ���� 0�� �Ǹ� �Ѿ� ����.
        if (MissileHp == 0)
        {
            Destroy(this.gameObject, 0.016f);
        }
    }

}
