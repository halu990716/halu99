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

    // ** Enemy�� �θ� ��ü
    private GameObject Parent;

    private GameObject prefab;

    public GameObject BossHpBar;
    public GameObject BossAppear;

    private int rand;

    private float Wait;

    private bool Boss;
    private bool Bossappear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;;

            rand = 1;

            Wait = 1.5f;

            Boss = false;
            Bossappear = false;

            // ** ���� ����Ǿ ��� ������ �� �ְ� ���ش�.
            //DontDestroyOnLoad(gameObject);

            // ** �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
            Parent = new GameObject("EnemyList");
        }
    }

    // ** �������ڸ��� Start�Լ��� �ڷ�ƾ �Լ��� ����
    private IEnumerator Start()
    {
        while (true)
        {
            for (int i = 0; i <= 100; ++i)
            {
                rand = Random.Range(Enemy_1_A, Enemy_1_D + 1);

                if (i == 100 && !Boss)
                {
                    Boss = true;

                    prefab = Resources.Load("Prefabs/Enemy/Enemy_2_A") as GameObject;

                    GameObject Obj = Instantiate(prefab);

                    Obj.transform.position = new Vector3(
                        0.0f, 25.0f, 0.0f);

                    Obj.transform.name = "Boss";

                    Obj.transform.parent = Parent.transform;

                    BossHpBar.SetActive(true);

                    StartCoroutine(bossAppear());

                    Wait = 3.0f;
                }
                else
                {
                    switch (rand)
                    {
                        case Enemy_1_A:

                            // ** Enemy�� ����� ���� ��ü
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

                    // ** Enemy ������ü�� �����Ѵ�.
                    GameObject Obj = Instantiate(prefab);

                    // ** Enemy �۵� ��ũ��Ʈ ����.
                    //Obj.AddComponent<EnemyController>();

                    // ** Ŭ���� ��ġ�� �ʱ�ȭ.
                    Obj.transform.position = new Vector3(
                        Random.Range(-8.0f, 8.0f), Random.Range(20.0f, 25.0f), 0.0f);




                    // ** Ŭ���� �̸� �ʱ�ȭ
                    Obj.transform.name = "Enemy";

                    // ** Ŭ���� �������� ����.
                    Obj.transform.parent = Parent.transform;
                }
                ControllerManager.GetInstance().EnemyHp++;

                if (Wait >= 0.5f && !Boss)
                    Wait -= 0.1f;

                // ** 1.5�� �޽�.
                yield return new WaitForSeconds(Random.Range(Wait, Wait +0.3f));
            }
        }
    }

    private void Update()
    {
        if (ControllerManager.GetInstance().BossDie)
            Destroy(gameObject, 0.016f);
    }

    IEnumerator bossAppear()
    {
        for (int i = 0; i < 6; ++i)
        {
            BossAppear.SetActive(!Bossappear);

            Bossappear = !Bossappear;

            yield return new WaitForSeconds(0.3f);
        }
    }
}
