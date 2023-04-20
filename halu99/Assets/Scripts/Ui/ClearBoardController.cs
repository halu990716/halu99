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

        UpRank();

        if (ControllerManager.GetInstance().BossDie || ControllerManager.GetInstance().RankButton) 
        {
            Ani.SetBool("Move", true);

            if(ControllerManager.GetInstance().BastUserName != ControllerManager.GetInstance().UserName &&
                ControllerManager.GetInstance().BastClearTime > ControllerManager.GetInstance().ClearTime)
            {
                ControllerManager.GetInstance().ThirdClearTime = ControllerManager.GetInstance().SecondClearTime;
                ControllerManager.GetInstance().ThirdName = ControllerManager.GetInstance().SecondUserName;

                ControllerManager.GetInstance().SecondClearTime = ControllerManager.GetInstance().BastClearTime;
                ControllerManager.GetInstance().SecondUserName = ControllerManager.GetInstance().BastUserName;

                ControllerManager.GetInstance().BastClearTime = ControllerManager.GetInstance().ClearTime;
                ControllerManager.GetInstance().BastUserName = ControllerManager.GetInstance().UserName;
            }

            else if(ControllerManager.GetInstance().ClearTime < ControllerManager.GetInstance().BastClearTime)
            {
                ControllerManager.GetInstance().BastClearTime = ControllerManager.GetInstance().ClearTime;
            }

            else if(ControllerManager.GetInstance().ClearTime < ControllerManager.GetInstance().SecondClearTime &&
                ControllerManager.GetInstance().UserName != ControllerManager.GetInstance().BastUserName &&
                ControllerManager.GetInstance().UserName != ControllerManager.GetInstance().SecondUserName)
            {
                ControllerManager.GetInstance().ThirdClearTime = ControllerManager.GetInstance().SecondClearTime;
                ControllerManager.GetInstance().SecondClearTime = ControllerManager.GetInstance().ClearTime;

                ControllerManager.GetInstance().ThirdName = ControllerManager.GetInstance().SecondUserName;
                ControllerManager.GetInstance().SecondUserName = ControllerManager.GetInstance().UserName;
            }

            else if(ControllerManager.GetInstance().ClearTime < ControllerManager.GetInstance().SecondClearTime &&
                ControllerManager.GetInstance().UserName != ControllerManager.GetInstance().BastUserName)
            {
                ControllerManager.GetInstance().SecondClearTime = ControllerManager.GetInstance().ClearTime;
            }

            else if(ControllerManager.GetInstance().ClearTime < ControllerManager.GetInstance().ThirdClearTime &&
                ControllerManager.GetInstance().UserName != ControllerManager.GetInstance().BastUserName &&
                ControllerManager.GetInstance().UserName != ControllerManager.GetInstance().SecondUserName)
            {
                ControllerManager.GetInstance().ThirdClearTime = ControllerManager.GetInstance().ClearTime;
                ControllerManager.GetInstance().ThirdName = ControllerManager.GetInstance().UserName;
            }
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
