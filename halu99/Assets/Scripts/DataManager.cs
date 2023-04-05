using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

[System.Serializable]
class DataForm
{
    public string BastName;
    public string BastClearTime;

    public string SecondName;
    public string SecondClearTime;

    public string ThirdName;
    public string ThirdClearTime;

    public DataForm(string _name, string _clearTime, string _secondName,
        string _secondClearTime, string _thirdName, string _thirdClearTime)
    {
        BastName = _name;
        BastClearTime = _clearTime;

        SecondName = _secondName;
        SecondClearTime = _secondClearTime;

        ThirdName = _thirdName;
        ThirdClearTime = _thirdClearTime;
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private string bastUserName;
    private int bastClearTime;

    private string secondUserName;
    private int secondClearTime;

    private string thirdUserName;
    private int thirdClearTime;

    private GameObject ClearBoard;

    private void Start()
    {
        //var jsonData = Resources.Load<TextAsset>("saveFile/Data");

        // ¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é¡é
        // public override int Read( Span<byte> buffer );
        // public override int Read( byte[] array, int offset, int count );
        ///halu99/UnityBuild
        byte[] vs = new byte[0];

        FileStream fileStream = new FileStream(
            Application.dataPath + "/Resources/saveFile/Data.json", FileMode.Open);
        
        var jsonData = fileStream.Read(vs, 0, vs.Length);


        DataForm form = JsonUtility.FromJson<DataForm>(jsonData.ToString());

        //bastUserName = "1";
        //bastClearTime = 1234;

        //secondUserName = "2";
        //secondClearTime = 2345;

        //thirdUserName = "3";
        //thirdClearTime = 3456;

        bastUserName = form.BastName;
        bastClearTime = int.Parse(form.BastClearTime);

        secondUserName = form.SecondName;
        secondClearTime = int.Parse(form.SecondClearTime);

        thirdUserName = form.ThirdName;
        thirdClearTime = int.Parse(form.ThirdClearTime);

        ControllerManager.GetInstance().BastUserName = bastUserName;
        ControllerManager.GetInstance().BastClearTime = bastClearTime;

        ControllerManager.GetInstance().SecondUserName = secondUserName;
        ControllerManager.GetInstance().SecondClearTime = secondClearTime;

        ControllerManager.GetInstance().ThirdName = thirdUserName;
        ControllerManager.GetInstance().ThirdClearTime = thirdClearTime;
    }

    void Update()
    {
        if (ControllerManager.GetInstance().UpDateRank)
        {
            ControllerManager.GetInstance().UpDateRank = false;

            if (bastClearTime > ControllerManager.GetInstance().ClearTime)
            {
                thirdUserName = secondUserName;
                thirdClearTime = secondClearTime;

                secondUserName = bastUserName;
                secondClearTime = bastClearTime;

                bastUserName = ControllerManager.GetInstance().UserName;
                bastClearTime = ControllerManager.GetInstance().ClearTime;
                SaveData(bastUserName, bastClearTime.ToString(), secondUserName,
                    secondClearTime.ToString(), thirdUserName, thirdClearTime.ToString());
            }

            else if (secondClearTime > ControllerManager.GetInstance().ClearTime)
            {
                thirdUserName = secondUserName;
                thirdClearTime = secondClearTime;

                secondUserName = ControllerManager.GetInstance().UserName;
                secondClearTime = ControllerManager.GetInstance().ClearTime;
                SaveData(bastUserName, bastClearTime.ToString(), secondUserName,
                    secondClearTime.ToString(), thirdUserName, thirdClearTime.ToString());
            }

            else if (thirdClearTime > ControllerManager.GetInstance().ClearTime)
            {
                thirdUserName = ControllerManager.GetInstance().UserName;
                thirdClearTime = ControllerManager.GetInstance().ClearTime;
                SaveData(bastUserName, bastClearTime.ToString(), secondUserName,
                    secondClearTime.ToString(), thirdUserName, thirdClearTime.ToString());
            }

            ControllerManager.GetInstance().BastUserName = bastUserName;
            ControllerManager.GetInstance().BastClearTime = bastClearTime;

            ControllerManager.GetInstance().SecondUserName = secondUserName;
            ControllerManager.GetInstance().SecondClearTime = secondClearTime;

            ControllerManager.GetInstance().ThirdName = thirdUserName;
            ControllerManager.GetInstance().ThirdClearTime = thirdClearTime;
        }
    }

    //public void SaveData(string _name, string _clearTime, string _secondName,
    //    string _secondClearTime, string _thirdName, string _thirdClearTime)
    //{
    //    DataForm form = new DataForm(_name, _clearTime, _secondName, _secondClearTime, _thirdName, _thirdClearTime);

    //    string JsonData = JsonUtility.ToJson(form);

    //    FileStream fileStream = new FileStream(
    //        Application.dataPath + form, FileMode.Create);

    //    byte[] data = Encoding.UTF8.GetBytes(JsonData);

    //    fileStream.Write(data, 0, data.Length);
    //    fileStream.Close();
    //}

    public void SaveData(string _name, string _clearTime, string _secondName,
       string _secondClearTime, string _thirdName, string _thirdClearTime)
    {
        GameObject CanvasObject = GameObject.Find("Canvas").gameObject;
        GameObject TextObject = new GameObject("Test");

        TextObject.transform.SetParent(CanvasObject.transform);

        RectTransform rectTransform = TextObject.AddComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0.0f, 850.0f);
        rectTransform.sizeDelta = new Vector2(250.0f, 50.0f);

        Text text = TextObject.AddComponent<Text>();

        text.fontSize = 25;
        text = GameObject.Find("Test").gameObject.GetComponent<Text>();
        
        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.font = font;

        text.text = string.Format( "{0}", Application.dataPath);

        DataForm form = new DataForm(_name, _clearTime, _secondName, _secondClearTime, _thirdName, _thirdClearTime);

        string JsonData = JsonUtility.ToJson(form);

        FileStream fileStream = new FileStream(
            Application.dataPath + "/halu99/UnityBuild/Resources/saveFile/Data.json", FileMode.Create);

        byte[] data = Encoding.UTF8.GetBytes(JsonData);

        
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
}