using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BossController;

public class BossController : MonoBehaviour
{
    public enum Pattern
    {
        DelayScrew,
        Explosion,
        ScrewPattern
    };

    private GameObject Target;

    private Animator Ani;

    private Sprite sprite;

    private Vector3 Movement;

    private List<GameObject> MissiletList = new List<GameObject>();
    private GameObject BossMissile;
    private GameObject EnemyMissile;
    private GameObject EnemyManager;
    private GameObject Timer;

    private int HP;
    private int Rand;
    private float Speed;
    private float WaitPattern;

    private float PatternPosition;
    private bool pattern;

    private void Awake()
    {
        Target = GameObject.Find("Player");
        EnemyManager = GameObject.Find("EnemyManager");
        Timer = GameObject.Find("Timer");
        BossMissile = Resources.Load("Prefabs/Enemy/Missile/BossMissile") as GameObject;
        EnemyMissile = Resources.Load("Prefabs/Enemy/Missile/Missile2") as GameObject;

        Ani = GetComponent<Animator>();

        Speed = 15.0f;
        PatternPosition = 14.0f;

        pattern = false;

        WaitPattern = UnityEngine.Random.Range(7.0f, 10.0f);

        Movement = new Vector3(0.0f, Speed, 0.0f);
    }

    private void Start()
    {
        HP = ControllerManager.GetInstance().BossHp;
        StartCoroutine(TwistPattern());
        StartCoroutine(BossPattern());

    }

    private void Update()
    {
        Move();
    }

    private void Move() 
    {
        if (transform.position.y > PatternPosition && !pattern)
            transform.position -= Movement * Time.deltaTime;
        if (transform.position.y < PatternPosition && pattern)
            transform.position += Movement * Time.deltaTime;
    }

    private IEnumerator BossPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitPattern);

            Pattern rand = (Pattern)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(Pattern)).Length));
            
            switch (rand)
            {
                case Pattern.DelayScrew:
                    StartCoroutine(GetDelayScrewPattern());
                    break;

                case Pattern.Explosion:
                    StartCoroutine(ExplosionPattern(5.0f, (int)(360 / 5.0f)));
                    StartCoroutine(PatternPositionWait());
                    PatternPosition = 0.0f;
                    pattern = false;
                    break;

                case Pattern.ScrewPattern:
                    StartCoroutine(GetScrewPattern());
                    break;
            }
        }
    }

    private IEnumerator GetScrewPattern(bool _option = false)
    {
        for (int j = 0; j < 3; j++)
        {
            float rand = UnityEngine.Random.Range(7.0f, 10.0f);
            float _angle = rand;

            int _count = (int)(360 / rand);
            for (int i = 0; i < _count; ++i)
            {
                GameObject Obj = Instantiate(BossMissile);
                BossMissileController controller = Obj.GetComponent<BossMissileController>();

                controller.Option = _option;

                _angle += rand;

                controller.Direction = new Vector3(
                    Mathf.Cos(_angle * 3.141592f / 180),
                    Mathf.Sin(_angle * 3.141592f / 180),
                    0.0f) * 5;


                Obj.transform.position = transform.position;
                Obj.transform.parent = EnemyManager.transform;

                MissiletList.Add(Obj);

                yield return new WaitForSeconds(0.016f);
            }
        }
    }

    private IEnumerator GetDelayScrewPattern()
    {
        float rand = UnityEngine.Random.Range(30.0f, 33.0f);

        float fAngle = rand;

        int iCount = (int)(360 / fAngle);

        int i = 0;

        while (i < (iCount) * 3)
        {
            GameObject Obj = Instantiate(BossMissile);
            BossMissileController controller = Obj.GetComponent<BossMissileController>();

            controller.Option = false;

            fAngle += 30.0f;


            controller.Direction = new Vector3(
                Mathf.Cos(fAngle * Mathf.Deg2Rad),
                Mathf.Sin(fAngle * Mathf.Deg2Rad),
                0.0f) * 5 + transform.position;


            Obj.transform.position = transform.position;
            Obj.transform.parent = EnemyManager.transform;

            MissiletList.Add(Obj);
            ++i;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public IEnumerator TwistPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitPattern * 0.5f);
            float fTime = 3.0f;
            float rand = UnityEngine.Random.Range(-8.0f, 8.0f);

            GameObject obj = Instantiate(Resources.Load("Prefabs/Enemy/Missile/Twist")) as GameObject;

            obj.transform.parent = EnemyManager.transform;
            obj.transform.position = new Vector3(
                rand,
                9.0f,
                0.0f);

            GameObject[] Bullet = new GameObject[2];
            GameObject TwistMissile = Resources.Load("Prefabs/Enemy/Missile/TwistMissile") as GameObject;


            while (fTime > 0)
            {
                fTime -= Time.deltaTime;

                TwistMissile.GetComponent<TwistMissileController>().Twist = true;

                Bullet[0] = Instantiate(TwistMissile);

                Bullet[0].transform.SetParent(transform);

                Bullet[0].transform.parent = EnemyManager.transform;

                Bullet[0].transform.position = new Vector3(
                obj.transform.position.x - 1.25f,
                obj.transform.position.y,
                0.0f);


                TwistMissile.GetComponent<TwistMissileController>().Twist = false;

                Bullet[1] = Instantiate(TwistMissile);

                Bullet[1].transform.SetParent(transform);

                Bullet[1].transform.parent = EnemyManager.transform;

                Bullet[1].transform.position = new Vector3(
                    obj.transform.position.x + 1.25f,
                    obj.transform.position.y,
                    0.0f);
                yield return null;
            }
            Destroy(obj.gameObject);
        }
    }

    public IEnumerator ExplosionPattern(float _angle, int _count, bool _option = true)
    {
        yield return new WaitForSeconds(1.0f);

        GameObject ParentObj = new GameObject("Missile");

        //parentObj.AddComponent<MyGizmo>();
        SpriteRenderer renderer = ParentObj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        BossMissileController controll = ParentObj.AddComponent<BossMissileController>();

        controll.Option = _option;

        //controll.Direction = (GameObject.Find("Player").transform.position - transform.position);


        ParentObj.transform.position = transform.position;

        yield return new WaitForSeconds(0.5f);

        Vector3 pos = ParentObj.transform.position;

        Destroy(ParentObj);


        for (int i = 0; i < _count; ++i)
        {
            GameObject Obj = Instantiate(EnemyMissile);

            BossMissileController controller = Obj.GetComponent<BossMissileController>();

            controller.Option = _option;

            _angle += 15.0f;

            controller.Direction = new Vector3(
                Mathf.Cos(_angle * 3.141592f / 180),
                Mathf.Sin(_angle * 3.141592f / 180),
                0.0f) * 15 + transform.position;


            Obj.transform.parent = EnemyManager.transform;
            Obj.transform.position = pos;

            MissiletList.Add(Obj);
        }
    }

    public void GuideBilletPattern()
    {
        GameObject Obj = Instantiate(BossMissile);
        BossMissileController controller = Obj.GetComponent<BossMissileController>();

        controller.Target = GameObject.Find("Player");
        controller.Option = true;

        Obj.transform.position = transform.position;
    }

    private IEnumerator PatternPositionWait()
    {
        yield return new WaitForSeconds(WaitPattern * 0.3f);

        PatternPosition = 14.0f;

        pattern = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            Ani.SetTrigger("Hit");

            HP = HP - ControllerManager.GetInstance().MissileDamage;
            ControllerManager.GetInstance().BossHp = HP;
        }

        Die();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Skill1")
        {
            HP = HP - 10;
            ControllerManager.GetInstance().BossHp = HP;

            Ani.SetTrigger("Hit");
        }

        Die();

    }

    private void Die()
    {
        if (HP <= 0)
        {

            TimerController sTimer = Timer.GetComponent<TimerController>();

            ControllerManager.GetInstance().BossDie = true;

            ControllerManager.GetInstance().ClearTime = sTimer.timer;

            Destroy(gameObject, 0.016f);

        }
    }
}
