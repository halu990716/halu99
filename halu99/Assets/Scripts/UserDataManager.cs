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
    public int player1, player2, player3, player4;
    public int bastTime, secondTime, thirdTime;
    public string bastName, secondName, thirdName;
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

        UD = JsonUtility.FromJson<UserData>(json);

        if (getData)
        {
            getData = false;

            ControllerManager.GetInstance().Coin = UD.coin;

            ControllerManager.GetInstance().ClearTime = UD.time;

            ControllerManager.GetInstance().player1 = UD.player1; 
            ControllerManager.GetInstance().player2 = UD.player2;
            ControllerManager.GetInstance().player3 = UD.player3;
            ControllerManager.GetInstance().player4 = UD.player4;
        }

        ControllerManager.GetInstance().BastUserName = UD.bastName;
        ControllerManager.GetInstance().BastClearTime = UD.bastTime;

        ControllerManager.GetInstance().SecondUserName = UD.secondName;
        ControllerManager.GetInstance().SecondClearTime = UD.secondTime;

        ControllerManager.GetInstance().ThirdName = UD.thirdName;
        ControllerManager.GetInstance().ThirdClearTime = UD.thirdTime;
    }
    
    public void IDData()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getData");
        form.AddField("index", ControllerManager.GetInstance().Index);

        getData = true;
        StartCoroutine(Post(form));
        StartCoroutine(updateData());
    }

    public void upData()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setData");
        form.AddField("coin", ControllerManager.GetInstance().Coin);
        form.AddField("time", ControllerManager.GetInstance().ClearTime);
        form.AddField("name", ControllerManager.GetInstance().UserName);

        UD.coin = ControllerManager.GetInstance().Coin;
        UD.time = ControllerManager.GetInstance().ClearTime;

        UD.player1 = ControllerManager.GetInstance().player1;
        UD.player2 = ControllerManager.GetInstance().player2;
        UD.player3 = ControllerManager.GetInstance().player3;
        UD.player4 = ControllerManager.GetInstance().player4;

        StartCoroutine(Post(form));
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
            form.AddField("name", ControllerManager.GetInstance().UserName);

            UD.coin = ControllerManager.GetInstance().Coin;
            UD.time = ControllerManager.GetInstance().ClearTime;

            UD.player1 = ControllerManager.GetInstance().player1;
            UD.player2 = ControllerManager.GetInstance().player2;
            UD.player3 = ControllerManager.GetInstance().player3;
            UD.player4 = ControllerManager.GetInstance().player4;

            StartCoroutine(Post(form));
        }
    }
}
