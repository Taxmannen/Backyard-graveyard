using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script Made By Svedlund */
public class Door : MonoBehaviour
{
    public Door targetDoor;
    public bool doorOnCooldown = false;
    private LoadingBar loadingBar;

    private Image cameraFadeImage;
    private Image cameraFadeCanvas;

    private Collider player;

    public GameObject fenceDoor;
    public Transform openTransform;
    public Transform closedTransform;


    private void Awake()
    {
        cameraFadeCanvas = Resources.Load<Image>("CameraFadeCanvas");
        if (GameObject.Find("CameraFadeCanvas(Clone)") == null)
        {
            cameraFadeImage = Instantiate(cameraFadeCanvas, this.transform.position, Quaternion.identity);
        }
        else cameraFadeImage = GameObject.Find("CameraFadeCanvas(Clone)").GetComponent<Image>();

        cameraFadeImage.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        cameraFadeImage.CrossFadeAlpha(0f, 0f, false);

        loadingBar = FindObjectOfType<LoadingBar>();
    }

    public IEnumerator DoorCooldown()
    {
        doorOnCooldown = true;
        yield return new WaitForSeconds(0.5f);
        doorOnCooldown = false;
        yield return null;
    }

    public IEnumerator CameraFade()
    {
        cameraFadeImage.CrossFadeAlpha(1.0f, 0.2f, true);
        yield return new WaitForSeconds(0.3f);
        cameraFadeImage.CrossFadeAlpha(0f, 0.2f, true);
        yield return null;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!doorOnCooldown && other.tag == "Player")
    //    {
    //        StartCoroutine(DoorCooldown());
    //        StartCoroutine(targetDoor.DoorCooldown());
    //        StartCoroutine(CameraFade());
    //        StartCoroutine(TeleportPlayer(other));
    //        //other.gameObject.transform.root.LookAt(targetDoor.transform);
    //    }
    //}

    public void StartTeleportationSequence()
    {
        //StartCoroutine(DoorCooldown());
        StartCoroutine(targetDoor.DoorCooldown());
        StartCoroutine(CameraFade());
        StartCoroutine(TeleportPlayer(player));
        //other.gameObject.transform.root.LookAt(targetDoor.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorOnCooldown && other.tag == "Player")
        {
            player = other;
            foreach (LoadingBar loadingBar in FindObjectsOfType<LoadingBar>())
            {
                loadingBar.EmptyLoadingBar();
            }
            loadingBar = other.GetComponentInChildren<LoadingBar>();
            loadingBar.StartLoadingBar(this);
            fenceDoor.transform.rotation = openTransform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            loadingBar.EmptyLoadingBar();
            fenceDoor.transform.rotation = closedTransform.rotation;
        }
    }

    private IEnumerator TeleportPlayer(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 relativePositionToDoor = transform.position - other.gameObject.transform.root.position;
        other.gameObject.transform.root.position = targetDoor.transform.position - relativePositionToDoor;
        yield return null;
    }

    public void DoorPairClosingSequence()
    {
        StartCoroutine(DoorPairClosingSequenceLerp());
    }

    private IEnumerator DoorPairClosingSequenceLerp()
    {
        float i = 0f;
        while (i <= 1)
        {
            i += 0.01f;
            targetDoor.fenceDoor.transform.rotation = Quaternion.Lerp(openTransform.rotation, closedTransform.rotation, i);
            
            yield return new WaitForSeconds(0.01f);
        }
        targetDoor.fenceDoor.transform.rotation = closedTransform.rotation;
        fenceDoor.transform.rotation = closedTransform.rotation;
        yield return null;
    }
}
