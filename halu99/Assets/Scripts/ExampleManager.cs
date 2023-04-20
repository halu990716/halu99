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
// ȸ������
// �α���

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
            checkText.text = "password�� �ʼ� �Է� �� �Դϴ�.";
            Check.SetActive(true);
            return "null";
        }
        else
        {
            // ** ��ȣȭ & ��ȣȭ
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
                print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
                StartCoroutine(delay());

                checkText.text = "���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�";

                Check.SetActive(true);
                return;
            }

            if (!Regex.IsMatch(id, emailPattern))
            {
                StartCoroutine(delay());

                checkText.text = "email ������ �ٽ� Ȯ���ϼ���!";
                Check.SetActive(true);
                return;
            }

            password = Security(PasswordInput.text);

            // ** login
            // ** ������ ��� ����
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
                print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
                StartCoroutine(delay());

                checkText.text = "���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�";

                Check.SetActive(true);
                return;
            }

            if (!Regex.IsMatch(id, emailPattern))
            {
                StartCoroutine(delay());

                checkText.text = "email ������ �ٽ� Ȯ���ϼ���!";
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
                print("���� ������ �����ϴ�.");
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) 
            return;

        
        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);

            checkText.text = GD.msg;

            Check.SetActive(true);

            return;
        }

        print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);

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

