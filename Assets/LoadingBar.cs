using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script Made By Svedlund */
public class LoadingBar : MonoBehaviour
{
    private float loadingBarFillAmount;
    private Image loadingBarImage;


    private void Start()
    {
        loadingBarImage = GetComponentInChildren<Image>();
        loadingBarFillAmount = loadingBarImage.fillAmount;
    }

    public void StartLoadingBar(Door triggeringDoor)
    {
        StartCoroutine(FillLoadingBar(triggeringDoor));
    }

    public void EmptyLoadingBar()
    {
        StopAllCoroutines();
        loadingBarFillAmount = 0f;
        loadingBarImage.fillAmount = loadingBarFillAmount;
    }

    private IEnumerator FillLoadingBar(Door triggeringDoor)
    {
        while (loadingBarFillAmount < 1f)
        {
            loadingBarFillAmount += 0.01f;
            loadingBarImage.fillAmount = loadingBarFillAmount;
            yield return new WaitForSeconds(0.01f);
        }
        triggeringDoor.StartTeleportationSequence();
        EmptyLoadingBar();
        yield return null;
    }
}
