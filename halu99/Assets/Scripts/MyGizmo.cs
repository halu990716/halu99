using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color color = Color.red;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawSphere(this.transform.position, 0.2f);
    }
}