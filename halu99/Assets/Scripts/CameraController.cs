using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라의 진동 시간
    private float shakeTime = 0.15f;

    //카메라의 진동 범위
    private Vector3 offset = new Vector3(0.05f, 0.05f, 0.0f);
    private Vector3 OldPosition; //= new Vector3(0.0f, -0.5f, -10.0f);
    // 코루틴 함수 실행
    IEnumerator Start()
    {
        OldPosition = Camera.main.transform.position;
        // 0.15초 동안 실행.
        while (shakeTime > 0.0f)
        {
            shakeTime -= Time.deltaTime;
                
            // 반복문이 실행되는 동안 반복적으로 호출.
            yield return null;

            // 카메라를 진동 범위 만큼 진동시킨다. 
            Camera.main.transform.position = new Vector3(
                Random.Range(OldPosition.x - offset.x, OldPosition.x + offset.x),
                Random.Range(OldPosition.y - offset.y, OldPosition.y + offset.y),
                -30.0f);
        }
        // 반복문이 종료되면 카메라 위치를 다시 원점에 놓는다.
        Camera.main.transform.position = OldPosition;

        // ** 클래스를 종료한다
        Destroy(this.gameObject);
    }
}
