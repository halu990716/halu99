using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPvar : MonoBehaviour
{
    private Slider HPBar;

    private void Awake()
    {
        HPBar = GetComponent<Slider>();
    }

    private void Start()
    {
        StartCoroutine(WaitStart());
    }

    private void Update()
    {
        HPBar.value = ControllerManager.GetInstance().Player_HP;

        if (HPBar.value == 0)
        {
            Destroy(this.gameObject, 0.016f);
        }
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(1.5f);

        HPBar.maxValue = ControllerManager.GetInstance().Player_HP;
        HPBar.value = HPBar.maxValue;
    }
}
