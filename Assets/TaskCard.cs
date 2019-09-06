using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <author>Kristoffer</author>
/// </summary>
public class TaskCard : MonoBehaviour
{
    public Image headImage;
    public Image bodyImage;

    public Image ornamentSlot01Image;
    public Image ornamentSlot02Image;
    public Image ornamentSlot03Image;

    public Sprite[] headSprites;
    public Sprite[] bodySprites;
    public Sprite[] ornamentSprites;

    [SerializeField]
    private Image taskCompletedImage;

    [SerializeField]
    private Image timerImage;
    private float timerWidth;
    private float timerWidthMax = 2f;
    private float timerWidthMin = 0f;
    private Color timerColor = new Color(0f, 255f, 0f);

    //float, lerp från max till min

    //private void Start()
    //{
    //    headImage.sprite = headSprites[Random.Range(0, headSprites.Length)];
    //    bodyImage.sprite = bodySprites[Random.Range(0, bodySprites.Length)];
    //    ornamentSlot01Image.sprite = ornamentSprites[Random.Range(0, ornamentSprites.Length)];
    //    ornamentSlot02Image.sprite = ornamentSprites[Random.Range(0, ornamentSprites.Length)];
    //    ornamentSlot03Image.sprite = ornamentSprites[Random.Range(0, ornamentSprites.Length)];
    //}

    //private void Update()
    //{
    //    timerWidth -= 0.01f;
    //    Debug.Log(timerWidth);
    //    timerImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, timerWidth);
    //    timerColor.g -= 1.5f;
    //    timerColor.r += 1.5f;
    //    timerImage.color = timerColor;
    //}

    public void UpdateTimerBar(float timerPercent)
    {
        timerImage.color = Color.Lerp(Color.green, Color.red, timerPercent);
        timerWidth = Mathf.Lerp(timerWidthMax, timerWidthMin, timerPercent);
        timerImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, timerWidth);
    }

    public void SetTaskIngredients(int Ornament01Type, int Ornament02Type, int Ornament03Type, int BodyType, int HeadType)
    {
        SetOrnaments(Ornament01Type, Ornament02Type, Ornament03Type);
        SetBody(BodyType);
        SetHead(HeadType);
    }

    private void SetOrnaments(int Ornament01Type, int Ornament02Type, int Ornament03Type)
    {
        ornamentSlot01Image.sprite = ornamentSprites[Ornament01Type];
        ornamentSlot02Image.sprite = ornamentSprites[Ornament02Type];
        ornamentSlot03Image.sprite = ornamentSprites[Ornament03Type];
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
        headImage.sprite = headSprites[HeadType];
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

    public void TaskCompleted()
    {
        foreach (Image image in GetComponentsInChildren<Image>())
        {
            taskCompletedImage.gameObject.SetActive(true);
        }
    }

    public void Disable() {
        foreach (Image image in GetComponentsInChildren<Image>()) {
            taskCompletedImage.gameObject.SetActive(false);
        }

        headImage.gameObject.SetActive(false);
        bodyImage.gameObject.SetActive(false);

        ornamentSlot01Image.gameObject.SetActive(false);
        ornamentSlot02Image.gameObject.SetActive(false);
        ornamentSlot03Image.gameObject.SetActive(false);
    }
}
