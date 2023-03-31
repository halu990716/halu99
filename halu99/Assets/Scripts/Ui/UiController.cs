using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    private GameObject Titel;
    private GameObject GameStart;
    public GameObject PlayerList;
    private GameObject Tutorial;
    private GameObject TutorialBoard;

    private Animator TutorialBoardAni;


    private bool GameStartActive;
    private bool TutorialBoardCheck;

    private void Awake()
    {
        Titel = GameObject.Find("Titel");
        GameStart = GameObject.Find("Game Start Button");
        Tutorial = GameObject.Find("Tutorial");
        TutorialBoard = GameObject.Find("Tutorial Board");

        TutorialBoardAni = TutorialBoard.GetComponent<Animator>();
    }
    private void Start()
    {
        GameStartActive = true;
        TutorialBoardCheck = false;
        GameStart.SetActive(GameStartActive);
        Tutorial.SetActive(GameStartActive); 
    }

    public void onTitleStart()
    {
        TitleSwich();

        PlayerList.SetActive(!GameStartActive);
    }

    public void onTitleTutorial()
    {
        TitleSwich();

        TutorialBoardCheck = !TutorialBoardCheck;

        TutorialBoardAni.SetBool("Move", TutorialBoardCheck);
    }

    public void onCheckmark()
    {
        TitleSwich();

        TutorialBoardCheck = !TutorialBoardCheck;

        TutorialBoardAni.SetBool("Move", TutorialBoardCheck);
    }

    public void onPlayer1()
    {
        ControllerManager.GetInstance().Player_List = 1;

        SceneManager.LoadScene("Game");
    }

    public void onPlayer2()
    {
        ControllerManager.GetInstance().Player_List = 2;

        SceneManager.LoadScene("Game");
    }

    public void onPlayer3()
    {
        ControllerManager.GetInstance().Player_List = 3;

        SceneManager.LoadScene("Game");
    }
    public void onPlayer4()
    {
        ControllerManager.GetInstance().Player_List = 4;

        SceneManager.LoadScene("Game");
    }

    private void TitleSwich()
    {
        GameStartActive = !GameStartActive;
        Titel.SetActive(GameStartActive);
        GameStart.SetActive(GameStartActive);
        Tutorial.SetActive(GameStartActive);
    }
}
