using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    // 미사일이 날라가는 속도
    private float Speed;

    // 미사일의 데미지
    private int Damage;

    // 미사일이 충돌한 횟수
    private int MissileHp;

    // 이펙트효과 원본
    public GameObject fxPrefab;

    void Start()
    {
        // 속도의 초기값
        Speed = 15.0f;

        // 데미지의 초기값
        Damage = ControllerManager.GetInstance().MissileDamage;

        // 미사일 체력의 초기값
        MissileHp = ControllerManager.GetInstance().MissileHp;
    }

    void Update()
    {
        transform.position += new Vector3(0.0f, Speed, 0.0f) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌횟수 차감.
        --MissileHp;

        // ** collision = 충돌한 대상.
        // ** 충돌한 대상을 삭제한다.
        if (collision.transform.tag == "Wall")
            Destroy(this.gameObject, 0.016f);

        // ** 총알의 충돌 횟수가 0이 되면 총알 삭제.
        if (MissileHp == 0)
        {
            // 이펙트효과 복제.
            GameObject Obj = Instantiate(fxPrefab);

            // 이펙트 효과의 위치를 지정
            Obj.transform.position = transform.position;

            Destroy(this.gameObject, 0.016f);
        }
    }

}
