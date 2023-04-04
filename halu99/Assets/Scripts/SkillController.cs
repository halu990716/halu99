using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private GameObject fxPrefab;
    private GameObject Parent;

    private void Awake()
    {
        fxPrefab = Resources.Load("Prefabs/FX/ElectricityBall")as GameObject;
        Parent = GameObject.Find("Player");
    }
    void Start()
    {
        StartCoroutine(Skill());
        StartCoroutine(DestroySkill());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Skill()
    {
        SoundManager.Instance.soundManager("Skill");

        while (true)
        {
            // 이펙트효과 복제.
            GameObject Obj = Instantiate(fxPrefab);

            // 이펙트 효과의 위치를 지정
            Obj.transform.position = transform.position;
            Obj.transform.parent = Parent.transform;

            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(3.0f);

        Destroy(this.gameObject, 0.016f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        int Damage = 10;

        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        DamageTextManager.Instance.CreateDamageText(pos, Damage);
    }

}
