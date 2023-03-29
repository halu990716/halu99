using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    private List<Transform> backGround = new List<Transform>();
    private List<Vector3> oldPosition = new List<Vector3>();
    public List<float> offsets = new List<float>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            backGround.Add(transform.GetChild(i));
            oldPosition.Add(backGround[i].position); 
        }

        offsets.Add(0.0625f);

        offsets.Add(0.25f);

        offsets.Add(0.5f);

    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            backGround[i].position -= new Vector3(0.0f, Time.deltaTime * offsets[i], 0.0f);

            if (backGround[i].position.y < -40.96f && i < 2)
                backGround[i].position = new Vector3 (0.0f, oldPosition[i].y, 0.0f);

            if (backGround[i].position.y < -20.48f && i == 2)
                backGround[i].position = new Vector3(0.0f, oldPosition[i].y, 0.0f);

        }
    }
}