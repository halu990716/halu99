using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefabManager
{
    // 인스턴스 생성
    public static PrefabManager instans { get; } = new PrefabManager();

    // 데이터 저장소
    private Dictionary<string, GameObject> prototypeObjectList = new Dictionary<string, GameObject>();

    private PrefabManager()
    {
        // 데이터를 불러온다
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");

        // 불러온 데이터를 Dictionary에 보관.
        foreach (GameObject element in prefabs)
            prototypeObjectList.Add(element.name, element);

    }

    // 외부에서 보관중인 Prefab을 참조할 수 있도록 Getter를 제공.
    public GameObject getprefabByName(string Key)
    {
        // 만약에 Key가 존재 한다면 원형 객체를 반환하고
        if (prototypeObjectList.ContainsKey(Key))
            return prototypeObjectList[Key];

        // 그렇지 않을때에는 null
        return null;
    }
}