using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

[System.Serializable]
public class MemberForm
{
    public int Index;
    public string Email;
    public string Password;

    public MemberForm(int index, string email, string password)
    {
        Index = index;
        Email = email;
        Password = password;
    }
}
// 회원가입
// 로그인


public class ExampleManager : MonoBehaviour
{

    string URL = "https://script.google.com/macros/s/AKfycbwfS_RSV5Z2-up0MuMk6BgwfVbJCgqEJsvn6A5z-y4G79W71B1v4DICkXKie-FptlPE/exec";


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

    public void NextScene()
    {
        SceneManager.LoadScene("progressScenes");
    }

}

