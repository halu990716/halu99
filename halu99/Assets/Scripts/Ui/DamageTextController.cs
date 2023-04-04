using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : MonoBehaviour
{
    private bool MoveB;

    private void Awake()
    {
        MoveB = false;
    }
    void Start()
    {
        Destroy(gameObject, 1.0f);

        StartCoroutine(MoveText());
    }

    void Update()
    {
        if (MoveB)
            transform.position += new Vector3(0.0f, 0.1f, 0.0f);
    }

    IEnumerator MoveText()
    {
        yield return new WaitForSeconds(0.7f);

        MoveB = true;

    }
}
