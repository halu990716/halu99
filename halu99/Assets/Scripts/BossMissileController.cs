using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileController : MonoBehaviour
{
    public float Speed;

    public GameObject Target = null;

    private int Hp;

    public bool Option;

    // 총알이 충돌한 횟수
    //private int hp;

    // 이펙트효과 원본
    private GameObject fxPrefab;

    private GameObject Parent;

    //private SpriteRenderer spriteRenderer;

    // 총알이 날아가야할 방향
    public Vector3 Direction { get; set; }


    private void Start()
    {
        // 속도 초기값
        Speed = 1.75f;
        Hp = 1;
        // 충돌 횟수를 3으로 지정한다
        //hp = 3;

        fxPrefab = Resources.Load("Prefabs/FX/Boom") as GameObject;
        Parent = GameObject.Find("EnemyList");

        //// ** 벡터의 정규화
        //if (Option)
        //    Direction = (Target.transform.position - transform.position).normalized;
        Direction.Normalize();

        float fAngle = getAngle(Vector3.down, Direction);


        transform.eulerAngles = new Vector3(
            0.0f, 0.0f, fAngle);
    }

    void Update()
    {
        // ** 실시간으로 타겟의 위치를 확인하고 방향을 갱신한다.
        if (Option && Target)
        {
            Direction = (Target.transform.position - transform.position).normalized;

            float fAngle = getAngle(Vector3.down, Target.transform.position);

            transform.eulerAngles = new Vector3(
            fAngle, fAngle, fAngle);
        }

        // 방향으로 속도만큼 위치를 변경
        transform.position -= Direction * Speed * Time.deltaTime;

    }

    // 충동체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌횟수 차감.
        --Hp;

        // 이펙트효과 복제.
        GameObject Obj = Instantiate(fxPrefab);

        // 이펙트 효과의 위치를 지정
        Obj.transform.position = transform.position;

        Obj.transform.parent = Parent.transform;

        // ** collision = 충돌한 대상.
        // ** 충돌한 대상을 삭제한다.
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);

        if (collision.transform.tag == "Player")
            SoundManager.Instance.soundManager("MissileAudio");

        // ** 총알의 충돌 횟수가 0이 되면 총알 삭제.
        if (Hp == 0)
        Destroy(this.gameObject, 0.016f);
    }


    public float getAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.down, to - from).eulerAngles.z;
    }
}

