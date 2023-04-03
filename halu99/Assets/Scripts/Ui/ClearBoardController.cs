using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearBoardController : MonoBehaviour
{
    public Text bastUserName;
    public Text secondUserName;
    public Text thirdName;

    public Text BastTime;
    public Text SecondTime;
    public Text ThirdTime;

    public Text ClearTime;

    private Animator Ani;


    private int Time;


    private void Awake()
    {
        Ani = GetComponent<Animator>();
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
        UpRank();

        if (ControllerManager.GetInstance().BossDie) 
        {
            Ani.SetBool("Move", true);

        }
    }

    private void UpRank()
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

        Time = ControllerManager.GetInstance().ClearTime;
        ClearTime.text = (Time / 60).ToString("D2") + ":" + (Time % 60).ToString("D2");

    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene("Main menu");
    }
}
