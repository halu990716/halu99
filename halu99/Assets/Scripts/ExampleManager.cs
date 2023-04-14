using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Reflection;

[System.Serializable]
public class GoogleData
{
    public string order, result, msg, login;
    public int index;
}
// 회원가입
// 로그인

public class ExampleManager : MonoBehaviour
{

    const string URL = "https://script.google.com/macros/s/AKfycbwfS_RSV5Z2-up0MuMk6BgwfVbJCgqEJsvn6A5z-y4G79W71B1v4DICkXKie-FptlPE/exec";
    public GoogleData GD;

    public GameObject Check;

    public Text checkText;

    public InputField IDInput, PasswordInput;

    string id;
    string password;

    bool delaybool = false;
    /*
    IEnumerator Start()
    {
        // ** 요청을 하기위한 작업.

        MemberForm member = new MemberForm(1,"","");

        WWWForm form = new WWWForm();

        form.AddField("Index", member.Index);
        form.AddField("Email", member.Email);
        form.AddField("Password", member.Password);

        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            // ** 응답에 대한 작업.

            print(request.downloadHandler.text);
        }
    }
    */

    /*
    IEnumerator Start()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            MemberForm json = JsonUtility.FromJson<MemberForm>(request.downloadHandler.text);

            // ** 응답에 대한 작업.

            print(json.Index);
            print(json.Email);
            print(json.Password);
        }
    }
    */

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        password = PasswordInput.text.Trim();

        if (id == "" || password == "") return false;
        else return true;
    }

    public void Register()
    {
        if(!delaybool)
        {
            delaybool = true;

            if (!SetIDPass())
            {
                print("아이디 또는 비밀번호가 비어있습니다");
                StartCoroutine(delay());

                checkText.text = "아이디 또는 비밀번호가 비어있습니다";

                Check.SetActive(true);
                return;
            }

            WWWForm form = new WWWForm();
            form.AddField("order", "register");
            form.AddField(nameof(id), id);
            form.AddField(nameof(password), password);

            StartCoroutine(Post(form));
            StartCoroutine(delay());
        }
    }

    public void Login()
    {
        if (!delaybool)
        {
            delaybool = true;

            if (!SetIDPass())
            {
                print("아이디 또는 비밀번호가 비어있습니다");
                StartCoroutine(delay());

                checkText.text = "아이디 또는 비밀번호가 비어있습니다";

                Check.SetActive(true);
                return;
            }

            WWWForm form = new WWWForm();
            form.AddField("order", "login");
            form.AddField(nameof(id), id);
            form.AddField(nameof(password), password);

            StartCoroutine(Post(form));
            StartCoroutine(delay());
        }
    }

    private void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        GetComponent<UserDataManager>().upData();

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                Response(www.downloadHandler.text);
            else
                print("웹의 응답이 없습니다.");
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) 
            return;

        
        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);

            checkText.text = GD.msg;

            Check.SetActive(true);

            return;
        }

        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

        ControllerManager.GetInstance().Login = GD.login;

        checkText.text = GD.msg;

        Check.SetActive(true);

    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.7f);

        delaybool = false;
    }

    public void checkButton()
    {
        Check.SetActive(false);

        if(GD.login == "true")
        {

            ControllerManager.GetInstance().Index = GD.index;
            ControllerManager.GetInstance().UserName = id;

            GetComponent<UserDataManager>().IDData();
            GetComponent<UserDataManager>().upData();

            SceneManager.LoadScene("progressScenes");
        }
    }
}

