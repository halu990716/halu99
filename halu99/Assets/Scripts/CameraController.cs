using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ī�޶��� ���� �ð�
    private float shakeTime = 0.15f;

    //ī�޶��� ���� ����
    private Vector3 offset = new Vector3(0.05f, 0.05f, 0.0f);
    private Vector3 OldPosition; //= new Vector3(0.0f, -0.5f, -10.0f);
    // �ڷ�ƾ �Լ� ����
    IEnumerator Start()
    {
        OldPosition = Camera.main.transform.position;
        // 0.15�� ���� ����.
        while (shakeTime > 0.0f)
        {
            shakeTime -= Time.deltaTime;
                
            // �ݺ����� ����Ǵ� ���� �ݺ������� ȣ��.
            yield return null;

            // ī�޶� ���� ���� ��ŭ ������Ų��. 
            Camera.main.transform.position = new Vector3(
                Random.Range(OldPosition.x - offset.x, OldPosition.x + offset.x),
                Random.Range(OldPosition.y - offset.y, OldPosition.y + offset.y),
                -15.0f);
        }
        // �ݺ����� ����Ǹ� ī�޶� ��ġ�� �ٽ� ������ ���´�.
        Camera.main.transform.position = OldPosition;

        // ** Ŭ������ �����Ѵ�
        Destroy(this.gameObject);
    }
}
