using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject GameStart;
    public GameObject PlayerList;

    public bool GameStartActive;

    private void Start()
    {
        GameStartActive = true;
        GameStart.SetActive(GameStartActive);
    }

    public void onTitleStart()
    {
        GameStartActive = !GameStartActive;
        GameStart.SetActive(GameStartActive);
        PlayerList.SetActive(!GameStartActive);
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
