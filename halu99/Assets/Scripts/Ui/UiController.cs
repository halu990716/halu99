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

    private Animator Ani;


    public bool GameStartActive;

    private void Awake()
    {
        Titel = GameObject.Find("Titel");
        GameStart = GameObject.Find("Game Start Button");
        Tutorial = GameObject.Find("Tutorial");
        TutorialBoard = GameObject.Find("Tutorial Board");
    }
    private void Start()
    {
        GameStartActive = true;
        GameStart.SetActive(GameStartActive);
        Tutorial.SetActive(GameStartActive); 
    }

    public void onTitleStart()
    {
        GameStartActive = !GameStartActive;
        Titel.SetActive(GameStartActive);
        GameStart.SetActive(GameStartActive);
        Tutorial.SetActive(GameStartActive);
        PlayerList.SetActive(!GameStartActive);
    }

    public void onTitleTutorial()
    {
        GameStartActive = !GameStartActive;
        Titel.SetActive(GameStartActive);
        GameStart.SetActive(GameStartActive);
        Tutorial.SetActive(GameStartActive);

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
}
