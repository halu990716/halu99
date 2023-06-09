using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int Coin;

    private void Start()
    {
        ControllerManager.GetInstance().UpLoadRank = true;
    }

    void Update()
    {
        /*
        if (ControllerManager.GetInstance().UpLoadRank)
        {
            UpLoadRank();
        }

        if (ControllerManager.GetInstance().UpDateRank)
        {
            ControllerManager.GetInstance().UpDateRank = false;

            if (bastClearTime > ControllerManager.GetInstance().ClearTime&&
                ControllerManager.GetInstance().UserName != bastUserName&&
                ControllerManager.GetInstance().UserName != secondUserName &&
                ControllerManager.GetInstance().UserName != thirdUserName)
            {
                thirdUserName = secondUserName;
                thirdClearTime = secondClearTime;

                secondUserName = bastUserName;
                secondClearTime = bastClearTime;

                bastUserName = ControllerManager.GetInstance().UserName;
                bastClearTime = ControllerManager.GetInstance().ClearTime;
            }

            else if (secondClearTime > ControllerManager.GetInstance().ClearTime&&
                ControllerManager.GetInstance().UserName != secondUserName&&
                ControllerManager.GetInstance().UserName != thirdUserName)
            {
                thirdUserName = secondUserName;
                thirdClearTime = secondClearTime;

                secondUserName = ControllerManager.GetInstance().UserName;
                secondClearTime = ControllerManager.GetInstance().ClearTime;

            }

            else if (thirdClearTime > ControllerManager.GetInstance().ClearTime&&
                ControllerManager.GetInstance().UserName != thirdUserName)
            {
                thirdUserName = ControllerManager.GetInstance().UserName;
                thirdClearTime = ControllerManager.GetInstance().ClearTime;
            }

            ControllerManager.GetInstance().BastUserName = bastUserName;
            ControllerManager.GetInstance().BastClearTime = bastClearTime;

            ControllerManager.GetInstance().SecondUserName = secondUserName;
            ControllerManager.GetInstance().SecondClearTime = secondClearTime;

            ControllerManager.GetInstance().ThirdName = thirdUserName;
            ControllerManager.GetInstance().ThirdClearTime = thirdClearTime;

            PlayerPrefs.SetString("BastUserName", bastUserName);
            PlayerPrefs.SetInt("BastClearTime", bastClearTime);

            PlayerPrefs.SetString("SecondUserName", secondUserName);
            PlayerPrefs.SetInt("SecondClearTime", secondClearTime);

            PlayerPrefs.SetString("ThirdUserName", thirdUserName);
            PlayerPrefs.SetInt("ThirdClearTime", thirdClearTime);

            ControllerManager.GetInstance().UpDateRankBoard = true;
        }
        PlayerPrefs.SetInt("Coin", ControllerManager.GetInstance().Coin);
    }

    void UpLoadRank()
    {
        ControllerManager.GetInstance().UpLoadRank = false;

        if (!PlayerPrefs.HasKey("BastUserName"))
        {
            PlayerPrefs.SetString("BastUserName", "NULL");
        }

        if (!PlayerPrefs.HasKey("BastClearTime"))
        {
            PlayerPrefs.SetInt("BastClearTime", 9999);
        }

        if (!PlayerPrefs.HasKey("SecondUserName"))
        {
            PlayerPrefs.SetString("SecondUserName", "NULL");
        }

        if (!PlayerPrefs.HasKey("SecondClearTime"))
        {
            PlayerPrefs.SetInt("SecondClearTime", 9999);
        }

        if (!PlayerPrefs.HasKey("ThirdUserName"))
        {
            PlayerPrefs.SetString("ThirdUserName", "NULL");
        }

        if (!PlayerPrefs.HasKey("ThirdClearTime"))
        {
            PlayerPrefs.SetInt("ThirdClearTime", 9999);
        }

        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }

        bastUserName = ControllerManager.GetInstance().BastUserName = PlayerPrefs.GetString("BastUserName");
        bastClearTime = ControllerManager.GetInstance().BastClearTime = PlayerPrefs.GetInt("BastClearTime");

        secondUserName = ControllerManager.GetInstance().SecondUserName = PlayerPrefs.GetString("SecondUserName");
        secondClearTime = ControllerManager.GetInstance().SecondClearTime = PlayerPrefs.GetInt("SecondClearTime");

        thirdUserName = ControllerManager.GetInstance().ThirdName = PlayerPrefs.GetString("ThirdUserName");
        thirdClearTime = ControllerManager.GetInstance().ThirdClearTime = PlayerPrefs.GetInt("ThirdClearTime");

        Coin = ControllerManager.GetInstance().Coin = PlayerPrefs.GetInt("Coin");

        ControllerManager.GetInstance().UpDateRankBoard = true;
        */
    }

}