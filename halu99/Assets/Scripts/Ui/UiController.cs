using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class UiController : MonoBehaviour
{
    private GameObject Titel;
    private GameObject GameStart;
    public GameObject PlayerList;
    private GameObject Tutorial;
    private GameObject TutorialBoard;
    private GameObject rankButton;
    private GameObject GameQuitButton;

    private Animator TutorialBoardAni;

    private AudioSource audioSource;

    private bool GameStartActive;
    private bool TutorialBoardCheck;

    private void Awake()
    {
        Titel = GameObject.Find("Titel");
        GameStart = GameObject.Find("Game Start Button");
        Tutorial = GameObject.Find("Tutorial");
        TutorialBoard = GameObject.Find("Tutorial Board");
        rankButton = GameObject.Find("Rank Button");
        GameQuitButton = GameObject.Find("Game Quit Button");

        audioSource = gameObject.AddComponent<AudioSource>();

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

        ControllerManager.GetInstance().BossHp = 30000;
        ControllerManager.GetInstance().BossDie = false;

        //audioSource.PlayOneShot(Click);
        SoundManager.Instance.soundManager("Click");
        
    }

    public void onTitleTutorial()
    {
        TitleSwich();

        TutorialBoardCheck = !TutorialBoardCheck;

        TutorialBoardAni.SetBool("Move", TutorialBoardCheck);

        SoundManager.Instance.soundManager("Click");

    }

    public void onCheckmark()
    {
        TitleSwich();

        TutorialBoardCheck = !TutorialBoardCheck;

        TutorialBoardAni.SetBool("Move", TutorialBoardCheck);

        SoundManager.Instance.soundManager("Click");


    }

    public void onPlayer1()
    {
        ControllerManager.GetInstance().Player_List = 1;

        SoundManager.Instance.soundManager("Click");

        SceneManager.LoadScene("Game");
    }

    public void onPlayer2()
    {
        ControllerManager.GetInstance().Player_List = 2;

        SoundManager.Instance.soundManager("Click");

        SceneManager.LoadScene("Game");
    }

    public void onPlayer3()
    {
        ControllerManager.GetInstance().Player_List = 3;

        SoundManager.Instance.soundManager("Click");

        SceneManager.LoadScene("Game");

    }
    public void onPlayer4()
    {
        ControllerManager.GetInstance().Player_List = 4;

        SoundManager.Instance.soundManager("Click");

        SceneManager.LoadScene("Game");

    }

    private void TitleSwich()
    {
        GameStartActive = !GameStartActive;
        Titel.SetActive(GameStartActive);
        GameStart.SetActive(GameStartActive);
        Tutorial.SetActive(GameStartActive);
        rankButton.SetActive(GameStartActive);
        GameQuitButton.SetActive(GameStartActive);
    }

    public void RankButton()
    {
        ControllerManager.GetInstance().RankButton = true;

        SoundManager.Instance.soundManager("Click");

        TitleSwich();
    }

    public void RankBackButton()
    {
        ControllerManager.GetInstance().RankButton = false;

        SoundManager.Instance.soundManager("Click");

        TitleSwich();

    }

    public void GameQuit()
    {
        SoundManager.Instance.soundManager("Click");

        Application.Quit();
    }
}
