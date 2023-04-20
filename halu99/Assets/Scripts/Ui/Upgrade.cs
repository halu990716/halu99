using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Upgrade : MonoBehaviour
{
    public Text player1Text,  player2Text, player3Text, player4Text;
    public Text player1Upgrad, player2Upgrad, player3Upgrad, player4Upgrad;
    public Text player1UpgradCoin, player2UpgradCoin, player3UpgradCoin, player4UpgradCoin;
    public Text UpgradeText;

    void Update()
    {
        player1Text.text = ControllerManager.GetInstance().player1.ToString();
        player2Text.text = (100 - (ControllerManager.GetInstance().player2 * 10)).ToString() + "퍼";
        player3Text.text = (1 + (ControllerManager.GetInstance().player3 * 0.2)).ToString() + "배";
        player4Text.text = (1 + (ControllerManager.GetInstance().player4 * 0.2)).ToString() + "배";

        player1Upgrad.text = "+" + ControllerManager.GetInstance().player1.ToString();
        player2Upgrad.text = "+" + ControllerManager.GetInstance().player2.ToString();
        player3Upgrad.text = "+" + ControllerManager.GetInstance().player3.ToString();
        player4Upgrad.text = "+" + ControllerManager.GetInstance().player4.ToString();

        if (ControllerManager.GetInstance().player1 < 5)
            player1UpgradCoin.text = (10 + (ControllerManager.GetInstance().player1 * 10)).ToString();
        else
            player1UpgradCoin.text = "MAX";

        if (ControllerManager.GetInstance().player2 < 5)
            player2UpgradCoin.text = (10 + (ControllerManager.GetInstance().player2 * 10)).ToString();
        else
            player2UpgradCoin.text = "MAX";

        if (ControllerManager.GetInstance().player3 < 5)
            player3UpgradCoin.text = (10 + (ControllerManager.GetInstance().player3 * 10)).ToString();
        else
            player3UpgradCoin.text = "MAX";

        if (ControllerManager.GetInstance().player4 < 5)
            player4UpgradCoin.text = (10 + (ControllerManager.GetInstance().player4 * 10)).ToString();
        else
            player4UpgradCoin.text = "MAX";

        
    }

    public void player1UpgradButton()
    {
        int rand = Random.Range(0, 2);

        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player1 * 10)) &&
             ControllerManager.GetInstance().player1 < 5&&
             rand == 0)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player1 * 10));
            ControllerManager.GetInstance().player1++;

            StartCoroutine(UpgradeTextTrue());
        }

        else if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player1 * 10)) &&
             ControllerManager.GetInstance().player1 < 5 &&
             rand == 1)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player1 * 10));
            StartCoroutine(UpgradeTextFlase());

            if(ControllerManager.GetInstance().player1 >= 3)
            {
                ControllerManager.GetInstance().player1--;
            }
        }

        else
        {
            StartCoroutine(UpgradeTextX());

        }
    }

    public void player2UpgradButton()
    {
        int rand = Random.Range(0, 2);

        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player2 * 10)) &&
            ControllerManager.GetInstance().player2 < 5 &&
            rand == 0)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player2 * 10));
            ControllerManager.GetInstance().player2++;

            StartCoroutine(UpgradeTextTrue());
        }

        else if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player2 * 10)) &&
             ControllerManager.GetInstance().player2 < 5 &&
             rand == 1)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player2 * 10));
            StartCoroutine(UpgradeTextFlase());

            if (ControllerManager.GetInstance().player2 >= 3)
            {
                ControllerManager.GetInstance().player2--;
            }
        }

        else
        {
            StartCoroutine(UpgradeTextX());

        }
    }

    public void player3UpgradButton()
    {
        int rand = Random.Range(0, 2);

        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player3 * 10)) &&
            ControllerManager.GetInstance().player3 < 5 &&
            rand == 0)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player3 * 10));
            ControllerManager.GetInstance().player3++;

            StartCoroutine(UpgradeTextTrue());

        }

        else if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player3 * 10)) &&
             ControllerManager.GetInstance().player3 < 5 &&
             rand == 1)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player3 * 10));
            StartCoroutine(UpgradeTextFlase());

            if (ControllerManager.GetInstance().player3 >= 3)
            {
                ControllerManager.GetInstance().player3--;
            }
        }

        else
        {
            StartCoroutine(UpgradeTextX());

        }
    }

    public void player4UpgradButton()
    {
        int rand = Random.Range(0, 2);

        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player4 * 10)) &&
            ControllerManager.GetInstance().player4 < 5 &&
            rand == 0)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player4 * 10));
            ControllerManager.GetInstance().player4++;

            StartCoroutine(UpgradeTextTrue());
        }

        else if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player4 * 10)) &&
             ControllerManager.GetInstance().player4 < 5 &&
             rand == 1)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player4 * 10));
            StartCoroutine(UpgradeTextFlase());

            if (ControllerManager.GetInstance().player4 >= 3)
            {
                ControllerManager.GetInstance().player4--;
            }
        }

        else
        {
            StartCoroutine(UpgradeTextX());

        }
    }

    IEnumerator UpgradeTextTrue()
    {
        UpgradeText.fontSize = 150;
        UpgradeText.text = "강화 성공";
        UpgradeText.color = new Color(UpgradeText.color.r, UpgradeText.color.g, UpgradeText.color.b, 1);

        while (UpgradeText.color.a > 0.0f)
        {
            UpgradeText.color = new Color(UpgradeText.color.r, UpgradeText.color.g, UpgradeText.color.b, UpgradeText.color.a - (Time.deltaTime));
            yield return null;
        }
    }

    IEnumerator UpgradeTextFlase()
    {
        UpgradeText.fontSize = 150;
        UpgradeText.text = "강화 실패";
        UpgradeText.color = new Color(UpgradeText.color.r, UpgradeText.color.g, UpgradeText.color.b, 1);

        while (UpgradeText.color.a >= 0.0f)
        {
            UpgradeText.color = new Color(UpgradeText.color.r, UpgradeText.color.g, UpgradeText.color.b, UpgradeText.color.a - (Time.deltaTime));
            yield return null;
        }
    }

    IEnumerator UpgradeTextX()
    {
        UpgradeText.fontSize = 100;
        UpgradeText.text = "강화할 수 없습니다";
        UpgradeText.color = new Color(UpgradeText.color.r, UpgradeText.color.g, UpgradeText.color.b, 1);

        while (UpgradeText.color.a >= 0.0f)
        {
            UpgradeText.color = new Color(UpgradeText.color.r, UpgradeText.color.g, UpgradeText.color.b, UpgradeText.color.a - (Time.deltaTime));
            yield return null;
        }
    }
}
