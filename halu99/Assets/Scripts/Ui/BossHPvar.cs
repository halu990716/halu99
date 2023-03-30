using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPvar : MonoBehaviour
{
    private Slider HpBar;

    private void Awake()
    {
        HpBar = GetComponent<Slider>();
    }
    void Start()
    {
        HpBar.maxValue = ControllerManager.GetInstance().BossHp;
        HpBar.value = HpBar.maxValue;
    }

    void Update()
    {
        HpBar.value = ControllerManager.GetInstance().BossHp;
    }
}
