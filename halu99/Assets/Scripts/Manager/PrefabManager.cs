using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefabManager
{
    // �ν��Ͻ� ����
    public static PrefabManager instans { get; } = new PrefabManager();

    // ������ �����
    private Dictionary<string, GameObject> prototypeObjectList = new Dictionary<string, GameObject>();

    private PrefabManager()
    {
        // �����͸� �ҷ��´�
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");

        // �ҷ��� �����͸� Dictionary�� ����.
        foreach (GameObject element in prefabs)
            prototypeObjectList.Add(element.name, element);

    }

    // �ܺο��� �������� Prefab�� ������ �� �ֵ��� Getter�� ����.
    public GameObject getprefabByName(string Key)
    {
        // ���࿡ Key�� ���� �Ѵٸ� ���� ��ü�� ��ȯ�ϰ�
        if (prototypeObjectList.ContainsKey(Key))
            return prototypeObjectList[Key];

        // �׷��� ���������� null
        return null;
    }
}