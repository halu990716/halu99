using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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

    private string emailPattern = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";

    string id;
    string password;

    bool delaybool = false;

    private void Start()
    {
        checkText.text = "";
    }

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        password = PasswordInput.text.Trim();

        if (id == "" || password == "") return false;
        else return true;
    }

    string Security(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            // ** true
            checkText.text = "password는 필수 입력 값 입니다.";
            Check.SetActive(true);
            return "null";
        }
        else
        {
            // ** 암호화 & 복호화
            // ** false
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }
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

            if (!Regex.IsMatch(id, emailPattern))
            {
                StartCoroutine(delay());

                checkText.text = "email 형식을 다시 확인하세요!";
                Check.SetActive(true);
                return;
            }

            password = Security(PasswordInput.text);

            // ** login
            // ** 쓰레드 방법 존재
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

            if (!Regex.IsMatch(id, emailPattern))
            {
                StartCoroutine(delay());

                checkText.text = "email 형식을 다시 확인하세요!";
                Check.SetActive(true);
                return;
            }

            password = Security(PasswordInput.text);

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

            SceneManager.LoadScene("progressScenes");
        }
    }
}

