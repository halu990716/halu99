using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    const int Ship_1_A = 1;
    const int Ship_1_B = 2;
    const int Ship_1_C = 3;
    const int Ship_1_D = 4;

    private int Player_List;

    // ** �����̴� �ӵ�
    private float Speed;

    // ���ݼӵ�
    private float AttackSpeed;

    private int AttackCount;

    // ī�޶� ����
    private float minX, maxX, minY, maxY;

    // �÷��̾ ���������� �ٶ� ����.
    private float Direction;

    // �ǰ��� �����Ѱ�
    private bool WaitHit;

    // ü��
    [HideInInspector] public int HP;

    private bool onAttack; // ���ݻ���

    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� Animator ������Ҹ� �޾ƿ��� ����...
    private Animator Ani;


    // �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ��� ����...
    private SpriteRenderer playerRenderer;

    private GameObject Parent;
    // ������ �̻��� ����
    private GameObject MissilePrefab;

    // ������ �Ѿ��� �������.
    private List<GameObject> Missile = new List<GameObject>();

    private void Awake()
    {
        Player_List = ControllerManager.GetInstance().Player_List;
        Parent = new GameObject("Missile");

        // player �� spriteRenderer �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        switch (Player_List)
        {
            case Ship_1_A:
                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_A") as GameObject;
                ControllerManager.GetInstance().Player_HP += 2;

                break;

            case Ship_1_B:
                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_B") as GameObject;
                ControllerManager.GetInstance().MissileHp++;

                break;

            case Ship_1_C:
                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_C") as GameObject;
                ControllerManager.GetInstance().MissileDamage += 10;

                break;

            case Ship_1_D:
                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_D") as GameObject;
                ControllerManager.GetInstance().AttackSpeed -= 0.3f;

                break;
        }

        Ani = this.GetComponent<Animator>();
    }

    void Start()
    {
        //  �ӵ��� �ʱ�ȭ.
        Speed = 10.0f;

        WaitHit = false;
        // ** �ʱⰪ ����
        onAttack = false;

        Direction = 1.0f;

        minX = -Camera.main.orthographicSize * Camera.main.aspect - 5.5f;
        maxX = Camera.main.orthographicSize * Camera.main.aspect + 5.5f;
        minY = -Camera.main.orthographicSize - 9.0f;
        maxY = Camera.main.orthographicSize + 10.0f;

    }

    void Update()
    {
        HP = ControllerManager.GetInstance().Player_HP;
        AttackSpeed = ControllerManager.GetInstance().AttackSpeed;
        AttackCount = ControllerManager.GetInstance().AttackCount;

        if (AttackSpeed > 0.1f)
        {
            AttackSpeed -= (AttackCount / 10);
        }

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.

        // Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�.
        if (Hor != 0)
            Direction = Hor;

        // �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);

        transform.position += Movement;

        if (Hor > 0.0f)
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, -10.0f);
        else if(Hor < 0.0f)
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 10.0f);
        else
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        if (!onAttack)
        {
            StartCoroutine(OnAttack());

            onAttack = true;
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY));
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(AttackSpeed);

        for (int i = 0; i < AttackCount; ++i)
        {
            yield return new WaitForSeconds(0.1f);

            // ** �Ѿ˿����� �����Ѵ�.
            GameObject Obj = Instantiate(MissilePrefab);

            // ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            Obj.transform.position = transform.position;
            Obj.transform.name = "Missile";
            Obj.transform.parent = Parent.transform;

            // ** �Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�.
            //BulletController Controller = Obj.AddComponent<BulletController>();

            // ** �Ѿ� ��ũ��Ʈ������ FX Prefab�� �����Ѵ�.
            //Controller.fxPrefab = fxPrefab;

            // ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Missile.Add(Obj);
        }

        onAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && !WaitHit)
        {
            OnHit();
        }

        if (collision.transform.tag == "EnemyMissile" && !WaitHit)
        {
            OnHit();
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Enemy" && !WaitHit)
    //    {
    //        OnHit();
    //    }

    //    if (collision.transform.tag == "EnemyMissile" && !WaitHit)
    //    {
    //        OnHit();
    //    }
    //}

    IEnumerator WaitHIT()
    {
        yield return new WaitForSeconds(0.5f);

        WaitHit = false;
    }

    private void OnHit()
    {
        Ani.SetTrigger("HIT");
        WaitHit = true;
        HP--;
        ControllerManager.GetInstance().Player_HP = HP;

        StartCoroutine(WaitHIT());
    }
}
