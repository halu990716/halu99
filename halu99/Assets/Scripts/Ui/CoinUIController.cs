using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIController : MonoBehaviour
{
    public Text CoinText;

    void Start()
    {
        
    }

    void Update()
    {
        CoinText.text = ControllerManager.GetInstance().Coin.ToString();
    }
}
