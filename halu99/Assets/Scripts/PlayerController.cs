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

    private float AttackSpeed;

    private float minX, maxX, minY, maxY;

    [HideInInspector] public int HP;

    private bool onAttack; // 공격상태

    // 움직임을 저장하는 벡터
    private Vector3 Movement;


    // 플레이어의 SpriteRenderer 구성요소를 받아오기 위해...
    private SpriteRenderer playerRenderer;

    // 복사할 미사일 원본
    private GameObject MissilePrefab;

    // 복제된 총알의 저장공간.
    private List<GameObject> Missile = new List<GameObject>();

    private void Awake()
    {
        Player_List = ControllerManager.GetInstance().Player_List;
        AttackSpeed = ControllerManager.GetInstance().AttackSpeed;

        // player 의 spriteRenderer 받아온다.
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
        //  속도를 초기화.
        Speed = 7.0f;

        // ** 초기값 셋팅
        onAttack = false;

        minX = -Camera.main.orthographicSize * Camera.main.aspect - 5.0f;
        maxX = Camera.main.orthographicSize * Camera.main.aspect + 5.0f;
        minY = -Camera.main.orthographicSize - 2.0f;
        maxY = Camera.main.orthographicSize + 3.0f;
    }

    void Update()
    {
        HP = ControllerManager.GetInstance().Player_HP;



        // **  Input.GetAxis =     -1 ~ 1 사이의 값을 반환함. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 셋중에 하나를 반환.

        // 입력받은 값으로 플레이어를 움직인다.
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

    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY));
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(AttackSpeed);

        // ** 총알원본을 본제한다.
        GameObject Obj = Instantiate(MissilePrefab);

        // ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
        Obj.transform.position = transform.position;

        // ** 총알의 BullerController 스크립트를 받아온다.
        //BulletController Controller = Obj.AddComponent<BulletController>();

        // ** 총알 스크립트내부의 FX Prefab을 설정한다.
        //Controller.fxPrefab = fxPrefab;

        // ** 모든 설정이 종료되었다면 저장소에 보관한다.
        Missile.Add(Obj);

        onAttack = false;
    }
   
}
