using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script Made By Svedlund */
public class LoadingBar : MonoBehaviour
{
    private float loadingBarFillAmount;
    private Image loadingBarImage;

    private Door triggeringDoor;


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
        if (triggeringDoor != null)
        {
            triggeringDoor.fenceDoor.transform.rotation = triggeringDoor.closedTransform.rotation;
            triggeringDoor = null;
        }
        StopAllCoroutines();
        loadingBarFillAmount = 0f;
        loadingBarImage.fillAmount = loadingBarFillAmount;
    }

    private IEnumerator FillLoadingBar(Door triggeringDoor)
    {
        this.triggeringDoor = triggeringDoor;
        while (loadingBarFillAmount <= 1f)
        {
            loadingBarFillAmount += 0.01f;
            loadingBarImage.fillAmount = loadingBarFillAmount;
            
            triggeringDoor.fenceDoor.transform.rotation = Quaternion.Lerp(triggeringDoor.closedTransform.rotation, triggeringDoor.openTransform.rotation, loadingBarFillAmount);

            yield return new WaitForSeconds(0.01f);
        }
        triggeringDoor.fenceDoor.transform.rotation = triggeringDoor.openTransform.rotation;
        triggeringDoor.StartTeleportationSequence();
        EmptyLoadingBar();
        yield return null;
    }
}
