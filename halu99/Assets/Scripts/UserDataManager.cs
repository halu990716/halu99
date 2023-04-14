using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public  class UserData
{
    public int index, coin, time;
}

public class UserDataManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwNod7Uc2YZnEDkC9kztLuFjMl2Qa5Qjd0YBXZNrHIKPgCXUVDZzz9EzBHUj-JbcYm1zQ/exec";
    public UserData UD;

    string ID;
    bool getData = false;

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

        if(getData)
        {
            getData = false;
            UD = JsonUtility.FromJson<UserData>(json);

            ControllerManager.GetInstance().Coin = UD.coin;

            ControllerManager.GetInstance().ClearTime = UD.time;
        }
    }
    
    public void IDData()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getData");
        form.AddField("index", ControllerManager.GetInstance().Index);

        getData = true;
        StartCoroutine(Post(form));
    }

    public void upData()
    {
        StartCoroutine(updateData());
    }

    public IEnumerator updateData()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);

            WWWForm form = new WWWForm();
            form.AddField("order", "setData");
            form.AddField("coin", ControllerManager.GetInstance().Coin);
            form.AddField("time", ControllerManager.GetInstance().ClearTime);

            UD.coin = ControllerManager.GetInstance().Coin;
            UD.time = ControllerManager.GetInstance().ClearTime;

            StartCoroutine(Post(form));
        }
    }
}
