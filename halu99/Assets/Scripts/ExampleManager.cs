using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
// 회원가입
// 로그인


public class ExampleManager : MonoBehaviour
{

    const string URL = "https://script.google.com/macros/s/AKfycbwfS_RSV5Z2-up0MuMk6BgwfVbJCgqEJsvn6A5z-y4G79W71B1v4DICkXKie-FptlPE/exec";
    public InputField EmailInput, PasswordInput;
    string email, password;

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

    bool SetEmailPass()
    {
        email = EmailInput.text.Trim();
        password = PasswordInput.text.Trim();

        if (email == "" || password == "") return false;
        else return true;
    }

    public void Register()
    {
        if (!SetEmailPass())
        {
            print("아이디 또는 비밀번호가 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("email", email);
        form.AddField("password", password);

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                print(www.downloadHandler.text);
            else
                print("웹의 응답이 없습니다.");
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("progressScenes");
    }

}

