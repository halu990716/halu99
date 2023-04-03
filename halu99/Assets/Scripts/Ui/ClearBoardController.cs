using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearBoardController : MonoBehaviour
{
    public Text bastUserName;
    public Text secondUserName;
    public Text thirdName;

    public Text BastTime;
    public Text SecondTime;
    public Text ThirdTime;

    private int Time;

    private void Awake()
    {
    }

    void Start()
    {
        //bastUserName.text = ControllerManager.GetInstance().BastUserName;
        //secondUserName.text = ControllerManager.GetInstance().SecondUserName;
        //thirdName.text = ControllerManager.GetInstance().ThirdName;

        //Time = ControllerManager.GetInstance().BastClearTime;
        //BastTime.text = (Time / 60).ToString("D2") + ":" + (Time % 60).ToString("D2");

        //Time = ControllerManager.GetInstance().SecondClearTime;
        //SecondTime.text = (Time / 60 ).ToString("D2") + ":" + (Time % 60).ToString("D2");   

        //Time = ControllerManager.GetInstance().ThirdClearTime;
        //ThirdTime.text = (Time / 60).ToString("D2") + ":" + (Time % 60).ToString("D2");
    }

    void Update()
    {
        bastUserName.text = ControllerManager.GetInstance().BastUserName;
        secondUserName.text = ControllerManager.GetInstance().SecondUserName;
        thirdName.text = ControllerManager.GetInstance().ThirdName;

        Time = ControllerManager.GetInstance().BastClearTime;
        BastTime.text = (Time / 60).ToString("D2") + ":" + (Time % 60).ToString("D2");

        Time = ControllerManager.GetInstance().SecondClearTime;
        SecondTime.text = (Time / 60).ToString("D2") + ":" + (Time % 60).ToString("D2");

        Time = ControllerManager.GetInstance().ThirdClearTime;
        ThirdTime.text = (Time / 60).ToString("D2") + ":" + (Time % 60).ToString("D2");
    }
}
