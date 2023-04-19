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
        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player1 * 10)) &&
             ControllerManager.GetInstance().player1 < 5)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player1 * 10));
            ControllerManager.GetInstance().player1++;

            StartCoroutine(UpgradeTextTrue());
        }
        else
        {
            StartCoroutine(UpgradeTextflase());

        }
    }

    public void player2UpgradButton()
    {
        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player2 * 10)) &&
            ControllerManager.GetInstance().player2 < 5)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player2 * 10));
            ControllerManager.GetInstance().player2++;

            StartCoroutine(UpgradeTextTrue());
        }
        else
        {
            StartCoroutine(UpgradeTextflase());

        }
    }

    public void player3UpgradButton()
    {
        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player3 * 10)) &&
            ControllerManager.GetInstance().player3 < 5)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player3 * 10));
            ControllerManager.GetInstance().player3++;

            StartCoroutine(UpgradeTextTrue());

        }
        else
        {
            StartCoroutine(UpgradeTextflase());

        }
    }

    public void player4UpgradButton()
    {
        if (ControllerManager.GetInstance().Coin >= (10 + (ControllerManager.GetInstance().player4 * 10)) &&
            ControllerManager.GetInstance().player4 < 5)
        {
            ControllerManager.GetInstance().Coin -= (10 + (ControllerManager.GetInstance().player4 * 10));
            ControllerManager.GetInstance().player4++;

            StartCoroutine(UpgradeTextTrue());
        }
        else
        {
            StartCoroutine(UpgradeTextflase());

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

    IEnumerator UpgradeTextflase()
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
