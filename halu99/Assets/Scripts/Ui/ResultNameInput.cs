using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultNameInput : MonoBehaviour
{
    public GameObject PalyerNameInput;
    public GameObject HomeButton;

    public InputField inputField;
    private string playerName = null;

    private void Awake()
    {
        //HomeButton = GameObject.Find("Palyer Name Input");
        playerName = inputField.GetComponent<InputField>().text;
    }


    void Update()
    {
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }
    public void InputName()
    {
        playerName = inputField.text;
        PlayerPrefs.SetString("CurrentPlayerName", playerName);

        PalyerNameInput.SetActive(false);
        HomeButton.SetActive(true);
        ControllerManager.GetInstance().UserName = playerName;
        //GameManager.instance.ScoreSet(GameManager.instance.score, playerName);
    }
}
