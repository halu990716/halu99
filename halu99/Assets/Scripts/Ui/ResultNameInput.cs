using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultNameInput : MonoBehaviour
{
    public GameObject PalyerNameInput;
    public GameObject HomeButton;
    public GameObject Loding;
    public GameObject input;

    private AudioSource audioSource;

    public InputField inputField;
    private string playerName = null;

    private void Awake()
    {
        //HomeButton = GameObject.Find("Palyer Name Input");
        playerName = inputField.GetComponent<InputField>().text;

        audioSource = gameObject.AddComponent<AudioSource>();

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

        Loding.SetActive(true);
        input.SetActive(false);

        ControllerManager.GetInstance().UserName = playerName;
        ControllerManager.GetInstance().UpDateRank = true;
        //GameManager.instance.ScoreSet(GameManager.instance.score, playerName);

        SoundManager.Instance.soundManager("Click");

        StartCoroutine(LodingWait());
    }

    IEnumerator LodingWait()
    {
        yield return new WaitForSeconds(3.0f);

        Loding.SetActive(false);
        PalyerNameInput.SetActive(false);
        HomeButton.SetActive(true);
    }
}
