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

    // ** �÷��̾��� �θ� ��ü
    //private GameObject Parent;

    private GameObject prefab;


    private void Awake()
    {
        if (instance == null)
        {
            Player_List = ControllerManager.GetInstance().Player_List;

            instance = this;
;
            // ** �����Ǵ� Player�� ��Ƶ� ���� ��ü
            //Parent = new GameObject("PlayerParent");

            switch (Player_List)
            {
                case Ship_1_A:
                    // ** Player�� ����� ���� ��ü
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_A");

                    break;

                case Ship_1_B:
                    // ** Player�� ����� ���� ��ü
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_B");

                    break;

                case Ship_1_C:
                    // ** Player�� ����� ���� ��ü
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_C");

                    break;

                case Ship_1_D:
                    // ** Player�� ����� ���� ��ü
                    prefab = PrefabManager.instans.getprefabByName("Ship_1_D");

                    break;
            }
        }
    }

    private void Start()
    {
        // ** �÷��̾� ������ü�� �����Ѵ�.
        GameObject Obj = Instantiate(prefab);

        Obj.transform.position = new Vector3(
            0.0f, -3.0f, 0.0f);

        // ** Player�� �̸� �ʱ�ȭ
        Obj.transform.name = "Player";

        // ** Ŭ���� �������� ����.
        //Obj.transform.parent = Parent.transform;
    }

    private void Update()
    {
        
    }
}