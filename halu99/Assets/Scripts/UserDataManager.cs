using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserDataManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwNod7Uc2YZnEDkC9kztLuFjMl2Qa5Qjd0YBXZNrHIKPgCXUVDZzz9EzBHUj-JbcYm1zQ/exec";
    public GoogleData GD;

    string ID;

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL,form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                Response(www.downloadHandler.text);
            }
            else
            {
                print("유저 데이터의 응답이 없습니다.");
            }
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json))
            return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            print(GD.order + "을 실행할 수 없습니다.에러 메시지 : " + GD.msg);

            return;
        }
        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);
    }

    public void IDData()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getData");
        form.AddField("id", ControllerManager.GetInstance().ID);


        StartCoroutine(Post(form));
    }
}
