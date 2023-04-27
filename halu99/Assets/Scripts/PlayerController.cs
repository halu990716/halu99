using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int MissileCount;

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
    private GameObject GuidedMissilePrefab;

    private GameObject DieFx;

    // ������ �Ѿ��� �������.
    private List<GameObject> Missile = new List<GameObject>();

    private void Awake()
    {
        Player_List = ControllerManager.GetInstance().Player_List;
        Parent = new GameObject("Missile");
        DieFx = Resources.Load("Prefabs/FX/DieFx") as GameObject;

        // player �� spriteRenderer �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        GuidedMissilePrefab = PrefabManager.instans.getprefabByName("Missile_Hiden");

        switch (Player_List)
        {
            case Ship_1_A:
                MissilePrefab = PrefabManager.instans.getprefabByName("Missile_A");
                ControllerManager.GetInstance().Player_HP += ControllerManager.GetInstance().player1;
                print(ControllerManager.GetInstance().player1);
                break;

            case Ship_1_B:
                MissilePrefab = PrefabManager.instans.getprefabByName("Missile_B");
                ControllerManager.GetInstance().SkillCool = 0.1f + (ControllerManager.GetInstance().player2 * 0.2f);

                break;

            case Ship_1_C:
                MissilePrefab = PrefabManager.instans.getprefabByName("Missile_C");
                ControllerManager.GetInstance().MaxMissileDamage *= 1 + (ControllerManager.GetInstance().player3 * 0.2f);
                ControllerManager.GetInstance().MissileDamage += ControllerManager.GetInstance().player3 * 2;
                ControllerManager.GetInstance().Ship_C = true;

                break;

            case Ship_1_D:
                MissilePrefab = PrefabManager.instans.getprefabByName("Missile_D");
                ControllerManager.GetInstance().AttackSpeed -= 0.0f + (ControllerManager.GetInstance().player4 * 0.1f);

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
        MissileCount = ControllerManager.GetInstance().MissileCount;

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

            if (MissileCount >= 1)
            {
                GameObject Obje = Instantiate(MissilePrefab);

                Obje.transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, 0.0f);
                Obje.transform.name = "Missile";
                Obje.transform.parent = Parent.transform;

                Missile.Add(Obje);
            }

            if (MissileCount >= 2)
            {
                GameObject Objec = Instantiate(MissilePrefab);

                Objec.transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, 0.0f);
                Objec.transform.name = "Missile";
                Objec.transform.parent = Parent.transform;

                Missile.Add(Objec);
            }
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

        if (HP <= 0)
        {
            ControllerManager.GetInstance().Player_Die = true;
            GameObject Obj = Instantiate(DieFx);

            Obj.transform.position = transform.position;

            SoundManager.Instance.soundManager("PlayerDie");

            // ** ����ȿ���� ������ ������ ����.
            GameObject camera = new GameObject("CameraController");

            // ** ���� ȿ�� ��Ʈ�ѷ� ����.
            camera.AddComponent<CameraController>();

            Destroy(gameObject, 0.016f);
        }
    }

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
