using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    // BackGround �� ���ִ� ���������� �ֻ��� ��ü(�θ�)
    private Transform parent;

    // Sprite�� �����ϰ� �ִ� �������
    private SpriteRenderer spriteRenderer;

    // �̹���
    public SpriteRenderer sprite;
    public Vector3 OldPosition;

    // ��������
    private float endPoint;

    // ���� ����
    private float exitPoint;

    // �̹��� �̵��ӵ�
    public float Speed;


    // �÷��̾� ����
    //public GameObject player;

    public Transform[] backGround = new Transform[3];
    public float[] offsets = new float[3];
    private float speed;


    // ������ ����
    private Vector3 Movemanet;

    // �̹����� �߾� ��ġ�� ���������� ����� �� �ֵ��� �ϱ� ���� ���濪Ȱ
    private Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

    private void Awake()
    {
        // �θ�ü�� �޾ƿ´�.
        parent = GameObject.Find("BackGround").transform;

        // �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
            backGround[i] = transform.GetChild(i);

        speed = 5.0f;

        offsets[0] = 2.0f;
        offsets[1] = 1.5f;

        offsets[2] = 1.0f;

        Movemanet = Vector3.zero;
        /*
        // ������ҿ� ���Ե� �̹����� �޾ƿ´�.
        sprite = spriteRenderer.sprite;

        // �÷��̾��� �⺻������ �޾ƿ´�.
        //player = GameObject.Find("Player").gameObject;

        // ���������� ����
        endPoint = sprite.bounds.size.y * 0.5f + transform.position.y;

        // ���������� ����
        exitPoint = sprite.bounds.size.y * 0.5f;
        // �̹����� �����Ѵ�.
        GameObject Obj = Instantiate(this.gameObject);
         */

        //sprite = image.sprite;
        OldPosition = new Vector3(0.0f, sprite.bounds.size.y, 0.0f);
    }

    void Update()       
    {
        //movemane = new Vector3(
        //    0.0f,
        //    Speed * Time.deltaTime + offset.y,
        //    0.0f);

        for (int i = 0; i < transform.childCount; ++i)
        {
            backGround[i].position -= new Vector3(0.0f, Time.deltaTime * Movemanet.x * speed * offsets[i], 0.0f);

            if (backGround[i].position.x < -50f)
                backGround[i].position = new Vector3(0.0f, 0.0f, 0.0f);

            endPoint -= Movemanet.y;
        }
        //transform.position -= movemane;
        //endPoint -= movemane.y;

        if(transform.position.y < -20.48f * 2)
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        /*
        // ������ �̹��� ����
        if (endPoint * 10 < endPoint)
        {
            // �̹����� �����Ѵ�.
            GameObject Obj = Instantiate(this.gameObject);

            // ������ �̹����� �θ� �����Ѵ�.
            Obj.transform.parent = parent.transform;

            // ������ �̹����� �̸��� �����Ѵ�.
            Obj.transform.name = transform.name;

            // ������ �̹����� ��ġ�� �����Ѵ�.
            Obj.transform.position = new Vector3(
                endPoint + 25.0f,
                0.0f, 0.0f);

            // ���������� �����Ѵ�
            endPoint += endPoint + 25.0f;
        }
         */

        // ���������� �����Ѵ�
        //endPoint += endPoint + 25.0f;

        // ���������� �����ϸ� �����Ѵ�.
        //if (transform.position.x + (sprite.bounds.size.x * 0.5f) - 2 < exitPoint)
        //{
        //    Destroy(this.gameObject);
        //}
    }

}
