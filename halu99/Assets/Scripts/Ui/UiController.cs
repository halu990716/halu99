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
    private GameObject rankButton;

    private Animator TutorialBoardAni;


    private bool GameStartActive;
    private bool TutorialBoardCheck;

    private void Awake()
    {
        Titel = GameObject.Find("Titel");
        GameStart = GameObject.Find("Game Start Button");
        Tutorial = GameObject.Find("Tutorial");
        TutorialBoard = GameObject.Find("Tutorial Board");
        rankButton = GameObject.Find("Rank Button");

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

        ControllerManager.GetInstance().Player_MaxHp = 3;
        ControllerManager.GetInstance().Player_HP = 3;
        ControllerManager.GetInstance().Player_Die = false;

        ControllerManager.GetInstance().SkillCool = 0.1f;

        ControllerManager.GetInstance().Ship_C = false;

        ControllerManager.GetInstance().MissileDamage = 10;
        ControllerManager.GetInstance().MaxMissileDamage = 50;
        ControllerManager.GetInstance().MissileHp = 1;
        ControllerManager.GetInstance().AttackSpeed = 1;
        ControllerManager.GetInstance().AttackCount = 1;
        ControllerManager.GetInstance().MissileCount = 0;
        ControllerManager.GetInstance().EnemyHp = 30;

        ControllerManager.GetInstance().BossHp = 30;
        ControllerManager.GetInstance().BossDie = false;

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
        rankButton.SetActive(GameStartActive);
    }

    public void RankButton()
    {
        ControllerManager.GetInstance().RankButton = true;
    }
}
