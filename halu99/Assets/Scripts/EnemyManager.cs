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

    private int rand;

    // ** �÷��̾��� ���� �̵� �Ÿ�
    public float Distance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Distance = 0.0f;

            rand = 1;

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
            rand = Random.Range(Enemy_1_A, Enemy_1_D + 1);

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
                Random.Range(-9.0f, 9.0f),10.0f, 0.0f);

            


            // ** Ŭ���� �̸� �ʱ�ȭ
            Obj.transform.name = "Enemy";

            // ** Ŭ���� �������� ����.
            Obj.transform.parent = Parent.transform;

            
            // ** 1.5�� �޽�.
            yield return new WaitForSeconds(1.5f);
        }
    }

}
