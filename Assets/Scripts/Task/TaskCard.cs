using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

/// <summary>
/// <author>Kristoffer</author>
/// <edits>Simon & Tåqvist</edits>
/// </summary>
public class TaskCard : MonoBehaviour
{
    #region references
    [Header("References")]
    public Image headImage;
    public Image bodyImage;

    public Image OrnamentTypelot01Image;
    public Image OrnamentTypelot01Container;
    public Image OrnamentTypelot02Image;
    public Image OrnamentTypelot02Container;
    public Image OrnamentTypelot03Image;
    public Image OrnamentTypelot03Container;

    public Image treatmentImage;
    public GameObject treatmentContainer;

    public Sprite[] HeadTypeprites;
    public Sprite[] bodySprites;
    public Sprite[] OrnamentTypeprites;
    public Sprite[] TreatmentSprites;

    [SerializeField]
    private Image taskCompletedImage;
    [SerializeField]
    private Image taskFailedImage;

    [SerializeField]
    private Image timerImage;

    [SerializeField]
    private PlaySound soundFX;
    #endregion

    private float timerWidth;
    private float timerWidthMax = 2f;
    private float timerWidthMin = 0f;
    public bool taskCompleted = false;
    private Color timerColor = new Color(0f, 255f, 0f);

    [SerializeField, ReadOnly] public Task task;

    public void UpdateTimerBar(float timerPercent)
    {
        timerImage.color = Color.Lerp(Color.green, Color.red, timerPercent);
        timerWidth = Mathf.Lerp(timerWidthMax, timerWidthMin, timerPercent);
        timerImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, timerWidth);
    }

    public void SetTaskIngredients(int Ornament01Type, int Ornament02Type, int Ornament03Type, int BodyType, int HeadType) {
        SetOrnamentType(Ornament01Type, Ornament02Type, Ornament03Type);
        SetBody(BodyType);
        SetHead(HeadType);
    }

    public void SetTaskIngredients(int Ornament01Type, int Ornament02Type, int Ornament03Type, int BodyType, int HeadType, int treatmentIndex, bool includeTreatment)
    {
        SetOrnamentType(Ornament01Type, Ornament02Type, Ornament03Type);
        SetBody(BodyType);
        SetHead(HeadType);

        if (!includeTreatment)
        {
            treatmentImage.gameObject.SetActive(false);
            treatmentContainer.gameObject.SetActive(false);
        }
        else
        {
            treatmentImage.gameObject.SetActive(true);
            treatmentContainer.gameObject.SetActive(true);
            SetTreatment(treatmentIndex);
        }
    }

    private void SetOrnamentType(int Ornament01Type, int Ornament02Type, int Ornament03Type)
    {
        if (Ornament01Type == (int)OrnamentType.None) {
            OrnamentTypelot01Image.sprite = null;
            OrnamentTypelot01Image.gameObject.SetActive(false);
            OrnamentTypelot01Container.gameObject.SetActive(false);
        }
        else {
            OrnamentTypelot01Image.gameObject.SetActive(true);
            OrnamentTypelot01Container.gameObject.SetActive(true);
            OrnamentTypelot01Image.sprite = OrnamentTypeprites[Ornament01Type];
        }
        if (Ornament02Type == (int)OrnamentType.None) {
            OrnamentTypelot02Image.sprite = null;
            OrnamentTypelot02Image.gameObject.SetActive(false);
            OrnamentTypelot02Container.gameObject.SetActive(false);
        }
        else {
            OrnamentTypelot02Image.gameObject.SetActive(true);
            OrnamentTypelot02Container.gameObject.SetActive(true);
            OrnamentTypelot02Image.sprite = OrnamentTypeprites[Ornament02Type];
        }
        if (Ornament03Type == (int)OrnamentType.None) {
            OrnamentTypelot03Image.sprite = null;
            OrnamentTypelot03Image.gameObject.SetActive(false);
            OrnamentTypelot03Container.gameObject.SetActive(false);
        }
        else {
            OrnamentTypelot03Image.gameObject.SetActive(true);
            OrnamentTypelot03Container.gameObject.SetActive(true);
            OrnamentTypelot03Image.sprite = OrnamentTypeprites[Ornament03Type];
        }
    }

    private void SetBody(int BodyType)
    {
        bodyImage.sprite = bodySprites[BodyType];
        switch (BodyType)
        {
            case 0:
                bodyImage.color = Color.red;
                break;
            case 1:
                bodyImage.color = Color.green;
                break;
            case 2:
                bodyImage.color = Color.blue;
                break;
            default:
                throw new System.Exception("TaskCard::SetBody exception: Invalid color");
        }
    }

    private void SetHead(int HeadType)
    {
        headImage.sprite = HeadTypeprites[HeadType];
        switch (HeadType)
        {
            case 0:
                headImage.color = Color.red;
                break;
            case 1:
                headImage.color = Color.green;
                break;
            case 2:
                headImage.color = Color.blue;
                break;
            default:
                throw new System.Exception("TaskCard::SetHead exception: Invalid color");
        }
    }

    private void SetTreatment(int treatmentType) {
        treatmentImage.sprite = TreatmentSprites[treatmentType];
        //switch (treatmentType) {
        //    case 0:
        //        treatmentImage.color = Color.red;
        //        break;
        //    case 1:
        //        treatmentImage.color = Color.green;
        //        break;
        //    case 2:
        //        treatmentImage.color = Color.blue;
        //        break;
        //    default:
        //        throw new System.Exception("TaskCard::SetHead exception: Invalid color");
        //}
    }

    public void TaskCompleted()
    {
        //foreach (Image image in GetComponentsInChildren<Image>())
        //{
        //    taskCompletedImage.gameObject.SetActive(true);
        //}
        soundFX.Play();

        taskCompletedImage.gameObject.SetActive(true);
        taskCompleted = true;
    }


    public void TaskFailed() {
        taskFailedImage.gameObject.SetActive(true);
        taskCompleted = false;
    }

    public void ScaleTaskCard(bool toSmall)
    {
        if (toSmall) transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        else         transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void Disable() {
        //foreach (Image image in GetComponentsInChildren<Image>()) {
        //    image?.gameObject?.SetActive(false);
        //}

        //headImage.gameObject.SetActive(false);
        //bodyImage.gameObject.SetActive(false);

        //OrnamentTypelot01Image.gameObject.SetActive(false);
        //OrnamentTypelot02Image.gameObject.SetActive(false);
        //OrnamentTypelot03Image.gameObject.SetActive(false);
    }



    [SerializeField] private Interactable interactable;
    private Transform veryNiceBoxTransform;
    private Coroutine coroutine;
    [SerializeField] private float respawnTimeWhenPlacedOnGround = 3;

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground" && interactable.ActiveHand == null && coroutine == null)
        {
            StartReturnToBoxCoroutine();
        }
    }

    public void StartReturnToBoxCoroutine()
    {
        coroutine = StartCoroutine("ReturnToNiceBoxAfterSeconds");
    }

    private IEnumerator ReturnToNiceBoxAfterSeconds()
    {
        yield return new WaitForSeconds(respawnTimeWhenPlacedOnGround);
        //Play Poof
        veryNiceBoxTransform = GameObject.FindGameObjectWithTag("VeryNiceBoxRespawn").transform;
        transform.position = new Vector3(veryNiceBoxTransform.position.x, veryNiceBoxTransform.position.y, veryNiceBoxTransform.position.z);
        coroutine = null;
        yield return null;
    }
}
