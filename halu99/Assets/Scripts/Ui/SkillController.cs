using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    //public List<GameObject> Images = new List<GameObject>();
    //public List<GameObject> Buttons = new List<GameObject>();
    //public List<Image> ButtonsImages = new List<Image>();

    public Image image;

    private float CoolDown;

    private bool Cool;
    private void Start()
    {
        GameObject SkillsObj = GameObject.Find("Skills");

        //for (int i = 0; i < SkillsObj.transform.childCount; ++i)
        //    Images.Add(SkillsObj.transform.GetChild(i).gameObject);

        //for (int i = 0; i < Images.Count; ++i)
        //    Buttons.Add(Images[i].transform.GetChild(0).gameObject);

        //for (int i = 0; i < Buttons.Count; ++i)
        //    ButtonsImages.Add(Buttons[i].GetComponent<Image>());

        CoolDown = 0.1f;
        Cool = false;
    }

    IEnumerator Skill()
    {
        while (image.fillAmount != 1)
        {
            image.fillAmount += Time.deltaTime * CoolDown;
            yield return null;
        }

        Cool = false;

        //while (ButtonsImages[i].fillAmount != 1)
        //{
        //    ButtonsImages[i].fillAmount += Time.deltaTime * CoolDown;
        //    yield return null;
        //}

        //Buttons[i].GetComponent<Button>().enabled = true;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !Cool)
        {
            image.fillAmount = 0;
            Cool = true;

            StartCoroutine(Skill());
        }
    }
}
