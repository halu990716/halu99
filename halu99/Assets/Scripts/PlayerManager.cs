using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerManager() { }
    private static PlayerManager instance = null;

    public static PlayerManager GetInstance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    const int Ship_1_A = 1;
    const int Ship_1_B = 2;
    const int Ship_1_C = 3;
    const int Ship_1_D = 4;

    private int Player_List;

    // ** 플레이어의 부모 개체
    //private GameObject Parent;

    private GameObject prefab;


    private void Awake()
    {
        if (instance == null)
        {
            Player_List = ControllerManager.GetInstance().Player_List;

            instance = this;
;
            // ** 생성되는 Player를 담아둘 상위 객체
            //Parent = new GameObject("PlayerParent");

            switch (Player_List)
            {
                case Ship_1_A:
                    // ** Player로 사용할 원형 객체
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_A");

                    break;

                case Ship_1_B:
                    // ** Player로 사용할 원형 객체
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_B");

                    break;

                case Ship_1_C:
                    // ** Player로 사용할 원형 객체
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_C");

                    break;

                case Ship_1_D:
                    // ** Player로 사용할 원형 객체
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_D");

                    break;
            }
        }
    }

    private void Start()
    {
        // ** 플레이어 원형객체를 복제한다.
        GameObject Obj = Instantiate(prefab);

        Obj.transform.position = new Vector3(
            0.0f, -3.0f, 0.0f);

        // ** Player의 이름 초기화
        Obj.transform.name = "Player";

        // ** 클론의 계층구조 설정.
        //Obj.transform.parent = Parent.transform;
    }

    private void Update()
    {
        
    }
}