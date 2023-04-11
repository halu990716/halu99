using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class progressBar : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public Text text;
    public Image image;

    IEnumerator Start()
    {

        asyncOperation = SceneManager.LoadSceneAsync("Main menu");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progerss = asyncOperation.progress / 0.9f * 100f;

            text.text = progerss.ToString() + "%";

            image.fillAmount = progerss;

            yield return null;

            if (asyncOperation.progress > 0.7f)
            {
                yield return new WaitForSeconds(2.5f);

                asyncOperation.allowSceneActivation = true;
            }
        }
    }
}
