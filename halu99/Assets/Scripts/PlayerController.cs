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

    private float AttackSpeed;

    [HideInInspector] public int HP;

    private bool onAttack; // ���ݻ���

    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ��� ����...
    private SpriteRenderer playerRenderer;

    // ������ �̻��� ����
    private GameObject MissilePrefab;

    // ������ ����Ʈ
    private GameObject fxPrefab;

    // ������ ��׶��� ����
    private GameObject backGround;

    // ������ ��׶����� �������.
    public List<GameObject> stageBack = new List<GameObject>();

    // ������ �Ѿ��� �������.
    private List<GameObject> Missile = new List<GameObject>();

    private void Awake()
    {
        Player_List = ControllerManager.GetInstance().Player_List;
        AttackSpeed = ControllerManager.GetInstance().AttackSpeed;

        // player �� spriteRenderer �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        switch (Player_List)
        {
            case Ship_1_A:

                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_A") as GameObject;

                break;

            case Ship_1_B:

                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_B") as GameObject;

                break;

            case Ship_1_C:

                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_C") as GameObject;

                break;

            case Ship_1_D:

                MissilePrefab = Resources.Load("Prefabs/Player/Missile/Missile_D") as GameObject;

                break;
        }
    }

    void Start()
    {
        //  �ӵ��� �ʱ�ȭ.
        Speed = 7.0f;

        // ** �ʱⰪ ����
        onAttack = false;
    }

    void Update()
    {
        HP = ControllerManager.GetInstance().Player_HP;

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.

        // �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);

        transform.position += Movement;

        if (!onAttack)
        {
            onAttack = true;
            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(AttackSpeed);

        // ** �Ѿ˿����� �����Ѵ�.
        GameObject Obj = Instantiate(MissilePrefab);

        // ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
        Obj.transform.position = transform.position;

        // ** �Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�.
        //BulletController Controller = Obj.AddComponent<BulletController>();

        // ** �Ѿ� ��ũ��Ʈ������ FX Prefab�� �����Ѵ�.
        //Controller.fxPrefab = fxPrefab;

        // ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
        Missile.Add(Obj);

        onAttack = false;
    }
}