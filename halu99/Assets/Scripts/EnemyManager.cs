using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyManager() { }
    private static EnemyManager instance = null;

    public static EnemyManager GetInstance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    const int Enemy_1_A = 1;
    const int Enemy_1_B = 2;
    const int Enemy_1_C = 3;
    const int Enemy_1_D = 4;

    // ** Enemy의 부모 개체
    private GameObject Parent;

    private GameObject prefab;

    private int rand;

    // ** 플레이어의 누적 이동 거리
    public float Distance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Distance = 0.0f;

            rand = 1;

            // ** 씬이 변경되어도 계속 유지될 수 있게 해준다.
            //DontDestroyOnLoad(gameObject);

            // ** 생성되는 Enemy를 담아둘 상위 객체
            Parent = new GameObject("EnemyList");
        }
    }

    // ** 시작하자마자 Start함수를 코루틴 함수로 실행
    private IEnumerator Start()
    {
        while (true)
        {
            rand = Random.Range(Enemy_1_A, Enemy_1_D + 1);

            switch (rand)
            {
                case Enemy_1_A:

                    // ** Enemy로 사용할 원형 객체
                    prefab = Resources.Load("Prefabs/Enemy/Enemy_1_A") as GameObject;

                    break;

                case Enemy_1_B:

                    prefab = Resources.Load("Prefabs/Enemy/Enemy_1_B") as GameObject;

                    break;

                case Enemy_1_C:

                    prefab = Resources.Load("Prefabs/Enemy/Enemy_1_C") as GameObject;

                    break;

                case Enemy_1_D:

                    prefab = Resources.Load("Prefabs/Enemy/Enemy_1_D") as GameObject;

                    break;
            }

            // ** Enemy 원형객체를 복제한다.
            GameObject Obj = Instantiate(prefab);

            // ** Enemy 작동 스크립트 포함.
            //Obj.AddComponent<EnemyController>();

            // ** 클론의 위치를 초기화.
            Obj.transform.position = new Vector3(
                Random.Range(-13.0f, 13.0f),10.0f, 0.0f);

            


            // ** 클론의 이름 초기화
            Obj.transform.name = "Enemy";

            // ** 클론의 계층구조 설정.
            Obj.transform.parent = Parent.transform;

            
            // ** 1.5초 휴식.
            yield return new WaitForSeconds(1.5f);
        }
    }

}
