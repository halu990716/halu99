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

    private int time;


    private void Awake()
    {
        Ani = GetComponent<Animator>();

        //Click = Resources.Load("Sounds/Click") as AudioClip;
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
        if (ControllerManager.GetInstance().UpDateRankBoard)
        {
            UpRank();
        }

        if (ControllerManager.GetInstance().BossDie || ControllerManager.GetInstance().RankButton) 
        {
            Ani.SetBool("Move", true);
        }
        
        if (!ControllerManager.GetInstance().RankButton && !ControllerManager.GetInstance().BossDie)
        {
            Ani.SetBool("Move", false);
        }
    }

    private void UpRank()
    {
        bastUserName.text = ControllerManager.GetInstance().BastUserName;
        secondUserName.text = ControllerManager.GetInstance().SecondUserName;
        thirdName.text = ControllerManager.GetInstance().ThirdName;

        time = ControllerManager.GetInstance().BastClearTime;
        BastTime.text = (time / 60).ToString("D2") + ":" + (time % 60).ToString("D2");

        time = ControllerManager.GetInstance().SecondClearTime;
        SecondTime.text = (time / 60).ToString("D2") + ":" + (time % 60).ToString("D2");

        time = ControllerManager.GetInstance().ThirdClearTime;
        ThirdTime.text = (time / 60).ToString("D2") + ":" + (time % 60).ToString("D2");

        time = ControllerManager.GetInstance().ClearTime;
        ClearTime.text = (time / 60).ToString("D2") + ":" + (time % 60).ToString("D2");

        ControllerManager.GetInstance().UpDateRankBoard = false;
    }

    public void OnHomeButton()
    {
        ControllerManager.GetInstance().BossDie = false;

        SoundManager.Instance.soundManager("Click");

        Time.timeScale = 1;

        SceneManager.LoadScene("Main menu");

    }

}
