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

    // ** 움직이는 속도
    private float Speed;

    // 공격속도
    private float AttackSpeed;

    private int AttackCount;

    // 카메라 범위
    private float minX, maxX, minY, maxY;

    // 플레이어가 마지막으로 바라본 방향.
    private float Direction;

    // 피격이 가능한가
    private bool WaitHit;

    // 체력
    [HideInInspector] public int HP;

    private bool onAttack; // 공격상태

    // 움직임을 저장하는 벡터
    private Vector3 Movement;

    // 플레이어의 Animator 구성요소를 받아오기 위해...
    private Animator Ani;


    // 플레이어의 SpriteRenderer 구성요소를 받아오기 위해...
    private SpriteRenderer playerRenderer;

    private GameObject Parent;
    // 복사할 미사일 원본
    private GameObject MissilePrefab;

    // 복제된 총알의 저장공간.
    private List<GameObject> Missile = new List<GameObject>();

    private void Awake()
    {
        Player_List = ControllerManager.GetInstance().Player_List;
        Parent = new GameObject("Missile");

        // player 의 spriteRenderer 받아온다.
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
        //  속도를 초기화.
        Speed = 10.0f;

        WaitHit = false;
        // ** 초기값 셋팅
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

        // **  Input.GetAxis =     -1 ~ 1 사이의 값을 반환함. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 셋중에 하나를 반환.

        // Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다.
        if (Hor != 0)
            Direction = Hor;

        // 입력받은 값으로 플레이어를 움직인다.
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

            // ** 총알원본을 본제한다.
            GameObject Obj = Instantiate(MissilePrefab);

            // ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
            Obj.transform.position = transform.position;
            Obj.transform.name = "Missile";
            Obj.transform.parent = Parent.transform;

            // ** 총알의 BullerController 스크립트를 받아온다.
            //BulletController Controller = Obj.AddComponent<BulletController>();

            // ** 총알 스크립트내부의 FX Prefab을 설정한다.
            //Controller.fxPrefab = fxPrefab;

            // ** 모든 설정이 종료되었다면 저장소에 보관한다.
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
