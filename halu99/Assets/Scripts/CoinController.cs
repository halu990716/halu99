using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.7f);
    }

    void Update()
    {
        transform.position += new Vector3(0.0f, 1.0f,0.0f) * Time.deltaTime;
    }
}
