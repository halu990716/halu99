using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextManager : MonoBehaviour
{
    private static DamageTextManager _instance = null;

    public static DamageTextManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DamageTextManager>();
            }
            return _instance;
        }
    }

    public Canvas canvas;
    public GameObject dmgTxt;
    public void CreateDamageText(Vector3 hitPoint, int hitDamage)
    {
        GameObject damageText = Instantiate(dmgTxt, hitPoint,
            Quaternion.identity, canvas.transform);
        damageText.GetComponent<Text>().text = hitDamage.ToString();
    }
}
