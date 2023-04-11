using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
// ȸ������
// �α���


public class ExampleManager : MonoBehaviour
{

    const string URL = "https://script.google.com/macros/s/AKfycbwfS_RSV5Z2-up0MuMk6BgwfVbJCgqEJsvn6A5z-y4G79W71B1v4DICkXKie-FptlPE/exec";
    public InputField EmailInput, PasswordInput;
    string email, password;

    /*
    IEnumerator Start()
    {
        // ** ��û�� �ϱ����� �۾�.

        MemberForm member = new MemberForm(1,"","");

        WWWForm form = new WWWForm();

        form.AddField("Index", member.Index);
        form.AddField("Email", member.Email);
        form.AddField("Password", member.Password);

        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            // ** ���信 ���� �۾�.

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

            // ** ���信 ���� �۾�.

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
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
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
                print("���� ������ �����ϴ�.");
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("progressScenes");
    }

}

